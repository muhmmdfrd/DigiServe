using Core.Enum;
using Repository1;
using System;
using System.Linq;

namespace Core.Manager.OrderPaymentManager
{
    public class OrderPaymentCreator : AsistanceBase<OrderPaymentAdapter, OrderPayment>
    {
        public OrderPaymentCreator(OrderPaymentAdapter manager) : base(manager)
        {
            // code here
        }

        public OrderPayment Save(OrderPaymentDTO dto, long userId)
        {
            var date = DateTime.Now.Date;
            var listTransac = Manager.OrderDetailManager.Value.Query.Value.OrderDetailForPayment(dto.OrderId, dto.UserId);
            var listPayment = Manager.Query.Value.TransformToList().Where(x => x.OrderId == dto.OrderId);

            if (listTransac.Count == 0)
            {
                throw new Exception("you don't have any order");
            }

            foreach (var valPay in listPayment)
            {
                foreach (var valTrans in listTransac)
                {
                    if (valPay.UserId == valTrans.UserId && valTrans.OrderId == valTrans.OrderId)
                    {
                        var update = Manager.Updater.Value.Update(dto, userId);
                        return update;
                    }
                }
            }

            var grandTotal = listTransac.Select(x => x.TotalPrice).Sum();
            var cashback = dto.Payment - grandTotal;
            var paymentDate = dto.OrderPaymentDate;
            var notePayment = dto.Note ?? "-";

            var newEntity = new OrderPayment
            {
                OrderPaymentCode = Guid.NewGuid().ToString(),
                OrderPaymentDate = paymentDate,
                PaymentStatus = (int)ChangeState.None,
                UserId = dto.UserId,
                OrderId = dto.OrderId,
                Payment = dto.Payment,
                PaymentMethod = dto.PaymentMethod,
                GrandTotal = grandTotal,
                Cashback = cashback,
                Note = notePayment,
                CreatedBy = (int)userId,
                CreatedDate = date,
                UpdatedBy = (int)userId,
                UpdatedDate = date
            };


            Manager.Database.OrderPayments.Add(newEntity);
            Manager.Database.SaveChanges();

            return newEntity;
        }
    }
}
