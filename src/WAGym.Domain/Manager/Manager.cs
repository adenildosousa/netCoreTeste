using WAGym.Common.Model.Login.Response;
using WAGym.Domain.Manager.Interface;

namespace WAGym.Domain.Manager
{
    public abstract class Manager : IManager
    {
        const string LOGGED_USER_SESSION = "LoggedUserInfo";
        protected LoginResponse? LoggedUser { get; set; }

        public Manager(ISessionManager sessionManager)
        {
            LoggedUser = sessionManager.GetLoggedUserSessionInfo(LOGGED_USER_SESSION);
        }
    }
}
