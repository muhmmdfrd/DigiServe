using Repository1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Manager.UserManager
{
    public class UserLogOut : AsistanceBase<UserAdapter, User>
    {
        public UserLogOut(UserAdapter manager) : base(manager)
        {
            // code here
        }

        public string Logout(long id)
        {
            using (var transac = new TransactionScope())
            {
                var exist = Manager.Query.Value.All().FirstOrDefault(scope => scope.UserId == id);
                if (exist == null)
                {
                    throw new Exception("You are don't exist");
                }
                else
                {
                    exist.Token = null;
                    Manager.Database.SaveChanges();
                }

                transac.Complete();

                return "logout";
            }
        }
    }
}
