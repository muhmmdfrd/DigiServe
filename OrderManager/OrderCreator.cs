using Core.Enum;
using Repository1;
using System;
using System.Transactions;

namespace Core.Manager.OrderManager
{
    public class OrderCreator : AsistanceBase<OrderAdapter, Order>
    {
        public OrderCreator(OrderAdapter manager) : base(manager)
        {
            // code here
        }

        public Order Save(OrderDTO dto, long userId)
        {
            using (var transac = new TransactionScope())
            {
                var date = DateTime.UtcNow.Date;

                var newEntity = new Order
                {
                    OrderCode = "ORD000" + dto.OrderCode,
                    Status = (int)dto.Status,
                    OrderDate = date,
                    CreatedBy = (int)userId,
                    CreateDate = date,
                    UpdatedBy = (int)userId,
                    UpdatedDate = date
                };
                Manager.Database.Orders.Add(newEntity);
                Manager.Database.SaveChanges();
                transac.Complete();

                return newEntity;
            }
        }

        public Order CreateOrder()
        {
            using (var transac = new TransactionScope())
            {
                var UserId = 1;
                var date = DateTime.UtcNow;

                var newEntity = new Order
                {
                    OrderCode = "ORD-" +date.ToString("yyyyMMdd"),
                    Status = (int)OrderState.Open,
                    OrderDate = date,
                    CreatedBy = UserId,
                    CreateDate = date,
                    UpdatedBy = UserId,
                    UpdatedDate = date
                };
                Manager.Database.Orders.Add(newEntity);
                Manager.Database.SaveChanges();
                transac.Complete();

                return newEntity;
            }
        }
    }
}
