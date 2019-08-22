using Core.Enum;
using Core.Manager.OrderDetailManager;
using Repository1;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Manager
{
    public class OrderDetailQuery : AsistanceBase<OrderDetailAdapter, OrderDetail>
    {
        public OrderDetailQuery(OrderDetailAdapter manager) : base(manager)
        {
            // code here
        }

        public IQueryable<OrderDetail> All(bool withDetail = false)
        {
            IQueryable<OrderDetail> orderDetail =
                withDetail ? Manager.Database.OrderDetails.AsQueryable().Include(x => x.User) : Manager.Database.OrderDetails;

            return orderDetail;
        }

        public IQueryable<OrderDetail> GetId(long orderDetailId)
        {
            return (from od in All() where od.OrderDetailId == orderDetailId select od).Take(1);
        }

        public IQueryable<OrderDetailDTO> TransformToQueryable()
        {
            return
                (from transform in All()
                 select new OrderDetailDTO
                 {
                     OrderDetailId = transform.OrderDetailId,
                     OrderId = transform.OrderId,
                     UserId = transform.UserId,
                 }).AsQueryable();
        }

        public List<OrderDetailDTO> OrderDetailForPayment(long orderId, long userId)
        {
            return
                (from transform in All()
                 where transform.OrderId == orderId && transform.UserId == userId
                 select new OrderDetailDTO
                 {
                     OrderDetailId = transform.OrderDetailId,
                     OrderId = transform.OrderId,
                     UserId = transform.UserId,
                     TotalPrice = transform.TotalPrice,
                     Qty = transform.Qty,
                     Note = transform.Note,
                     CreatedBy = transform.CreatedBy,
                     CreateDate = transform.CreateDate,
                     UpdatedBy = transform.UpdatedBy,
                     UpdatedDate = transform.UpdatedDate
                 }).ToList();
        }

        public List<OrderDetailDTO> TransformToList()
        {
            return
                (from transform in All()
                 select new OrderDetailDTO
                 {
                     OrderDetailId = transform.OrderDetailId,
                     OrderId = transform.OrderId,
                     UserId = transform.UserId,
                     MenuCode = transform.MenuCode,
                     TotalPrice = transform.TotalPrice,
                     Qty = transform.Qty,
                     Note = transform.Note,
                     CreatedBy = transform.CreatedBy,
                     CreateDate = transform.CreateDate,
                     UpdatedBy = transform.UpdatedBy,
                     UpdatedDate = transform.UpdatedDate
                 }).ToList();
        }

        public List<OrderDetailDTO> Join()
        {
            return
                (from orderDetail in All(true)
                 join menu in Manager.Database.Menus on orderDetail.MenuCode equals menu.MenuCode
                 select new OrderDetailDTO()
                 {
                    OrderDetailId = orderDetail.OrderDetailId,
                    OrderId = orderDetail.OrderId,
                    UserId = orderDetail.UserId,
                    Username = orderDetail.User.Username,
                    MenuCode = orderDetail.MenuCode,
                    Menu = menu.Name,
                    MenuType = menu.MenuType,
                    Adress = menu.Address,
                    Price = menu.Price,
                    IMEI = orderDetail.User.IMEI,
                    Name = orderDetail.User.Person.Name,
                    TotalPrice = orderDetail.TotalPrice,
                    Qty = orderDetail.Qty,
                    Note = orderDetail.Note,
                    CreatedBy = orderDetail.CreatedBy,
                    CreateDate = orderDetail.CreateDate,
                    UpdatedBy = orderDetail.UpdatedBy,
                    UpdatedDate = orderDetail.UpdatedDate
                 }).ToList();
        }

        public List<OrderDetailIdDTO> OrderJoinId(long id)
        {   
            return
                (from orderDetail in All(true)
                 join menu in Manager.Database.Menus on orderDetail.MenuCode equals menu.MenuCode
                 where orderDetail.OrderId == id
                 select new OrderDetailIdDTO()
                 {
                     OrderId = orderDetail.OrderId,
                     OrderDetailId = orderDetail.OrderDetailId,
                     Name = orderDetail.User.Person.Name,
                     Qty = orderDetail.Qty,
                     Note = orderDetail.Note,
                     Menu = menu.Name,
                     MenuType = menu.MenuType,
                     Address = menu.Address,
                     Price = menu.Price,
                     TotalPrice = orderDetail.TotalPrice,
                     CreatedBy = (int)orderDetail.UserId,
                     CreatedDate = orderDetail.CreateDate,
                     UpdatedBy = (int)orderDetail.UserId,
                     UpdatedDate = orderDetail.UpdatedDate
                 }).OrderBy(x => x.Name).ToList();
        }

        public List<OrderDetailIdDTO> OrderToday()
        {
            return
                (from orderDetail in All(true)
                 join menu in Manager.Database.Menus on orderDetail.MenuCode equals menu.MenuCode
                 where DateTime.UtcNow.Day == orderDetail.Order.CreateDate.Value.Day
                 select new OrderDetailIdDTO()
                 {
                     OrderId = orderDetail.OrderId,
                     OrderDetailId = orderDetail.OrderDetailId,
                     Name = orderDetail.User.Person.Name,
                     Note = orderDetail.Note,
                     Qty = orderDetail.Qty,
                     TotalPrice = orderDetail.TotalPrice,
                     Menu = menu.Name,
                     MenuType = menu.MenuType,
                     Address = menu.Address,
                     Price = menu.Price,
                     CreatedBy = (int)orderDetail.UserId,
                     CreatedDate = orderDetail.CreateDate,
                     UpdatedBy = (int)orderDetail.UserId,
                     UpdatedDate = orderDetail.UpdatedDate
                 }).ToList();
        }

        public List<OrderDetailIdDTO> OrderByUser(long id)
        {
            return
                (from orderDetail in All(true)
                 join menu in Manager.Database.Menus on orderDetail.MenuCode equals menu.MenuCode
                 where orderDetail.UserId == id
                 where orderDetail.CreateDate.Value.Day == DateTime.Now.Day
                 select new OrderDetailIdDTO()
                 {
                     OrderId = orderDetail.OrderId,
                     OrderDetailId = orderDetail.OrderDetailId,
                     Name = orderDetail.User.Person.Name,
                     Qty = orderDetail.Qty,
                     Note = orderDetail.Note,
                     Menu = menu.Name,
                     MenuType = menu.MenuType,
                     Address = menu.Address,
                     Price = menu.Price,
                     TotalPrice = orderDetail.TotalPrice,
                     CreatedBy = (int)orderDetail.UserId,
                     CreatedDate = orderDetail.CreateDate,
                     UpdatedBy = (int)orderDetail.UserId,
                     UpdatedDate = orderDetail.UpdatedDate
                 }).ToList();
        }

        public List<OrderDetailIdDTO> OrderByHistory(long id)
        {
            return
                (from orderDetail in All(true)
                 join menu in Manager.Database.Menus on orderDetail.MenuCode equals menu.MenuCode
                 where orderDetail.UserId == id
                 where orderDetail.Order.Status == (int)OrderState.Close
                 select new OrderDetailIdDTO()
                 {
                     OrderId = orderDetail.OrderId,
                     OrderDetailId = orderDetail.OrderDetailId,
                     Name = orderDetail.User.Person.Name,
                     Qty = orderDetail.Qty,
                     Note = orderDetail.Note,
                     TotalPrice = orderDetail.TotalPrice,
                     Menu = menu.Name,
                     MenuType = menu.MenuType,
                     Address = menu.Address,
                     Price = menu.Price,
                     CreatedBy = (int)orderDetail.UserId,
                     CreatedDate = orderDetail.CreateDate,
                     UpdatedBy = (int)orderDetail.UserId,
                     UpdatedDate = orderDetail.UpdatedDate
                 }).ToList();
        }
        
        public List<OrderDetailIdDTO> OrderByHistoryInDay(long userId, long orderId)
        {
            return
                (from orderDetail in All(true)
                 join menu in Manager.Database.Menus on orderDetail.MenuCode equals menu.MenuCode
                 where orderDetail.UserId == userId
                 where orderDetail.Order.OrderId == orderId
                 where orderDetail.Order.Status == (int)OrderState.Close
                 select new OrderDetailIdDTO()
                 {
                     OrderId = orderDetail.OrderId,
                     OrderDetailId = orderDetail.OrderDetailId,
                     UserId = orderDetail.UserId,
                     Name = orderDetail.User.Person.Name,
                     Qty = orderDetail.Qty,
                     Note = orderDetail.Note,
                     Menu = menu.Name,
                     MenuType = menu.MenuType,
                     Address = menu.Address,
                     Price = menu.Price,
                     TotalPrice = orderDetail.TotalPrice,
                     CreatedBy = (int)orderDetail.UserId,
                     CreatedDate = orderDetail.CreateDate,
                     UpdatedBy = (int)orderDetail.UserId,
                     UpdatedDate = orderDetail.UpdatedDate
                 }).ToList();
        }

        
        public List<OrderDetailGroupDTO> TransformToGroupAll(long orderId)
        {
            var detaiMenu = 
                (from detailMenu in All(true)
                 join mn in Manager.Database.Menus on detailMenu.MenuCode equals mn.MenuCode
                 where detailMenu.UserId == detailMenu.User.UserId
                 where detailMenu.OrderId == orderId
                 select new OrderDetailMenuDTO()
                 {
                    UserId = detailMenu.User.UserId,
                    OrderDetailId = detailMenu.OrderDetailId,
                    Qty = detailMenu.Qty,
                    Note = detailMenu.Note,
                    Menu = mn.Name,
                    MenuCode = mn.MenuCode,
                    Address = mn.Address,
                    Price = mn.Price,
                    TotalPrice = detailMenu.TotalPrice,
                 });

            return 
                (from pay in Manager.OrderPaymentManager.Value.Query.Value.All(true)
                 where pay.OrderId == orderId
                 select new OrderDetailGroupDTO
                 {
                    UserId = pay.User.UserId,
                    NameCustomer = pay.User.Person.Name,
                    GrandTotal = pay.GrandTotal,
                    Payment = pay.Payment,
                    MenuOrdered = detaiMenu.Where(s => s.UserId == pay.User.UserId).ToList(),
                    Cashback = pay.Cashback,
                    Note = pay.Note
                 }).ToList();
        }

        public List<OrderDetailGroupDTO> TransformToGroupUser(long userId, long orderId)
        {
            var detaiMenu =
                (from detailMenu in All(true)
                 join mn in Manager.Database.Menus on detailMenu.MenuCode equals mn.MenuCode
                 where detailMenu.UserId == userId
                 where detailMenu.OrderId == orderId
                 select new OrderDetailMenuDTO()
                 {
                     UserId = detailMenu.User.UserId,
                     OrderDetailId = detailMenu.OrderDetailId,
                     Qty = detailMenu.Qty,
                     Note = detailMenu.Note,
                     Menu = mn.Name,
                     MenuCode = mn.MenuCode,
                     Address = mn.Address,
                     Price = mn.Price,
                     TotalPrice = detailMenu.TotalPrice,
                 });

            return
                (from pay in Manager.OrderPaymentManager.Value.Query.Value.All(true)
                 where pay.User.UserId == userId
                 where pay.OrderId == orderId
                 select new OrderDetailGroupDTO
                 {
                     UserId = pay.User.UserId,
                     NameCustomer = pay.User.Person.Name,
                     GrandTotal = pay.GrandTotal,
                     Payment = pay.Payment,
                     MenuOrdered = detaiMenu.Where(s => s.UserId == pay.User.UserId).ToList(),
                     Cashback = pay.Cashback,
                     Note = pay.Note
                 }).ToList();
        }

        public List<OrderDetailGroupDTO> TransformToGroupOthers(long userId, long orderId)
        {
            var detaiMenu =
                (from detailMenu in All(true)
                 join mn in Manager.Database.Menus on detailMenu.MenuCode equals mn.MenuCode
                 where detailMenu.UserId != userId
                 where detailMenu.OrderId == orderId
                 select new OrderDetailMenuDTO()
                 {
                     UserId = detailMenu.User.UserId,
                     OrderDetailId = detailMenu.OrderDetailId,
                     Qty = detailMenu.Qty,
                     Note = detailMenu.Note,
                     Menu = mn.Name,
                     MenuCode = mn.MenuCode,
                     Address = mn.Address,
                     Price = mn.Price,
                     TotalPrice = detailMenu.TotalPrice,
                 });

            return
                (from pay in Manager.OrderPaymentManager.Value.Query.Value.All(true)
                 where pay.User.UserId != userId
                 where pay.OrderId == orderId
                 select new OrderDetailGroupDTO
                 {
                     UserId = pay.User.UserId,
                     NameCustomer = pay.User.Person.Name,
                     GrandTotal = pay.GrandTotal,
                     Payment = pay.Payment,
                     MenuOrdered = detaiMenu.Where(s => s.UserId == pay.User.UserId).ToList(),
                     Cashback = pay.Cashback,
                     Note = pay.Note
                 }).ToList().GroupBy(scope => scope.NameCustomer).Select(group => group.First()).ToList();
        }

        public List<OrderDetailMenuGroupDTO> OrderGroupMenuByUser(long orderId)
        {
            var customer =
                from od in All(true)
                join mn in Manager.Database.Menus on od.MenuCode equals mn.MenuCode 
                where od.OrderId == orderId
                select new CustomerDTO
                {
                    UserId = od.User.UserId,
                    MenuCode = od.MenuCode,
                    Name = od.User.Person.Name,
                    Note = od.Note,
                    Qty = od.Qty
                };

            return
                (from od in Manager.Database.OrderDetails
                 join mn in Manager.Database.Menus on od.MenuCode equals mn.MenuCode
                 where od.OrderId == orderId
                 select new OrderDetailMenuGroupDTO
                 {
                     Menu = mn.Name,
                     Price = od.Price,
                     TotalQty = customer.Where(x => x.MenuCode.Equals(od.MenuCode)).Select(y => y.Qty).Sum(),
                     TotalPrice = customer.Where(x => x.MenuCode.Equals(od.MenuCode)).Select(y => y.Qty).Sum() * od.Price,
                     Customer = customer.Where(x => x.MenuCode.Equals(od.MenuCode)).ToList()
                 }).ToList().GroupBy(x => x.Menu).Select(group => group.First()).ToList();
        }
    }
}
