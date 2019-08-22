using Repository1;
using System;

namespace Core.Manager.OrderManager
{
    public class OrderAdapter :  ManagerBase<TutorialEntities>
    {
        public Lazy<OrderCreator> Creator { get; set; }
        public Lazy<OrderQuery> Query { get; set; }
        public Lazy<OrderDeleter> Deleter { get; set; }
        public Lazy<OrderUpdater> Updater { get; set; }
        public Lazy<OrderDetailAdapter> OrderDetailManager { get; set; }

        public OrderAdapter() : base()
        {
            Assistance(base.Database);
        }

        public OrderAdapter(TutorialEntities dbContext) : base(dbContext)
        {
            Assistance(dbContext);
        }

        protected override void Assistance(TutorialEntities dbContext)
        {
            Creator = new Lazy<OrderCreator>(() =>
            {
                return new OrderCreator(this);
            }, true);

            Deleter = new Lazy<OrderDeleter>(() =>
            {
                return new OrderDeleter(this);
            }, true);

            Updater = new Lazy<OrderUpdater>(() =>
            {
                return new OrderUpdater(this);
            }, true);

            Query = new Lazy<OrderQuery>(() =>
            {
                return new OrderQuery(this);
            }, true);

            OrderDetailManager = new Lazy<OrderDetailAdapter>(() =>
            {
                return new OrderDetailAdapter(this.Database);
            }, true);
        }
    }
}
