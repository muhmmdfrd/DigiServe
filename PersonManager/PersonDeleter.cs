using Repository1;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.PersonManager
{
    public class PersonDeleter : AsistanceBase<PersonAdapter, Person>
    {
        public PersonDeleter(PersonAdapter manager) : base(manager)
        {
            // code here
        }

        public string Delete(long id)
        {
            using (var transac = new TransactionScope())
            {
                var exist = Manager.Query.Value.All().FirstOrDefault(a => a.PersonId == id);
                if (exist == null)
                {
                    throw new Exception("Person doesn't exist");
                }
                else
                {
                    Manager.Database.People.Remove(exist);
                    Manager.Database.SaveChanges();
                }

                transac.Complete();

                return "Data Deleted";
            }
        }
    }
}
