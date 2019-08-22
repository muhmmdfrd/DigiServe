using Core.Enum;
using Core.Manager.OrderPaymentManager;
using Repository1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Core.Manager.OrderManager
{
    public class OrderUpdater : AsistanceBase<OrderAdapter, Order>
    {
        public OrderUpdater(OrderAdapter manager) : base(manager)
        {
            // code here
        }

        public List<OnHoldChangeDTO> CloseOrder()
        {
            using (var transac = new TransactionScope())
            {
                using (var manager = new OrderAdapter())
                {
                    var lastOrder = manager.Query.Value.Get().OrderByDescending(x => x.OrderId).FirstOrDefault();

                    if (lastOrder == null)
                    {
                        throw new Exception("Order doesn't exist");
                    }

                    var orderDetailToday = Manager.OrderDetailManager.Value
                        .OrderPaymentManager.Value.Query.Value.All()
                        .Where(x => x.OrderId == lastOrder.OrderId);

                    foreach (var val in orderDetailToday)
                    {
                        if (val.PaymentStatus == (int)ChangeState.OnHold)
                        {
                            var userOnHold =
                                (from op in Manager.Database.OrderPayments
                                 join od in Manager.Database.OrderDetails on op.OrderId equals od.OrderId
                                 join u in Manager.Database.Users on op.UserId equals u.UserId
                                 join p in Manager.Database.People on u.PersonId equals p.PersonId
                                 where op.PaymentStatus == (int)ChangeState.OnHold
                                 select new OnHoldChangeDTO
                                 {
                                     UserId = u.UserId,
                                     Name = p.Name,
                                     Cashback = op.Cashback
                                 }).ToList().GroupBy(x => x.Name).Select(group => group.First()).ToList();

                            return userOnHold;
                        }
                    }

                    lastOrder.Status = (int)OrderState.Close;
                    var myStatus = lastOrder.Status;
                    manager.Database.SaveChanges();
                }
                transac.Complete();
                return null;
            }
        }

        public Order Update(OrderDTO dto, long userId)
        {
            using (var transac = new TransactionScope())
            {
                var exist = Manager.Query.Value.Get().FirstOrDefault(scope => scope.OrderId == dto.OrderId);

                if (exist == null)
                {
                    throw new Exception("You are don't exist");
                }
                else
                {
                    exist.OrderId = dto.OrderId;
                    exist.OrderCode = dto.OrderCode;
                    exist.Status = (int)dto.Status;
                    exist.OrderDate = dto.OrderDate;
                    exist.UpdatedBy = (int)userId;
                    exist.UpdatedDate = DateTime.UtcNow;
                    Manager.Database.SaveChanges();
                }

                transac.Complete();

                return exist;
            }
        }

        public string StartOrder()
        {
            using (var transac = new TransactionScope())
            {
                using (var manager = new OrderAdapter())
                {
                    var lastOrder = manager.Query.Value.Get().OrderByDescending(x => x.OrderId).FirstOrDefault();

                    if (lastOrder == null)
                    {
                        throw new Exception("Order doesn't exist");
                    }

                    lastOrder.Status = (int)OrderState.Start;
                    var myStatus = lastOrder.Status;
                    manager.Database.SaveChanges();
                }
                transac.Complete();
            }
            return "started";
        }
    }
}
