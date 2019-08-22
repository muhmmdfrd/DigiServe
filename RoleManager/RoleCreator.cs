using Repository1;
using System;
using System.Transactions;

namespace Core.Manager.RoleManager
{
    public class RoleCreator : AsistanceBase<RoleManager, Role>
    {
        public RoleCreator(RoleManager manager) : base(manager)
        {
            // code here
        }

        public Role Save(RoleDTO dto, long userId)
        {
            using (var transac = new TransactionScope())
            {
                var date = DateTime.UtcNow;

                var newEntity = new Role
                {
                    Name = dto.Name,
                    Status = dto.Status,
                    CreatedBy = (int)userId,
                    CreatedDate = date,
                    UpdatedBy = (int)userId,
                    UpdatedDate = date
                };

                var isName = newEntity.Name;

                Manager.Database.Roles.Add(newEntity);
                Manager.Database.SaveChanges();
                transac.Complete();

                return newEntity;
            }
        }
    }
}
