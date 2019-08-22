using Repository1;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.MenuHistoryManager
{
    public class MenuHistoryDeleter : AsistanceBase<MenuHistoryAdapter, MenuHistory>
    {
        public MenuHistoryDeleter(MenuHistoryAdapter manager) : base(manager)
        {
            // code here
        }

        public string Delete(long id)
        {
            using (var transac = new TransactionScope())
            {
                var exist = Manager.Query.Value.Get().FirstOrDefault(a => a.MenuHistoryId == id);
                if (exist == null)
                {
                    throw new Exception("Menu History doesn't exist");
                }
                else
                {
                    Manager.Database.MenuHistories.Remove(exist);
                    Manager.Database.SaveChanges();
                }

                transac.Complete();

                return "Data Deleted";
            }
        }
    }
}
