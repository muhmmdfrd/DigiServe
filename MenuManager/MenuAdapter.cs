using Repository1;
using System;

namespace Core.Manager.MenuManager
{
    public class MenuAdapter : ManagerBase<TutorialEntities>
    {
        public Lazy<MenuCreator> Creator { get; internal set; }
        public Lazy<MenuDeleter> Deleter { get; internal set; }
        public Lazy<MenuQuery> Query { get; internal set; }
        public Lazy<MenuUpdater> Updater { get; internal set; }
        public Lazy<OrderDetailAdapter> OrderDetailManager { get; set; }
        public MenuAdapter() : base()
        {
            Assistance(base.Database);
        }

        public MenuAdapter(TutorialEntities dbContext) : base(dbContext)
        {
            Assistance(dbContext);
        }

        protected override void Assistance(TutorialEntities dbContext)
        {
            Creator = new Lazy<MenuCreator>(() => { return new MenuCreator(this); }, true);
            Deleter = new Lazy<MenuDeleter>(() => { return new MenuDeleter(this); }, true);
            Updater = new Lazy<MenuUpdater>(() => { return new MenuUpdater(this); }, true);
            Query = new Lazy<MenuQuery>(() => { return new MenuQuery(this); }, true);
            OrderDetailManager = new Lazy<OrderDetailAdapter>(() => { return new OrderDetailAdapter(this.Database); }, true);
        }
    }


}
