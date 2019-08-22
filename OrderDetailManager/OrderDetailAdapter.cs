using Core.Manager.MenuManager;
using Core.Manager.OrderManager;
using Core.Manager.OrderPaymentManager;
using Core.Manager.PersonManager;
using Core.Manager.UserManager;
using Repository1;
using System;

namespace Core.Manager
{
    public class OrderDetailAdapter : ManagerBase<TutorialEntities>
    {
        public Lazy<OrderDetailCreator> Creator { get; set; }
        public Lazy<OrderDetailQuery> Query { get; set; }
        public Lazy<OrderDetailDeleter> Deleter { get; set; }
        public Lazy<OrderDetailUpdater> Updater { get; set; }
        public Lazy<MenuAdapter> MenuManager { get; set; }
        public Lazy<PersonAdapter> PersonManager { get; set; }
        public Lazy<UserAdapter> UserManager { get; set; }
        public Lazy<OrderAdapter> OrderManager { get; set; }
        public Lazy<OrderPaymentAdapter> OrderPaymentManager { get; set; }

        public OrderDetailAdapter() : base()
        {
            Assistance(base.Database);
        }

        public OrderDetailAdapter(TutorialEntities dbContext) : base(dbContext)
        {
            Assistance(dbContext);
        }

        protected override void Assistance(TutorialEntities dbContext)
        {
            Creator = new Lazy<OrderDetailCreator>(() => { return new OrderDetailCreator(this); }, true);
            Deleter = new Lazy<OrderDetailDeleter>(() => { return new OrderDetailDeleter(this); }, true);
            Updater = new Lazy<OrderDetailUpdater>(() => { return new OrderDetailUpdater(this); }, true);
            Query = new Lazy<OrderDetailQuery>(() => { return new OrderDetailQuery(this); }, true);
            MenuManager = new Lazy<MenuAdapter>(() => { return new MenuAdapter(this.Database); }, true);
            PersonManager = new Lazy<PersonAdapter>(() => { return new PersonAdapter(this.Database); }, true);
            UserManager = new Lazy<UserAdapter>(() => { return new UserAdapter(this.Database); }, true);
            OrderManager = new Lazy<OrderAdapter>(() => { return new OrderAdapter(this.Database); }, true);
            OrderPaymentManager = new Lazy<OrderPaymentAdapter>(() => { return new OrderPaymentAdapter(this.Database); }, true);

        }
    }
}
