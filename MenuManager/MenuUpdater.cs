using Repository1;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.MenuManager
{
    public class MenuUpdater : AsistanceBase<MenuAdapter, Menu>
    {
        public MenuUpdater(MenuAdapter manager) : base(manager)
        {
            // code here
        }

        public Menu Update(MenuDTO dto, long userId)
        {
            using (var transac = new TransactionScope())
            {
                var date = DateTime.UtcNow;

                var exist = Manager.Query.Value.Get().FirstOrDefault(s => s.MenuId == dto.MenuId);
                if (exist == null)
                {
                    throw new Exception("Your menu doesn't exist");
                }
                else
                {
                    if (exist.Price != dto.Price)
                    {
                        var newMenuHistory = new MenuHistory
                        {
                            MenuCode = exist.MenuCode,
                            MenuType = exist.MenuType,
                            Name = exist.Name,
                            Address = exist.Address,
                            CreatedBy = userId,
                            CreatedDate = date,
                            UpdatedBy = userId,
                            UpdatedDate = date,
                            Price = exist.Price
                        };
                        Manager.Database.MenuHistories.Add(newMenuHistory);
                    }

                    exist.MenuCode = dto.MenuCode;
                    exist.MenuType = dto.MenuType;
                    exist.Name = dto.Name;
                    exist.Address = dto.Address;
                    exist.UpdatedBy = (int)userId;
                    exist.UpdatedDate = date;
                    exist.Price = dto.Price;

                    Manager.Database.SaveChanges();
                }

                transac.Complete();

                return exist;
            }
        }
    }
}
