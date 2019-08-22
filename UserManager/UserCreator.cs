using Core.Manager.PersonManager;
using Repository1;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;

namespace Core.Manager.UserManager
{
    public class UserCreator : AsistanceBase<UserAdapter, User>
    {
        public UserCreator(UserAdapter manager) : base(manager)
        {
            // code here
        }

        public User Save(PersonUserDTO dto, long userId)
        {
            using (var transac = new TransactionScope())
            {
                var date = DateTime.UtcNow;

                var newPerson = new Person
                {
                    Name = dto.Name,
                    Gender = dto.Gender,
                    DateOfBirth = dto.DateOfBirth,
                    Address = dto.Address,
                    CreatedBy = (int)userId,
                    CreatedDate = date,
                    UpdatedBy = (int)userId,
                    UpdatedDate = date
                };

                string passHashed = Encrypt(dto.Password);

                var newEntity = new User
                {
                    Username = dto.Username,
                    Password = passHashed,
                    IMEI = dto.IMEI,
                    Status = (int)dto.Status,
                    RoleId = dto.RoleId,
                    CreatedBy = (int)userId,
                    CreatedDate = date,
                    UpdatedBy = (int)userId,
                    UpdatedDate = date
                };

                newPerson.Users.Add(newEntity);
                Manager.Database.People.Add(newPerson);
                Manager.Database.SaveChanges();
                transac.Complete();

                return newEntity;
            }
        }

        public string Encrypt(string pass)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                return GetMd5Hash(md5Hash, pass);
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
