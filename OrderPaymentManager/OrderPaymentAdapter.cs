using Repository1;
using System;

namespace Core.Manager.OrderPaymentManager
{
    public class OrderPaymentAdapter : ManagerBase<TutorialEntities>
    {
        public Lazy<OrderPaymentCreator> Creator { get; set; }
        public Lazy<OrderPaymentQuery> Query { get; set; }
        public Lazy<OrderPaymentDeleter> Deleter { get; set; }
        public Lazy<OrderPaymentUpdater> Updater { get; set; }
        public Lazy<OrderDetailAdapter> OrderDetailManager { get; set; }

        public OrderPaymentAdapter() : base()
        {
            Assistance(base.Database);
        }

        public OrderPaymentAdapter(TutorialEntities dbContext) : base(dbContext)
        {
            Assistance(dbContext);
        }

        protected override void Assistance(TutorialEntities dbContext)
        {
            Creator = new Lazy<OrderPaymentCreator>(() =>
            {
                return new OrderPaymentCreator(this);
            }, true);

            Deleter = new Lazy<OrderPaymentDeleter>(() =>
            {
                return new OrderPaymentDeleter(this);
            }, true);

            Updater = new Lazy<OrderPaymentUpdater>(() =>
            {
                return new OrderPaymentUpdater(this);
            }, true);

            Query = new Lazy<OrderPaymentQuery>(() =>
            {
                return new OrderPaymentQuery(this);
            }, true);

            OrderDetailManager = new Lazy<OrderDetailAdapter>(() =>
            {
                return new OrderDetailAdapter(this.Database);
            }, true);
        }
    }
}
