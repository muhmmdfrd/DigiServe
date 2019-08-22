using Repository1;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.MenuHistoryManager
{
    public class MenuHistoryUpdater : AsistanceBase<MenuHistoryAdapter, MenuHistory>
    {
        public MenuHistoryUpdater(MenuHistoryAdapter manager) : base(manager)
        {
            // code here
        }

        public MenuHistory Update(MenuHistoryDTO dto)
        {
            using (var transac = new TransactionScope())
            {
                var userId = 1;
                var date = DateTime.UtcNow;

                var exist = Manager.Query.Value.Get().FirstOrDefault(s => s.MenuHistoryId == dto.MenuHistoryId);
                if (exist == null)
                {
                    throw new Exception("Your menu doesn't exist");
                }
                else
                {
                    exist.MenuCode = dto.MenuCode;
                    exist.MenuType = dto.MenuType;
                    exist.Name = dto.Name;
                    exist.Address = dto.Address;
                    exist.Price = dto.Price;
                    exist.UpdatedBy = userId;
                    exist.UpdatedDate = date;
                    Manager.Database.SaveChanges();
                }

                transac.Complete();

                return exist;
            }
        }
    }
}
