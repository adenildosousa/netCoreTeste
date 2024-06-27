using AutoMapper;
using WAGym.Common.Enum;
using WAGym.Common.Model.User.Request;
using WAGym.Common.Model.User.Response;
using WAGym.Domain.Extension;

namespace WAGym.Domain.AutoMapper
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserRequest, Data.Data.User>()
                .ForMember(src => src.StatusId, opt => opt.MapFrom(src => (int)StatusEnum.Active));

            CreateMap<PutUserRequest, Data.Data.User>()
                .ForMember(src => src.StatusId, opt => opt.MapFrom(src => (int)src.Status));

            CreateMap<Data.Data.User, UserResponse>()
                .ForMember(src => src.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(src => src.Status, opt => opt.MapFrom(src => ((StatusEnum)src.StatusId).GetName()))
                .ForMember(src => src.UserUpdate, opt => opt.MapFrom(src => src.UserUpdate.Username))
                .ForMember(src => src.UserUpdateId, opt => opt.MapFrom(src => src.UserUpdateId));
        }
    }
}