using WAGym.Common.Enum;
using WAGym.Common.Model.Login.Request;
using WAGym.Common.Model.Login.Response;
using WAGym.Data.Data;

namespace WAGym.Domain.Manager.Interface
{
    public interface ILoginManager : IManager
    {
        Task<LoginResponse> Authenticate(LoginRequest loginRequest);
        LoginResponse RefreshToken();
        LoginResponse RevokeToken();
        Task<LoginProfileResponse> ProfileHasFunctionality(FunctionalityEnum functionality);
    }
}
