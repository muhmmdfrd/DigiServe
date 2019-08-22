using Repository1;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.RoleManager
{
    public class RoleUpdater : AsistanceBase<RoleManager, Role>
    {
        public RoleUpdater(RoleManager manager) : base(manager)
        {
            // code here
        }

        public Role Update(RoleDTO dto, long userId)
        {
            using (var transac = new TransactionScope())
            {
                var date = DateTime.UtcNow;

                var exist = Manager.Query.Value.Get().FirstOrDefault(s => s.RoleId == dto.RoleId);
                if (exist == null)
                {
                    throw new Exception("Your Role doesn't exist");
                }
                else
                {
                    exist.RoleId = dto.RoleId;
                    exist.Name = dto.Name;
                    exist.Status = dto.Status;
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
