using Repository1;
using System;
using System.Transactions;

namespace Core.Manager.MenuHistoryManager
{
    public class MenuHistoryCreator : AsistanceBase<MenuHistoryAdapter, MenuHistory>
    {
        public MenuHistoryCreator(MenuHistoryAdapter manager) : base(manager)
        {
            // code here
        }

        public MenuHistory Save(MenuHistoryDTO dto, long userId)
        {
            using (var transac = new TransactionScope())
            {
                var date = DateTime.UtcNow;

                var newEntity = new MenuHistory
                {
                    MenuCode = dto.MenuCode,
                    MenuType = dto.MenuType,
                    Name = dto.Name,
                    Address = dto.Address,
                    CreatedBy = (int)userId,
                    CreatedDate = date,
                    UpdatedBy = (int)userId,
                    UpdatedDate = date,
                    Price = dto.Price
                };

                Manager.Database.MenuHistories.Add(newEntity);
                Manager.Database.SaveChanges();
                transac.Complete();

                return newEntity;
            } 
        }
    }
}
