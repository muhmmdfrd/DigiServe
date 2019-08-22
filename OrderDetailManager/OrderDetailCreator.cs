using Repository1;
using System;
using System.Linq;

namespace Core.Manager
{
    public class OrderDetailCreator : AsistanceBase<OrderDetailAdapter, OrderDetail>
    {
        public OrderDetailCreator(OrderDetailAdapter manager) : base(manager)
        {
            // code here
        }
        public OrderDetail Save(OrderDetailDTO dto, long userId)
        {
            var orderDetail = new OrderDetail();
            var date = DateTime.UtcNow;
            var orderIdToday = Manager.OrderManager.Value.Query.Value.Get().FirstOrDefault(scope => scope.CreateDate.Value.Day == DateTime.UtcNow.Day).OrderId;

            if (orderIdToday == 0)
            {
                return null;
            }

            string note = dto.Note;

            if (note == null || string.IsNullOrEmpty(note))
            {
                note = "-";
            }

            var priceMenu = Manager.MenuManager.Value.Query.Value.Transform().FirstOrDefault(x => x.MenuCode == dto.MenuCode).Price;
            var addTotalPrice = 0.0;
            var duplicateOrder = Manager.Query.Value.All().
                FirstOrDefault(s => s.OrderId == orderIdToday && s.UserId == dto.UserId && s.MenuCode == dto.MenuCode);
            var orderToday = Manager.OrderManager.Value.Query.Value.TransformToList().FirstOrDefault(x => x.OrderId == orderIdToday);

            if (orderToday.Status == Enum.OrderState.Start || orderToday.Status == Enum.OrderState.Close)
            {
                throw new Exception("Unfortunately, order have been closed");
            }

            if (duplicateOrder != null)
            {
                addTotalPrice = Convert.ToDouble(dto.Qty * priceMenu);
                
                //update harga dan qty
                duplicateOrder.Qty += dto.Qty;
                duplicateOrder.TotalPrice += addTotalPrice;
                duplicateOrder.UpdatedBy = (int)userId;
                duplicateOrder.UpdatedDate = date;
                orderDetail = duplicateOrder;
            }
            else
            {
                //insert baru
                var newEntity = new OrderDetail
                {
                    OrderId = orderIdToday,
                    MenuCode = dto.MenuCode,
                    UserId = dto.UserId,
                    Qty = dto.Qty,
                    Note = note,
                    CreatedBy = (int)userId,
                    CreateDate = date,
                    UpdatedBy = (int)userId,
                    UpdatedDate = date
                };
                newEntity.Price = priceMenu;
                addTotalPrice = Convert.ToDouble(newEntity.Price * newEntity.Qty);
                newEntity.TotalPrice = addTotalPrice;

                Manager.Database.OrderDetails.Add(newEntity);
                orderDetail = newEntity;
            }

            Manager.Database.SaveChanges();

            return orderDetail;
        }
    }
}
