using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Net;
using WAGym.Common.Enum;
using WAGym.Common.Exception;
using WAGym.Common.Model.Base.Response;
using WAGym.Common.Model.Login.Response;
using WAGym.Common.Model.Pagination.Request;
using WAGym.Common.Model.Pagination.Response;
using WAGym.Common.Model.Profile.Response;
using WAGym.Common.Model.Token;
using WAGym.Common.Model.User.Request;
using WAGym.Common.Model.User.Response;
using WAGym.Data.Data;
using WAGym.Data.Repository.Interface;
using WAGym.Domain.AutoMapper;
using WAGym.Domain.Manager;
using WAGym.Domain.Manager.Interface;

namespace WAGym.Domain.Tests.Manager
{
    [TestClass]
    public class UserManagerTests
    {
        private UserManager _userManager;
        private IUserRepository _userRepository;
        private ISessionManager _sessionManager;
        private IValidator<User> _validator;
        private IMapper _mapper;
        private LoginResponse _loginResponse;
        private UserRequest _userRequest;
        private PutUserRequest _putUserRequest;
        private PaginationRequest _paginationRequest;
        private ValidationResult _resultValidation;
        private IEnumerable<User> _userList;
        private User _userEntity;
        private long _companyId;
        private long _personId;

        [TestInitialize]
        public void Setup()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _sessionManager = Substitute.For<ISessionManager>();
            _validator = Substitute.For<IValidator<User>>();

            Token token = new Token(true, DateTime.Now, DateTime.Now.AddMonths(1), "Access Token", "Refresh Token");
            _loginResponse = new LoginResponse(1, 1, 1, token, new ProfileResponse());
            _sessionManager.GetLoggedUserSessionInfo(Arg.Any<string>()).Returns(_loginResponse);

            IConfigurationProvider configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserMapping>();
            });
            _mapper = configuration.CreateMapper();
            _userManager = new UserManager(
                _userRepository,
                _sessionManager,
                _validator,
                _mapper);

            long personId = 1;
            string username = "username";
            string password = "123";
            long companyId = 1;
            _userRequest = new UserRequest(personId, username, password, companyId);
            _putUserRequest = new PutUserRequest(username, password, StatusEnum.Active);
            _paginationRequest = new PaginationRequest
            {
                CompanyId = 1,
                PageNumber = 1,
                PageSize = 1
            };
            _userEntity = new User
            {
                CompanyId = 1,
                PersonId = personId,
                Username = username,
                Password = password,
                StatusId = (int)StatusEnum.Active
            };
            _companyId = 1;
            _personId = 1;
            _resultValidation = new ValidationResult();
            _userList = new List<User>()
            {
                new User
                {
                    Id = 1,
                    PersonId = 1,
                    StatusId = (int)StatusEnum.Active
                },
                new User
                {
                    Id = 2,
                    PersonId = 2,
                    StatusId = (int)StatusEnum.Active
                }
            };
        }

        [TestMethod]
        public async Task CreateUser_WithInvalidRequest_ShouldThrowValidationException()
        {
            _resultValidation = null;
            _validator.ValidateAsync(Arg.Any<User>()).Returns(_resultValidation);

            await Assert.ThrowsExceptionAsync<ValidationException>(async () => await _userManager.CreateUser(_userRequest));
            await _validator.Received(1).ValidateAsync(Arg.Any<User>());
        }

        [TestMethod]
        public async Task CreateUser_WithValidRequest_AndExistingUser_ShouldThrowBusinessException()
        {
            _validator.ValidateAsync(Arg.Any<User>()).Returns(_resultValidation);
            _userRepository.UserExists(Arg.Any<long>(), Arg.Any<string>(), Arg.Any<long>()).Returns(true);

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _userManager.CreateUser(_userRequest));
            await _validator.Received(1).ValidateAsync(Arg.Any<User>());
            await _userRepository.Received(1).UserExists(Arg.Any<long>(), Arg.Any<string>(), Arg.Any<long>());
        }

        [TestMethod]
        public async Task CreateUser_WithValidRequest_AndErrorOnSave_ShouldThrowBusinessException()
        {
            _validator.ValidateAsync(Arg.Any<User>()).Returns(_resultValidation);
            _userRepository.UserExists(Arg.Any<long>(), Arg.Any<string>(), Arg.Any<long>()).Returns(false);
            _userRepository.SaveAsync().Returns(false);

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _userManager.CreateUser(_userRequest));
            await _validator.Received(1).ValidateAsync(Arg.Any<User>());
            await _userRepository.Received(1).UserExists(Arg.Any<long>(), Arg.Any<string>(), Arg.Any<long>());
            await _userRepository.Received(1).SaveAsync();
        }

        [TestMethod]
        public async Task CreateUser_WithValidRequest_AndNonExistingUsername_ShouldReturnBaseResponse_WithStatusCodeOk()
        {
            _validator.ValidateAsync(Arg.Any<User>()).Returns(_resultValidation);
            _userRepository.UserExists(Arg.Any<long>(), Arg.Any<string>(), Arg.Any<long>()).Returns(false);
            _userRepository.SaveAsync().Returns(true);

            BaseResponse result = await _userManager.CreateUser(_userRequest);

            await _validator.Received(1).ValidateAsync(Arg.Any<User>());
            await _userRepository.Received(1).UserExists(Arg.Any<long>(), Arg.Any<string>(), Arg.Any<long>());
            await _userRepository.Received(1).SaveAsync();
            Assert.AreEqual(HttpStatusCode.OK, result.Status);
            Assert.IsNull(result.Message);
        }

        [TestMethod]
        public async Task EditUser_WithInvalidCompanyId_ShouldThrowBusinessException()
        {
            _companyId = 0;

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _userManager.EditUser(_companyId, _personId, _putUserRequest));
        }

        [TestMethod]
        public async Task EditUser_WithInvalidPersonId_ShouldThrowBusinessException()
        {
            _personId = 0;
            
            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _userManager.EditUser(_companyId, _personId, _putUserRequest));
        }

        [TestMethod]
        public async Task EditUser_WithInvalidRequest_ShouldThrowBusinessException()
        {
            _putUserRequest = null;

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _userManager.EditUser(_companyId, _personId, _putUserRequest));
        }

        [TestMethod]
        public async Task EditUser_WithValidRequest_AndUserEntityIsNull_ShouldThrowBusinessException()
        {
            _userEntity = null;
            _userRepository.GetAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(_userEntity);

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _userManager.EditUser(_companyId, _personId, _putUserRequest));
            await _userRepository.Received(1).GetAsync(Arg.Any<Expression<Func<User, bool>>>());
        }

        [TestMethod]
        public async Task EditUser_WithValidRequest_AndUserEntityIsReturned_AndUsernameNotEqualsToRequest_ShouldThrowBusinessException()
        {
            _putUserRequest = new PutUserRequest("username2", "123", StatusEnum.Active);
            _userRepository.GetAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(_userEntity);
            _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(true);

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _userManager.EditUser(_companyId, _personId, _putUserRequest));
            await _userRepository.Received(1).GetAsync(Arg.Any<Expression<Func<User, bool>>>());
            await _userRepository.Received(1).AnyAsync(Arg.Any<Expression<Func<User, bool>>>());
        }

        [TestMethod]
        public async Task EditUser_WithValidRequest_AndUserEntityIsReturned_AndSaveFail_ShouldThrowBusinessException()
        {
            _userRepository.GetAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(_userEntity);
            _userRepository.SaveAsync().Returns(false);

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _userManager.EditUser(_companyId, _personId, _putUserRequest));
            await _userRepository.Received(1).GetAsync(Arg.Any<Expression<Func<User, bool>>>());
            _userRepository.Received(1).Update(Arg.Any<User>());
            await _userRepository.Received(1).SaveAsync();
        }

        [TestMethod]
        public async Task EditUser_WithValidRequest_AndUserEntityIsReturned_AndSave_ShouldReturnBaseResponse()
        {
            _userRepository.GetAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(_userEntity);
            _userRepository.SaveAsync().Returns(true);

            BaseResponse result = await _userManager.EditUser(_companyId, _personId, _putUserRequest);

            await _userRepository.Received(1).GetAsync(Arg.Any<Expression<Func<User, bool>>>());
            _userRepository.Received(1).Update(Arg.Any<User>());
            await _userRepository.Received(1).SaveAsync();
            Assert.IsNotNull(result);
            Assert.IsNull(result.Message);
            Assert.AreEqual(HttpStatusCode.OK, result.Status);
        }

        [TestMethod]
        public async Task DetailUser_WithInvalidId_ShouldThrowBusinessException()
        {
            _personId = 0;

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _userManager.DetailUser(_personId));
        }

        [TestMethod]
        public async Task DetailUser_WithValidId_AndUserEntityIsNull_ShouldThrowBusinessException()
        {
            _userEntity = null;
            _userRepository.GetUserDetail(Arg.Any<long>()).Returns(_userEntity);

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _userManager.DetailUser(_personId));
            await _userRepository.Received(1).GetUserDetail(Arg.Any<long>());
        }

        [TestMethod]
        public async Task DetailUser_WithValidId_AndUserEntityIsReturned_ShouldReturnUserResponse()
        {
            _userRepository.GetUserDetail(Arg.Any<long>()).Returns(_userEntity);

            UserResponse result = await _userManager.DetailUser(_personId);

            await _userRepository.Received(1).GetUserDetail(Arg.Any<long>());
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(UserResponse));
        }

        [TestMethod]
        public async Task GetList_WithInvalidRequest_ShouldThrowBusinessException()
        {
            _paginationRequest = null;

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _userManager.GetList(_paginationRequest));
        }

        [TestMethod]
        public async Task GetList_WithValidRequest_AndNoRecordsFound_ShouldThrowBusinessException()
        {
            _userList = null;
            _userRepository.CountAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(0);
            _userRepository.GetPaginatedList(Arg.Any<PaginationRequest>(), Arg.Any<Expression<Func<User, bool>>>()).Returns(_userList);

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _userManager.GetList(_paginationRequest));
            await _userRepository.Received(1).CountAsync(Arg.Any<Expression<Func<User, bool>>>());
            await _userRepository.Received(1).GetPaginatedList(Arg.Any<PaginationRequest>(), Arg.Any<Expression<Func<User, bool>>>());
        }

        [TestMethod]
        public async Task GetList_WithValidRequest_AndRecordsFound_ShouldReturnPaginationResponseOfUserResponse()
        {
            _userRepository.CountAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(2);
            _userRepository.GetPaginatedList(Arg.Any<PaginationRequest>(), Arg.Any<Expression<Func<User, bool>>>()).Returns(_userList);

            PaginationResponse<UserResponse> result = await _userManager.GetList(_paginationRequest);

            await _userRepository.Received(1).CountAsync(Arg.Any<Expression<Func<User, bool>>>());
            await _userRepository.Received(1).GetPaginatedList(Arg.Any<PaginationRequest>(), Arg.Any<Expression<Func<User, bool>>>());
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PaginationResponse<UserResponse>));
        }

        [TestMethod]
        public async Task DeleteUser_WithInvalidId_ShouldThrowBusinessException()
        {
            _personId = 0;

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _userManager.DeleteUser(_personId, StatusEnum.Inactive));
        }

        [TestMethod]
        public async Task DeleteUser_WithInvalidId_AndUserEntityIsNull_ShouldThrowBusinessException()
        {
            _userEntity = null;
            _userRepository.GetUserDetail(Arg.Any<long>()).Returns(_userEntity);

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _userManager.DeleteUser(_personId, StatusEnum.Inactive));
            await _userRepository.Received(1).GetUserDetail(Arg.Any<long>());
        }

        [TestMethod]
        public async Task DeleteUser_WithValidId_AndUserEntityIsReturned_ShouldReturnBaseResponse()
        {
            _userRepository.GetUserDetail(Arg.Any<long>()).Returns(_userEntity);
            _userRepository.SaveAsync().Returns(true);

            BaseResponse result = await _userManager.DeleteUser(_personId, StatusEnum.Inactive);

            await _userRepository.Received(1).GetUserDetail(Arg.Any<long>());
            await _userRepository.Received(1).SaveAsync();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BaseResponse));
        }
    }
}
