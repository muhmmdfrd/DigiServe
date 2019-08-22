using Core.Enum;
using Repository1;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Manager.OrderPaymentManager
{
    public class OrderPaymentQuery : AsistanceBase<OrderPaymentAdapter, OrderPayment>
    {
        public OrderPaymentQuery(OrderPaymentAdapter manager) : base(manager)
        {
            // code here
        }

        public IQueryable<OrderPayment> All(bool includeDetail = false)
        {
            IQueryable<OrderPayment> orderPayment =
                includeDetail ? Manager.Database.OrderPayments.AsQueryable().Include(s => s.User) : Manager.Database.OrderPayments;

            return orderPayment;
        }

        public OrderPaymentDTO TranformSingle(long id)
        {
            return
                (from transform in All()
                 where transform.OrderPaymentId == id
                 select new OrderPaymentDTO()
                 {
                     OrderPaymentId = transform.OrderPaymentId,
                     OrderPaymentCode = transform.OrderPaymentCode,
                     OrderPaymentDate = transform.OrderPaymentDate,
                     PaymentStatus = (ChangeState)transform.PaymentStatus,
                     OrderId = transform.OrderId,
                     UserId = transform.UserId,
                     Payment = transform.Payment,
                     GrandTotal = transform.GrandTotal,
                     Cashback = transform.Cashback,
                     CreatedBy = transform.CreatedBy,
                     CreatedDate = transform.CreatedDate,
                     UpdatedBy = transform.UpdatedBy,
                     UpdatedDate = transform.UpdatedDate
                 }).FirstOrDefault();
        }

        public List<OrderPaymentDTO> TransformToList()
        {
            return
                (from transform in All(true)
                 join person in Manager.Database.People on transform.User.Person.PersonId equals person.PersonId
                 select new OrderPaymentDTO()
                 {
                     OrderPaymentId = transform.OrderPaymentId,
                     OrderPaymentCode = transform.OrderPaymentCode,
                     OrderPaymentDate = transform.OrderPaymentDate,
                     PaymentMethod = transform.PaymentMethod,
                     Note = transform.Note,
                     PaymentStatus = (ChangeState)transform.PaymentStatus,
                     OrderId = transform.OrderId,
                     UserId = transform.UserId,
                     Name = person.Name,
                     Payment = transform.Payment,
                     GrandTotal = transform.GrandTotal,
                     Cashback = transform.Cashback,
                     CreatedBy = transform.CreatedBy,
                     CreatedDate = transform.CreatedDate,
                     UpdatedBy = transform.UpdatedBy,
                     UpdatedDate = transform.UpdatedDate
                 }).ToList();
        }

        public List<OrderPaymentDTO> TransformUserList(long userId, long orderId)
        {
            return
                (from transform in All(true)
                 join person in Manager.Database.People on transform.User.Person.PersonId equals person.PersonId
                 select new OrderPaymentDTO
                 {
                     OrderPaymentId = transform.OrderPaymentId,
                     OrderPaymentCode = transform.OrderPaymentCode,
                     OrderPaymentDate = transform.OrderPaymentDate,
                     PaymentMethod = transform.PaymentMethod,
                     Note = transform.Note,
                     PaymentStatus = (ChangeState)transform.PaymentStatus,
                     OrderId = transform.OrderId,
                     UserId = transform.UserId,
                     Name = person.Name,
                     Payment = transform.Payment,
                     GrandTotal = transform.GrandTotal,
                     Cashback = transform.Cashback,
                     CreatedBy = transform.CreatedBy,
                     CreatedDate = transform.CreatedDate,
                     UpdatedBy = transform.UpdatedBy,
                     UpdatedDate = transform.UpdatedDate
                 }).ToList();
        }

        public List<OrderPaymentDTO> TransformOrderList(long orderId)
        {
            return
               (from transform in All(true)
                join person in Manager.Database.People on transform.User.Person.PersonId equals person.PersonId
                select new OrderPaymentDTO
                {
                    OrderPaymentId = transform.OrderPaymentId,
                    OrderPaymentCode = transform.OrderPaymentCode,
                    OrderPaymentDate = transform.OrderPaymentDate,
                    PaymentMethod = transform.PaymentMethod,
                    Note = transform.Note,
                    PaymentStatus = (ChangeState)transform.PaymentStatus,
                    OrderId = transform.OrderId,
                    UserId = transform.UserId,
                    Name = person.Name,
                    Payment = transform.Payment,
                    GrandTotal = transform.GrandTotal,
                    Cashback = transform.Cashback,
                    CreatedBy = transform.CreatedBy,
                    CreatedDate = transform.CreatedDate,
                    UpdatedBy = transform.UpdatedBy,
                    UpdatedDate = transform.UpdatedDate
                }).ToList();
        }


    }
}
