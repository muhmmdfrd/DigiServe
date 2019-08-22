using Repository1;
using System;

namespace Core.Manager.RoleManager
{
    public class RoleManager : ManagerBase<TutorialEntities>
    {
        public Lazy<RoleCreator> Creator { get; internal set; }
        public Lazy<RoleQuery> Query { get; internal set; }
        public Lazy<RoleDeleter> Deleter { get; internal set; }
        public Lazy<RoleUpdater> Updater { get; internal set; }

        public RoleManager() : base()
        {
            Assistance(base.Database);
        }

        public RoleManager(TutorialEntities dbContext) : base(dbContext)
        {
            Assistance(dbContext);
        }

        protected override void Assistance(TutorialEntities dbContext)
        {
            Creator = new Lazy<RoleCreator>(() =>
            {return new RoleCreator(this);
            }, true);
            Deleter = new Lazy<RoleDeleter>(() =>
            { return new RoleDeleter(this);
            }, true);
            Updater = new Lazy<RoleUpdater>(() =>
            { return new RoleUpdater(this);
            }, true);
            Query = new Lazy<RoleQuery>(() => { return new RoleQuery(this); }, true);
        }
    }
}
