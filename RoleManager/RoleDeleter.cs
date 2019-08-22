using Repository1;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.RoleManager
{
    public class RoleDeleter : AsistanceBase<RoleManager, Role>
    {
        public RoleDeleter(RoleManager manager) : base(manager)
        {
            // code here
        }

        public string Delete(long id)
        {
            using (var transac = new TransactionScope())
            {
                var exist = Manager.Query.Value.Get().FirstOrDefault(a => a.RoleId == id);
                if (exist == null)
                {
                    throw new Exception("Menu doesn't exist");
                }
                else
                {
                    Manager.Database.Roles.Remove(exist);
                    Manager.Database.SaveChanges();
                }

                transac.Complete();

                return "Data Deleted";
            }
        }
    }
}
