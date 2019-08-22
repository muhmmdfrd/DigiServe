using Repository1;
using System;
using System.Transactions;

namespace Core.Manager.MenuManager
{
    public class MenuCreator : AsistanceBase<MenuAdapter, Menu>
    {
        public MenuCreator(MenuAdapter manager) : base(manager)
        {
            // code here
        }

        public Menu Save(MenuDTO dto, long userId)
        {
            using (var transac = new TransactionScope())
            {
                var date = DateTime.UtcNow;

                var newEntity = new Menu
                {
                    MenuCode = Guid.NewGuid().ToString(),
                    MenuType = dto.MenuType,
                    Name = dto.Name,
                    Address = dto.Address,
                    CreatedBy = (int)userId,
                    CreateDate = date,
                    UpdatedBy = (int)userId,
                    UpdatedDate = date,
                    Price = dto.Price
                };

                Manager.Database.Menus.Add(newEntity);
                Manager.Database.SaveChanges();

                transac.Complete();

                return newEntity;
            }

        }
    }
}
