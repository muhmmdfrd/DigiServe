using System.Collections.Generic;
using System.Linq;
using Repository1;

namespace Core.Manager.RoleManager
{
    public class RoleQuery : AsistanceBase<RoleManager, Role>
    {
        public RoleQuery(RoleManager manager) : base(manager)
        {
            // code here
        }

        public IQueryable<Role> Get()
        {
            return Manager.Database.Roles;
        }

        public List<RoleDTO> Transform()
        {
            return (
                from tr in Get()
                select new RoleDTO()
                {
                    RoleId = tr.RoleId,
                    Name = tr.Name,
                    Status = tr.Status,
                    CreatedBy = tr.CreatedBy,
                    CreatedDate = tr.CreatedDate,
                    UpdatedBy = tr.UpdatedBy,
                    UpdatedDate = tr.UpdatedDate
                }).ToList();
        }

        public RoleDTO TransformSingle(long id)
        {
            return (from tr in Get()
                    where tr.RoleId == id
                    select new RoleDTO()
                    {
                        RoleId = tr.RoleId,
                        Name = tr.Name,
                        Status = tr.Status,
                        CreatedBy = tr.CreatedBy,
                        CreatedDate = tr.CreatedDate,
                        UpdatedBy = tr.UpdatedBy,
                        UpdatedDate = tr.UpdatedDate
                    }).FirstOrDefault();
        }
    }
}
