using Repository1;
using System;
using System.Linq; 

namespace Core.Manager.LoginManager
{
    public class LoginAuth : AsistanceBase<LoginManager, User>
    {
        public LoginAuth(LoginManager manager) : base(manager)
        {
            // code here
        }

        public User AuthUserPass(string userName, AuthRequest dto)
        {
           return Manager.Database.Users.FirstOrDefault(s => s.Username == dto.Username && s.Password == dto.Password);
        }

        public User AuthUser(string imei)
        {

            if (string.IsNullOrEmpty(imei))
            {
                throw new Exception("IMEI is empty");
            }

            var user = Manager.Database.Users.FirstOrDefault(s => s.IMEI == imei);

            if (user == null)
            {
                throw new Exception("IMEI not found");
            }

            return user;
        }
    }
}
