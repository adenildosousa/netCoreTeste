using WAGym.Common.Model.Login.Response;

namespace WAGym.Domain.Manager.Interface
{
    public interface ISessionManager : IManager
    {
        void SetObject<TEntity>(string key, TEntity value) where TEntity : class;
        TEntity? GetObject<TEntity>(string key) where TEntity : class;
        LoginResponse? GetLoggedUserSessionInfo(string key);
        void Kill(string key);
    }
}
