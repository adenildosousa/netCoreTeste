using AutoMapper;
using WAGym.Common.Model.ProfileUser.Request;

namespace WAGym.Domain.AutoMapper
{
    public class ProfileUserMapping : Profile
    {
        public ProfileUserMapping()
        {
            CreateMap<ProfileUserRequest, Data.Data.ProfileUser>();
        }
    }
}
