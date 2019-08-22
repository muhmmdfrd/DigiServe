using Core.Manager.PersonManager;
using Repository1;
using System;

namespace Core.Manager.UserManager
{
    public class UserAdapter : ManagerBase<TutorialEntities>
    {
        public Lazy<UserCreator> Creator { get; internal set; }
        public Lazy<UserQuery> Query { get; internal set; }
        public Lazy<UserDeleter> Deleter { get; internal set; }
        public Lazy<UserUpdater> Updater { get; internal set; }
        public Lazy<UserLogOut> Logout { get; set; }
        public Lazy<EncryptString> Encrypt { get; set; }
        public Lazy<PersonAdapter> PersonManager { get; set; }

        public UserAdapter() : base()
        {
            Assistance(base.Database);
        }

        public UserAdapter(TutorialEntities dbContext) : base(dbContext)
        {
            Assistance(dbContext);
        }

        protected override void Assistance(TutorialEntities dbContext)
        {
            Creator = new Lazy<UserCreator>(() => { return new UserCreator(this); }, true);
            Deleter = new Lazy<UserDeleter>(() => { return new UserDeleter(this); }, true);
            Updater = new Lazy<UserUpdater>(() => { return new UserUpdater(this); }, true);
            Query = new Lazy<UserQuery>(() => { return new UserQuery(this); }, true);
            Logout = new Lazy<UserLogOut>(() => { return new UserLogOut(this); }, true);
            Encrypt = new Lazy<EncryptString>(() => { return new EncryptString(this); }, true);
            PersonManager = new Lazy<PersonAdapter>(() => { return new PersonAdapter(this.Database); }, true);
        }
    }
}
