using Repository1;
using System;
using System.Transactions;

namespace Core.Manager.PersonManager
{
    public class PersonCreator : AsistanceBase<PersonAdapter, Person>
    {
        public PersonCreator(PersonAdapter manager) : base(manager)
        {
            // code here
        }

        public Person Save(PersonDTO dto, long userId)
        {
            using (var transac = new TransactionScope())
            {
                var date = DateTime.UtcNow;

                var newEntity = new Person
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

                Manager.Database.People.Add(newEntity);
                Manager.Database.SaveChanges();
                transac.Complete();

                return newEntity;
            }
        }
    }
}
