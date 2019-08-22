using Core.Enum;
using Repository1;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Manager.OrderManager
{
    public class OrderQuery : AsistanceBase<OrderAdapter, Order>
    {
        public OrderQuery(OrderAdapter manager) : base(manager)
        {
            // code here
        }

        public IQueryable<Order> Get(bool withDetail = false)
        {
            IQueryable<Order> order = 
                withDetail ? Manager.Database.Orders.AsQueryable().Include(x => x.OrderPayments) : Manager.Database.Orders;

            return order;
        }

        public JustDate TransformDate()
        {
            return (Get().OrderByDescending(scope => scope.CreateDate)
                   .Select(scope => new JustDate() { CreateDate = scope.CreateDate }))
                   .FirstOrDefault();
        }

        public OrderDTO TranformSingle(long id)
        {
            return
                (from transform in Get()
                 where transform.OrderId == id
                 select new OrderDTO()
                 {
                     OrderId = transform.OrderId,
                     OrderCode = transform.OrderCode,
                     Status = (OrderState)transform.Status,
                     OrderDate = transform.OrderDate,
                     CreatedBy = transform.CreatedBy,
                     CreateDate = transform.CreateDate,
                     UpdatedBy = transform.UpdatedBy,
                     UpdatedDate = transform.UpdatedDate

                 }).FirstOrDefault();
        }

        public List<OrderDTO> TransformToList()
        {
            return 
                (from transform in Get()
                 select new OrderDTO()
                 {
                    OrderId = transform.OrderId,
                    OrderCode = transform.OrderCode,
                    Status = (OrderState)transform.Status,
                    OrderDate = transform.OrderDate,
                    CreatedBy = transform.CreatedBy,
                    CreateDate = transform.CreateDate,
                    UpdatedBy = transform.UpdatedBy,
                    UpdatedDate = transform.UpdatedDate
                 }).ToList();
        }

        public OrderDTO TransformGetDTO(long id, bool withDetails = false)
        {
            var details = Manager.OrderDetailManager.Value.Query.Value.TransformToQueryable();
            var header = 
                (from transform in Get()
                 where transform.OrderId == id
                 select new OrderDTO()
                 {
                    OrderId = transform.OrderId,
                    OrderCode = transform.OrderCode,
                    Status = (OrderState)transform.Status,
                    OrderDate = transform.OrderDate,
                    CreatedBy = transform.CreatedBy,
                    CreateDate = transform.CreateDate,
                    UpdatedBy = transform.UpdatedBy,
                    UpdatedDate = transform.UpdatedDate
                 }).FirstOrDefault();

            if (header != null)
            {
                if ((withDetails && details.Count() > 0))
                {
                    header.OrderDetailDTOs = details.Where(s => s.OrderId == header.OrderId).ToList();
                }
            }

            return header;
        }
    }
}
