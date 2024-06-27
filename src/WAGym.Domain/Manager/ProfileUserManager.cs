using AutoMapper;
using FluentValidation;
using System.Net;
using WAGym.Common.Enum;
using WAGym.Common.Exception;
using WAGym.Common.Model.Base.Response;
using WAGym.Common.Model.Profile.Request;
using WAGym.Common.Model.ProfileUser.Request;
using WAGym.Common.Resource;
using WAGym.Data.Data;
using WAGym.Data.Repository.Interface;
using WAGym.Domain.Manager.Interface;

namespace WAGym.Domain.Manager
{
    public class ProfileUserManager : Manager, IProfileUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IProfileUserRepository _profileUserRepository;
        private readonly IMapper _mapper;

        public ProfileUserManager(ISessionManager sessionManager,
                                  IUserRepository userRepository,
                                  IProfileRepository profileRepository,
                                  IProfileUserRepository profileUserRepository,
                                  IMapper mapper)
            : base(sessionManager)
        {
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _profileUserRepository = profileUserRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Create(ProfileUserRequest request)
        {
            if (request == null)
                throw new BusinessException(Resource.InvalidRequest);
            
            await ValidateUserAndProfiles(request);

            _profileUserRepository.AddRange(GetProfileUserList(request));

            if (!await _profileUserRepository.SaveAsync())
                throw new BusinessException(Resource.ErrorSaving);

            return new BaseResponse(HttpStatusCode.OK);
        }

        private IEnumerable<ProfileUser> GetProfileUserList(ProfileUserRequest request)
        {
            List<ProfileUser> profileUsers = new List<ProfileUser>();

            foreach (ProfileRequest profile in request.Profiles)
            {
                ProfileUser profileUser = _mapper.Map<ProfileUser>(request);
                profileUser.ProfileId = profile.Id;
                profileUser.Default = profile.Default;
                profileUser.UserUpdateId = LoggedUser!.UserId;

                profileUsers.Add(profileUser);
            }

            return profileUsers;
        }

        private async Task ValidateUserAndProfiles(ProfileUserRequest request)
        {
            bool userExists = await _userRepository.AnyAsync(x =>
                                           x.CompanyId == request.CompanyId
                                        && x.PersonId == request.PersonId
                                        && x.Id == request.UserId);

            if (!userExists)
                throw new BusinessException(Resource.UserNotFound);

            if (!request.Profiles.Any())
                throw new BusinessException(Resource.ProfilesReportedDoesNotExists);

            bool profileExists = await _profileRepository.AnyAsync(x =>
                   request.Profiles.Select(x => x.Id).ToList().Contains(x.Id)
                && x.StatusId == (int)StatusEnum.Active);

            if (!profileExists)
                throw new BusinessException(Resource.OnlyExistingProfilesAreAllowed);

            if (request.Profiles.Where(x => x.Default).Count() != 1)
                throw new BusinessException(Resource.OnlyOneProfileCanBeDefault);
        }
    }
}
