using Repository1;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.MenuManager
{
    public class MenuDeleter : AsistanceBase<MenuAdapter, Menu>
    {
        public MenuDeleter(MenuAdapter manager) : base(manager)
        {
            // code here
        }

        public string Delete(long id)
        {
            using (var transac = new TransactionScope())
            {
                var exist = Manager.Query.Value.Get().FirstOrDefault(a => a.MenuId == id);
                var orderDetail = Manager.OrderDetailManager.Value.Query.Value.TransformToList();

                if (exist == null)
                {
                    throw new Exception("Menu doesn't exist");
                }
                else
                {
                    foreach (var val in orderDetail)
                    {
                        if (val.MenuCode.Equals(exist.MenuCode))
                        {
                            return "data is being ordered";
                        }
                        else
                        {
                            Manager.Database.Menus.Remove(exist);
                            Manager.Database.SaveChanges();
                            break;
                        }
                        
                    }
                }
                transac.Complete();

                return "Data Deleted";
            }
        }
    }
}
