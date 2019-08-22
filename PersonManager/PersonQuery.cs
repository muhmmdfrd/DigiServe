using Repository1;
using System.Collections.Generic;
using System.Linq;

namespace Core.Manager.PersonManager
{
    public class PersonQuery : AsistanceBase<PersonAdapter, Role>
    {
        public PersonQuery(PersonAdapter manager) : base(manager)
        {
            // code here
        }

        public IQueryable<Person> All()
        {
            return Manager.Database.People;
        }

        public IQueryable<PersonForDetail> QueryablePerson(long id)
        {
            return (All().Where(x => x.PersonId == id).Select(x => new PersonForDetail() { Name = x.Name })).AsQueryable();
        }

        public List<PersonDTO> Transform()
        {
            return 
                (from transform in All()
                select new PersonDTO()
                {
                    PersonId = transform.PersonId,
                    Name = transform.Name,
                    Gender = transform.Gender,
                    DateOfBirth = transform.DateOfBirth,
                    Address = transform.Address,
                    CreatedBy = transform.CreatedBy,
                    CreatedDate = transform.CreatedDate,
                    UpdatedBy = transform.UpdatedBy,
                    UpdatedDate = transform.UpdatedDate
                }).ToList();
        }

        public PersonDTO TransformSingle(long id)
        {
            return 
                (from transform in All()
                 where transform.PersonId == id
                 select new PersonDTO()
                 {
                    PersonId = transform.PersonId,
                    Name = transform.Name,
                    Gender = transform.Gender,
                    DateOfBirth = transform.DateOfBirth,
                    Address = transform.Address,
                    CreatedBy = transform.CreatedBy,
                    CreatedDate = transform.CreatedDate,
                    UpdatedBy = transform.UpdatedBy,
                    UpdatedDate = transform.UpdatedDate
                 }).FirstOrDefault();
        }
    }
}
