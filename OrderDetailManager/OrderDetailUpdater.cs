using Repository1;
using System;
using System.Linq;

namespace Core.Manager
{
    public class OrderDetailUpdater : AsistanceBase<OrderDetailAdapter, OrderDetail>
    {
        public OrderDetailUpdater(OrderDetailAdapter manager) : base(manager)
        {
            // code here
        }

        public OrderDetail Update(OrderDetailDTO dto, long userId)
        {
            var exist = Manager.Query.Value.All().FirstOrDefault(scope => scope.OrderDetailId == dto.OrderDetailId);

            if (exist == null)
            {
                throw new Exception("Order are doesn't exist");
            }
            else
            {
                var orderIdToday = Manager.OrderManager.Value.Query.Value.TransformToList()
                    .FirstOrDefault(x => x.CreateDate.Value.Day == DateTime.Now.Day).OrderId;
                var orderToday = Manager.OrderManager.Value.Query.Value.TransformToList()
                    .FirstOrDefault(x => x.OrderId == orderIdToday);

                if (orderToday.Status == Enum.OrderState.Close || orderToday.Status == Enum.OrderState.Start)
                {
                    throw new Exception("unfortunately, order have been closed");
                }

                exist.Qty = dto.Qty;
                exist.TotalPrice = exist.Price * exist.Qty; 
                exist.UpdatedBy = (int)userId;
                exist.UpdatedDate = DateTime.UtcNow;

                Manager.Database.SaveChanges();
            }
                return exist;
        }
    }
}
