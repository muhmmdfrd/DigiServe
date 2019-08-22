using Repository1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Core.Manager.UserManager
{
    public class UserDeleter : AsistanceBase<UserAdapter, User>
    {
        public UserDeleter(UserAdapter manager) : base(manager)
        {
            // code here
        }

        public string Delete(long id)
        {
            var exist = Manager.Query.Value.All().FirstOrDefault(a => a.UserId == id);
              
            if (exist == null)
            {
                throw new Exception("user doesn't exist");
            }
            else
            {
                Manager.Database.Users.Remove(exist);
                Manager.Database.SaveChanges();
            }

            return "deleted";
        }
    }
}
