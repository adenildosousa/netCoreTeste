using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WAGym.Common.Model.Login.Response;
using WAGym.Domain.Manager.Interface;

namespace WAGym.Domain.Manager
{
    public class SessionManager : ISessionManager
    {
        private readonly IHttpContextAccessor _context;

        public SessionManager(IHttpContextAccessor context)
        {
            _context = context;
        }

        public void SetObject<TEntity>(string key, TEntity value) where TEntity : class
        {
            string objectString = JsonConvert.SerializeObject(value);
            _context.HttpContext.Session.SetString(key, objectString);
        }

        public TEntity? GetObject<TEntity>(string key) where TEntity : class
        {
            string result = _context.HttpContext.Session.GetString(key);

            if (result == null)
                return null;
            
            return JsonConvert.DeserializeObject<TEntity>(result);
        }

        public LoginResponse? GetLoggedUserSessionInfo(string key)
        {
            LoginResponse? loggedUserInfo = GetObject<LoginResponse>(key);
            return loggedUserInfo;
        }
    
        public void Kill(string key)
        {
            _context.HttpContext.Session.Remove(key);
        }
    }
}
