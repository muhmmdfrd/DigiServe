using Core.Enum;
using Repository1;
using System;
using System.Linq;

namespace Core.Manager.OrderPaymentManager
{
    public class OrderPaymentUpdater : AsistanceBase<OrderPaymentAdapter, OrderPayment>
    {
        public OrderPaymentUpdater(OrderPaymentAdapter manager) : base(manager)
        {
            // code here
        }

        public OrderPayment Update(OrderPaymentDTO dto, long userId)
        {
            var existingPayment = Manager.Query.Value.All();
            var exist = existingPayment.FirstOrDefault(scope => scope.OrderId == dto.OrderId && scope.UserId == dto.UserId);
            var orderStarted = Manager.OrderDetailManager.Value.OrderManager.Value.Query.Value.TransformToList()
                .FirstOrDefault(x => x.OrderId == dto.OrderId);

            if (exist == null)
            {
                throw new Exception("Your order is doesn't exist :)");
            }
            else if (orderStarted.Status == OrderState.Close)
            {
                throw new Exception("unfortunately, order today have been closed");
            }
            else
            {
                var listTransac = Manager.OrderDetailManager.Value.Query.Value.OrderDetailForPayment(dto.OrderId, dto.UserId);

                exist.GrandTotal = listTransac.Select(x => x.TotalPrice).Sum();
                exist.Payment = (dto.Payment == 0) ? exist.Payment : dto.Payment;
                exist.Cashback = exist.Payment - exist.GrandTotal;

                var paymentStat = ChangeState.OnHold;

                if (exist.Cashback == 0)
                {
                    paymentStat = ChangeState.OnComplete;
                }
                else if (exist.Cashback < 0)
                {
                    paymentStat = ChangeState.None;
                }
                else
                {
                    paymentStat = ChangeState.OnHold;
                }

                exist.PaymentStatus = (int)paymentStat;
                exist.UpdatedBy = userId;
                exist.Note = dto.Note ?? exist.Note;
                exist.UpdatedDate = DateTime.UtcNow;

                Manager.Database.SaveChanges();
            }
            return exist;
        }

        public OrderPayment Submit(OrderPaymentDTO dto, long userId)
        {
            var exist = Manager.Query.Value.All().FirstOrDefault(scope => scope.OrderId == dto.OrderId && scope.UserId == dto.UserId);

            if (exist == null)
            {
                throw new Exception("Your order is doesn't exist :)");
            }
            else
            {
                if (exist.Cashback < 0)
                {
                    throw new Exception("you have a debt");
                }

                exist.PaymentStatus = (int)ChangeState.OnComplete;
                exist.UpdatedBy = userId;
                exist.UpdatedDate = DateTime.UtcNow;
                Manager.Database.SaveChanges();
            }
            return exist;
        }
    }
}
