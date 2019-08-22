using Repository1;
using System;

namespace Core.Manager.LoginManager
{
    public class LoginManager : ManagerBase<TutorialEntities>
    {
        public Lazy<LoginAuth> Login { get; internal set; }

        public LoginManager() : base()
        {
            Assistance(base.Database);
        }

        public LoginManager(TutorialEntities dbContext) : base(dbContext)
        {
            Assistance(dbContext);
        }

        protected override void Assistance(TutorialEntities dbContext)
        {
            Login = new Lazy<LoginAuth>(() => { return new LoginAuth(this); }, true);
        }
    }
}
