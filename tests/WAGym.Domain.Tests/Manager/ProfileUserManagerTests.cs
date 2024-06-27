using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Linq.Expressions;
using System.Net;
using WAGym.Common.Exception;
using WAGym.Common.Model.Base.Response;
using WAGym.Common.Model.Login.Response;
using WAGym.Common.Model.Profile.Request;
using WAGym.Common.Model.Profile.Response;
using WAGym.Common.Model.ProfileUser.Request;
using WAGym.Common.Model.Token;
using WAGym.Data.Data;
using WAGym.Data.Repository.Interface;
using WAGym.Domain.AutoMapper;
using WAGym.Domain.Manager;
using WAGym.Domain.Manager.Interface;

namespace WAGym.Domain.Tests.Manager
{
    [TestClass]
    public class ProfileUserManagerTests
    {
        private ProfileUserManager _profileUserManager;
        private ProfileUserRequest? _profileUserRequest;
        private LoginResponse _loginResponse;
        private ProfileUser _profileUserEntity;
        private ISessionManager _sessionManager;
        private IUserRepository _userRepository;
        private IProfileRepository _profileRepository;
        private IProfileUserRepository _profileUserRepository;
        private IMapper _mapper;
        private long _expectedCompanyId;
        private long _expectedPersonId;
        private long _expectedUserId;

        [TestInitialize]
        public void Setup()
        {
            _expectedCompanyId = 1;
            _expectedPersonId = 1;
            _expectedUserId = 1;

            _sessionManager = Substitute.For<ISessionManager>();
            _userRepository = Substitute.For<IUserRepository>();
            _profileRepository = Substitute.For<IProfileRepository>();
            _profileUserRepository = Substitute.For<IProfileUserRepository>();

            Token token = new Token(true, DateTime.Now, DateTime.Now.AddMonths(1), "Access Token", "Refresh Token");
            _loginResponse = new LoginResponse(_expectedCompanyId, _expectedPersonId, _expectedUserId, token, new ProfileResponse());
            _sessionManager.GetLoggedUserSessionInfo(Arg.Any<string>()).Returns(_loginResponse);

            IConfigurationProvider configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProfileUserMapping>();
            });
            _mapper = configuration.CreateMapper();

            _profileUserManager = new ProfileUserManager(
                _sessionManager,
                _userRepository,
                _profileRepository,
                _profileUserRepository,
                _mapper);

            _profileUserRequest = new ProfileUserRequest
            {
                CompanyId = _expectedCompanyId,
                PersonId = _expectedPersonId,
                UserId = _expectedUserId
            };
            _profileUserEntity = new ProfileUser();
        }

        [TestMethod]
        public async Task Create_WithInvalidRequest_ShouldThrowBusinessException()
        {
            _profileUserRequest = null;

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _profileUserManager.Create(_profileUserRequest!));
        }

        [TestMethod]
        public async Task Create_WithValidRequest_AndNonExistingUser_ShouldThrowBusinessException()
        {
            _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(false);

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _profileUserManager.Create(_profileUserRequest!));
            await _userRepository.Received(1).AnyAsync(Arg.Any<Expression<Func<User, bool>>>());
        }

        [TestMethod]
        public async Task Create_WithValidRequest_AndNonExistingProfiles_ShouldThrowBusinessException()
        {
            _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(true);

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _profileUserManager.Create(_profileUserRequest!));
            await _userRepository.Received(1).AnyAsync(Arg.Any<Expression<Func<User, bool>>>());
        }

        [TestMethod]
        public async Task Create_WithValidRequest_AndProfilesDoesNotMatchDatabase_ShouldThrowBusinessException()
        {
            _profileUserRequest!.Profiles.Add(new ProfileRequest { Id = 1, Default = true });
            _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(true);
            _profileRepository.AnyAsync(Arg.Any<Expression<Func<Data.Data.Profile, bool>>>()).Returns(false);

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _profileUserManager.Create(_profileUserRequest!));
            await _userRepository.Received(1).AnyAsync(Arg.Any<Expression<Func<User, bool>>>());
            await _profileRepository.Received(1).AnyAsync(Arg.Any<Expression<Func<Data.Data.Profile, bool>>>());
        }

        [TestMethod]
        public async Task Create_WithValidRequest_AndProfilesHasMultipleDefault_ShouldThrowBusinessException()
        {
            _profileUserRequest!.Profiles.AddRange(new List<ProfileRequest>()
            {
                new ProfileRequest { Id = 1, Default = true },
                new ProfileRequest { Id = 2, Default = true }
            });
            _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(true);
            _profileRepository.AnyAsync(Arg.Any<Expression<Func<Data.Data.Profile, bool>>>()).Returns(true);

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _profileUserManager.Create(_profileUserRequest!));
            await _userRepository.Received(1).AnyAsync(Arg.Any<Expression<Func<User, bool>>>());
            await _profileRepository.Received(1).AnyAsync(Arg.Any<Expression<Func<Data.Data.Profile, bool>>>());
        }

        [TestMethod]
        public async Task Create_WithValidRequest_AndProfilesDoesNotHasDefault_ShouldThrowBusinessException()
        {
            _profileUserRequest!.Profiles.AddRange(new List<ProfileRequest>()
            {
                new ProfileRequest { Id = 1, Default = false },
                new ProfileRequest { Id = 2, Default = false}
            });
            _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(true);
            _profileRepository.AnyAsync(Arg.Any<Expression<Func<Data.Data.Profile, bool>>>()).Returns(true);

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _profileUserManager.Create(_profileUserRequest!));
            await _userRepository.Received(1).AnyAsync(Arg.Any<Expression<Func<User, bool>>>());
            await _profileRepository.Received(1).AnyAsync(Arg.Any<Expression<Func<Data.Data.Profile, bool>>>());
        }

        [TestMethod]
        public async Task Create_WithValidRequest_AddTwoProfiles_ButThrowBusinessExceptionWhenSaving()
        {
            _profileUserRequest!.Profiles.AddRange(new List<ProfileRequest>()
            {
                new ProfileRequest { Id = 1, Default = true },
                new ProfileRequest { Id = 2, Default = false }
            });
            _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(true);
            _profileRepository.AnyAsync(Arg.Any<Expression<Func<Data.Data.Profile, bool>>>()).Returns(true);
            _profileUserRepository.Add(Arg.Any<ProfileUser>()).Returns(_profileUserEntity);
            _profileUserRepository.SaveAsync().Returns(false);

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _profileUserManager.Create(_profileUserRequest!));
            await _userRepository.Received(1).AnyAsync(Arg.Any<Expression<Func<User, bool>>>());
            await _profileRepository.Received(1).AnyAsync(Arg.Any<Expression<Func<Data.Data.Profile, bool>>>());
            _profileUserRepository.Received(1).AddRange(Arg.Any<IEnumerable<ProfileUser>>());
            await _profileUserRepository.Received(1).SaveAsync();
        }

        [TestMethod]
        public async Task Create_WithValidRequest_AddTwoProfiles_AndSucessfullySave_ShouldReturnBaseResponse()
        {
            _profileUserRequest!.Profiles.AddRange(new List<ProfileRequest>()
            {
                new ProfileRequest { Id = 1, Default = true },
                new ProfileRequest { Id = 2, Default = false}
            });
            _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(true);
            _profileRepository.AnyAsync(Arg.Any<Expression<Func<Data.Data.Profile, bool>>>()).Returns(true);
            _profileUserRepository.Add(Arg.Any<ProfileUser>()).Returns(_profileUserEntity);
            _profileUserRepository.SaveAsync().Returns(true);

            BaseResponse result = await _profileUserManager.Create(_profileUserRequest!);

            await _userRepository.Received(1).AnyAsync(Arg.Any<Expression<Func<User, bool>>>());
            await _profileRepository.Received(1).AnyAsync(Arg.Any<Expression<Func<Data.Data.Profile, bool>>>());
            _profileUserRepository.Received(1).AddRange(Arg.Any<IEnumerable<ProfileUser>>());
            await _profileUserRepository.Received(1).SaveAsync();
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.Status);
        }
    }
}
