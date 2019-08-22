using Repository1;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.PersonManager
{
    public class PersonUpdater : AsistanceBase<PersonAdapter, Person>
    {
        public PersonUpdater(PersonAdapter manager) : base(manager)
        {
            // code here
        }

        public Person Update(PersonDTO dto, long userId)
        {
            using (var transac = new TransactionScope())
            {
                var date = DateTime.UtcNow;

                var exist = Manager.Query.Value.All().FirstOrDefault(s => s.PersonId == dto.PersonId);
                if (exist == null)
                {
                    throw new Exception("You are don't exist");
                }
                else
                {
                    exist.PersonId = dto.PersonId;
                    exist.Name = dto.Name;
                    exist.Gender = dto.Gender;
                    exist.DateOfBirth = dto.DateOfBirth;
                    exist.Address = dto.Address;
                    exist.UpdatedBy = (int)userId;
                    exist.UpdatedDate = date;
                    Manager.Database.SaveChanges();
                }

                transac.Complete();

                return exist;
            }
        }


    }
}
