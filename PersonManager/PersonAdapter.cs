using System;
using Repository1;

namespace Core.Manager.PersonManager
{
    public class PersonAdapter : ManagerBase<TutorialEntities>
    {
        public Lazy<PersonCreator> Creator { get; internal set; }
        public Lazy<PersonQuery> Query { get; internal set; }
        public Lazy<PersonDeleter> Deleter { get; internal set; }
        public Lazy<PersonUpdater> Updater { get; internal set; }

        public PersonAdapter() : base()
        {
            Assistance(base.Database);
        }

        public PersonAdapter(TutorialEntities dbContext) : base(dbContext)
        {
            Assistance(dbContext);
        }

        protected override void Assistance(TutorialEntities dbContext)
        {
            Creator = new Lazy<PersonCreator>(() => { return new PersonCreator(this); }, true);
            Deleter = new Lazy<PersonDeleter>(() => { return new PersonDeleter(this); }, true);
            Updater = new Lazy<PersonUpdater>(() => { return new PersonUpdater(this); }, true);
            Query = new Lazy<PersonQuery>(() => { return new PersonQuery(this); }, true);
        }
    }
}
