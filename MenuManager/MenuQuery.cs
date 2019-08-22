using Repository1;
using System.Collections.Generic;
using System.Linq;

namespace Core.Manager.MenuManager
{
    public class MenuQuery : AsistanceBase<MenuAdapter, Menu>
    {
        public MenuQuery(MenuAdapter manager) : base(manager)
        {
            // code here
        }

        public IQueryable<Menu> Get()
        {
            return Manager.Database.Menus;
        }

        public MenuDTO TransformPost(long id)
        {
            return (from transform in Get()
                    where transform.MenuId == id
                    select new MenuDTO()
                    {
                        MenuId = transform.MenuId,
                        MenuCode = transform.MenuCode,
                        MenuType = transform.MenuType,
                        Name = transform.Name,
                        Address = transform.Address,
                        CreatedBy = transform.CreatedBy,
                        CreateDate = transform.CreateDate,
                        UpdatedBy = transform.UpdatedBy,
                        UpdatedDate = transform.UpdatedDate,
                        Price = transform.Price
                    }).FirstOrDefault();
        }

        public IQueryable<MenuDTO> TransformToQueryable()
        {
            return
                (from transform in Get()
                 select new MenuDTO
                 {
                     MenuId = transform.MenuId,
                     MenuCode = transform.MenuCode,
                     MenuType = transform.MenuType,
                     Name = transform.Name,
                     Price = transform.Price,
                     Address = transform.Address,
                     CreatedBy = transform.CreatedBy,
                     CreateDate = transform.CreateDate,
                     UpdatedBy = transform.UpdatedBy,
                     UpdatedDate = transform.UpdatedDate 
                 }).AsQueryable();
        }

        public List<MenuDTO> Transform()
        {
            return (from transform in Get()
                select new MenuDTO()
                {
                    MenuId = transform.MenuId,
                    MenuCode = transform.MenuCode,
                    MenuType = transform.MenuType,
                    Name = transform.Name,
                    Address = transform.Address,
                    CreatedBy = transform.CreatedBy,
                    CreateDate = transform.CreateDate,
                    UpdatedBy = transform.UpdatedBy,
                    UpdatedDate = transform.UpdatedDate,
                    Price = transform.Price
                }).ToList();
        }
    }
}
