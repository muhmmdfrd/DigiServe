using Repository1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Manager.MenuHistoryManager
{
    public class MenuHistoryAdapter: ManagerBase<TutorialEntities>
    {
        public Lazy<MenuHistoryCreator> Creator { get; set; }
        public Lazy<MenuHistoryQuery> Query { get; set; }
        public Lazy<MenuHistoryDeleter> Deleter { get; set; }
        public Lazy<MenuHistoryUpdater> Updater { get; set; }

        public MenuHistoryAdapter() : base()
        {
            Assistance(base.Database);
        }

        public MenuHistoryAdapter(TutorialEntities dbContext) : base(dbContext)
        {
            Assistance(dbContext);
        }

        protected override void Assistance(TutorialEntities dbContext)
        {
            Creator = new Lazy<MenuHistoryCreator>(() => { return new MenuHistoryCreator(this); }, true);
            Deleter = new Lazy<MenuHistoryDeleter> (() => { return new MenuHistoryDeleter(this); }, true);
            Updater = new Lazy<MenuHistoryUpdater>(() => { return new MenuHistoryUpdater(this); }, true);
            Query = new Lazy<MenuHistoryQuery>(() => { return new MenuHistoryQuery(this); }, true);
        }
    }
}
