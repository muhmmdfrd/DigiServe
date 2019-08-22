using Repository1;
using System.Collections.Generic;
using System.Linq;

namespace Core.Manager.MenuHistoryManager
{
    public class MenuHistoryQuery : AsistanceBase<MenuHistoryAdapter, MenuHistory>
    {
        public MenuHistoryQuery(MenuHistoryAdapter manager) : base(manager)
        {
            // code here
        }

        public IQueryable<MenuHistory> Get()
        {
            return Manager.Database.MenuHistories;
        }

        public MenuHistoryDTO TransformSingle(long id)
        {
            return 
                (from transform in Get()
                    where transform.MenuHistoryId == id
                    select new MenuHistoryDTO()
                    {
                        MenuHistoryId = transform.MenuHistoryId,
                        MenuCode = transform.MenuCode,
                        MenuType = transform.MenuType,
                        Name = transform.Name,
                        Address = transform.Address,
                        CreatedBy = transform.CreatedBy,
                        CreatedDate = transform.CreatedDate,
                        UpdatedBy = transform.UpdatedBy,
                        UpdatedDate = transform.UpdatedDate,
                        Price = transform.Price
                    }).FirstOrDefault();
        }

        public IQueryable<MenuHistoryDTO> TransformToQueryable()
        {
            return
                (from transform in Get()
                 select new MenuHistoryDTO
                 {
                     MenuHistoryId = transform.MenuHistoryId,
                     MenuCode = transform.MenuCode,
                     MenuType = transform.MenuType,
                     Name = transform.Name,
                     Price = transform.Price,
                     Address = transform.Address,
                     CreatedBy = transform.CreatedBy,
                     CreatedDate = transform.CreatedDate,
                     UpdatedBy = transform.UpdatedBy,
                     UpdatedDate = transform.UpdatedDate
                 }).AsQueryable();
        }

        public List<MenuHistoryDTO> Transform()
        {
            return 
                (from transform in Get()
                    select new MenuHistoryDTO()
                    {
                        MenuHistoryId = transform.MenuHistoryId,
                        MenuCode = transform.MenuCode,
                        MenuType = transform.MenuType,
                        Name = transform.Name,
                        Address = transform.Address,
                        CreatedBy = transform.CreatedBy,
                        CreatedDate = transform.CreatedDate,
                        UpdatedBy = transform.UpdatedBy,
                        UpdatedDate = transform.UpdatedDate,
                        Price = transform.Price
                    }).ToList();
        }
    }
}
