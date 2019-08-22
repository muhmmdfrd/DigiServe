using Core.Manager.PersonManager;
using Repository1;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.UserManager
{
    public class UserUpdater : AsistanceBase<UserAdapter, User>
    {
        public UserUpdater(UserAdapter manager) : base(manager)
        {
            // code here
        }

        public string UpdateToken(string token, long id)
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
                    exist.Token = token;
                    Manager.Database.SaveChanges();
                }

                transac.Complete();

                return exist.Token;
            }
        }

        public User Update(PersonUserDTO dto, long userId)
        {
            using (var transac = new TransactionScope())
            {
                var exist = Manager.Query.Value.All().FirstOrDefault(scope => scope.UserId == dto.UserId);
                var person = Manager.PersonManager.Value.Query.Value.All().FirstOrDefault(scope => scope.PersonId == exist.PersonId);

                if (exist == null)
                {
                    throw new Exception("You are don't exist");
                }
                else
                {
                    string hashedPass;
                    if (dto.Password == null || string.IsNullOrEmpty(dto.Password))
                    {
                        hashedPass = exist.Password;
                    }
                    else
                    {
                        hashedPass = Manager.Encrypt.Value.Encrypt(dto.Password);
                    }
                  
                    // update user
                    exist.Username = dto.Username;
                    exist.Password = hashedPass;
                    exist.IMEI = dto.IMEI;
                    exist.Status = (int)dto.Status;
                    exist.RoleId = dto.RoleId;
                    exist.UpdatedBy = (int)userId;
                    exist.UpdatedDate = DateTime.UtcNow;

                    // update person
                    person.Name = dto.Name;
                    person.Address = dto.Address;
                    person.DateOfBirth = dto.DateOfBirth;
                    person.Gender = dto.Gender;
                    person.UpdatedBy = (int)userId;
                    person.UpdatedDate = DateTime.UtcNow;
                    Manager.Database.SaveChanges();
                    Manager.PersonManager.Value.Database.SaveChanges();
                }

                transac.Complete();

                return exist;
            }
        }

        public User UpdatePass(string oldPass, string newPass, long userId)
        {
            var exist = Manager.Query.Value.All().FirstOrDefault(scope => scope.UserId == userId);
            var hashedOldPass = Manager.Encrypt.Value.Encrypt(oldPass);

            if (exist.Password.Equals(hashedOldPass))
            {
                exist.Password = Manager.Encrypt.Value.Encrypt(newPass);
                exist.UpdatedDate = DateTime.Now;
                exist.UpdatedBy = (int)userId;
                Manager.Database.SaveChanges();
            }
            else
            {
                throw new Exception("your password is doesn't match");
            }
            return exist;
        }
    }
}
