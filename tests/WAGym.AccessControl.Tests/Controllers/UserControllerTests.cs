using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Net;
using WAGym.AccessControl.Controllers;
using WAGym.Common.Enum;
using WAGym.Common.Model.Base.Response;
using WAGym.Common.Model.Login.Response;
using WAGym.Common.Model.Pagination.Request;
using WAGym.Common.Model.Pagination.Response;
using WAGym.Common.Model.User.Request;
using WAGym.Common.Model.User.Response;
using WAGym.Common.Resource;
using WAGym.Domain.Extension;
using WAGym.Domain.Manager.Interface;

namespace WAGym.AccessControl.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        private UserController _userController;
        private IUserManager _userManager;
        private ILoginManager _loginManager;
        private UserRequest _userRequest;
        private PutUserRequest _putUserRequest;
        private PaginationRequest _paginationRequest;
        private BaseResponse _baseResponse;
        private LoginProfileResponse _loginProfileResponse;
        private UserResponse _userResponse;
        private IEnumerable<UserResponse> _userResponses;
        private PaginationResponse<UserResponse> _paginationResponse;
        private long _companyId;
        private long _personId;

        [TestInitialize]
        public void Setup()
        {
            string username = "username";
            string password = "123";
            long companyId = 1;

            _userManager = Substitute.For<IUserManager>();
            _loginManager = Substitute.For<ILoginManager>();
            _userController = new UserController(_userManager, _loginManager);
            _userRequest = new UserRequest(1, username, password, companyId);
            _putUserRequest = new PutUserRequest(username, password, StatusEnum.Active);
            _paginationRequest = new PaginationRequest
            {
                CompanyId = 1,
                PageNumber = 1,
                PageSize = 1,
            };
            _baseResponse = new BaseResponse(HttpStatusCode.OK);
            _loginProfileResponse = new LoginProfileResponse(true);
            _companyId = 1;
            _personId = 1;
            _userResponse = new UserResponse(_personId,
                                             1,
                                             "username",
                                             (int)StatusEnum.Active,
                                             StatusEnum.Active.GetName(),
                                             10,
                                             "user",
                                             1);
            _userResponses = new List<UserResponse>()
            {
                new UserResponse(1, 1, "user1", (int)StatusEnum.Active, StatusEnum.Active.GetName(), 10, "user", 1),
                new UserResponse(2, 2, "user2", (int)StatusEnum.Active, StatusEnum.Active.GetName(), 10, "user", 1),
                new UserResponse(3, 3, "user3", (int)StatusEnum.Active, StatusEnum.Active.GetName(), 10, "user", 1),
            };

            _paginationResponse = new PaginationResponse<UserResponse>(_userResponses, 2, 2, 2, 3);
        }

        [TestMethod]
        public async Task Post_WithInvalidRequest_ShouldReturnBadRequest()
        {
            _userRequest = null;

            ActionResult<BaseResponse> result = await _userController.Post(_userRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(Resource.InvalidRequest, ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task Post_WithValidRequest_AndHasNoAccessToFunctionality_ShouldReturnUnauthorized()
        {
            _loginProfileResponse = new LoginProfileResponse(false, string.Format(Resource.ProfileDoesNotHaveAccessToFunctionality, FunctionalityEnum.CreateUser.GetName()));
            _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>()).Returns(_loginProfileResponse);

            ActionResult<BaseResponse> result = await _userController.Post(_userRequest);

            await _loginManager.Received(1).ProfileHasFunctionality(Arg.Any<FunctionalityEnum>());
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, ((ObjectResult)result.Result).StatusCode);
        }

        [TestMethod]
        public async Task Post_WithValidRequest_AndHasAccessToFunctionality_AndResponseIsNull_ShouldReturnNotFound()
        {
            _baseResponse = null;
            _userManager.CreateUser(Arg.Any<UserRequest>()).Returns(_baseResponse);
            _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>()).Returns(_loginProfileResponse);

            ActionResult<BaseResponse> result = await _userController.Post(_userRequest);

            await _userManager.Received(1).CreateUser(Arg.Any<UserRequest>());
            await _loginManager.Received(1).ProfileHasFunctionality(Arg.Any<FunctionalityEnum>());
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(Resource.UserNotFound, ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task Post_WithValidRequest_AndHasAccessToFunctionality_ShouldReturnOk()
        {
            _userManager.CreateUser(Arg.Any<UserRequest>()).Returns(_baseResponse);
            _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>()).Returns(_loginProfileResponse);

            ActionResult<BaseResponse> result = await _userController.Post(_userRequest);

            await _userManager.Received(1).CreateUser(Arg.Any<UserRequest>());
            await _loginManager.Received(1).ProfileHasFunctionality(Arg.Any<FunctionalityEnum>());
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((ObjectResult)result.Result).StatusCode);
            Assert.IsNull(((BaseResponse)((ObjectResult)result.Result).Value).Message);
        }

        [TestMethod]
        public async Task Put_WithInvalidCompanyId_ShouldReturnBadRequest()
        {
            _companyId = 0;

            ActionResult<BaseResponse> result = await _userController.Put(_companyId, _personId, _putUserRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(Resource.InvalidRequest, ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task Put_WithInvalidPersonId_ShouldReturnBadRequest()
        {
            _personId = 0;

            ActionResult<BaseResponse> result = await _userController.Put(_companyId, _personId, _putUserRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(Resource.InvalidRequest, ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task Put_WithInvalidRequest_ShouldReturnBadRequest()
        {
            _putUserRequest = null;

            ActionResult<BaseResponse> result = await _userController.Put(_companyId, _personId, _putUserRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(Resource.InvalidRequest, ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task Put_WithValidRequest_AndHasNoAccess_ShouldReturnUnauthorized()
        {
            _loginProfileResponse = new LoginProfileResponse(false, string.Format(Resource.ProfileDoesNotHaveAccessToFunctionality, FunctionalityEnum.CreateUser.GetName()));
            _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>()).Returns(_loginProfileResponse);

            ActionResult<BaseResponse> result = await _userController.Put(_companyId, _personId, _putUserRequest);

            await _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>());
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(string.Format(Resource.ProfileDoesNotHaveAccessToFunctionality, FunctionalityEnum.CreateUser.GetName()), ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task Put_WithValidRequest_AndHasAccessToFunctionality_AndResponseIsNull_ShouldReturnNotFound()
        {
            _baseResponse = null;
            _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>()).Returns(_loginProfileResponse);
            _userManager.EditUser(Arg.Any<long>(), Arg.Any<long>(), Arg.Any<PutUserRequest>()).Returns(_baseResponse);

            ActionResult<BaseResponse> result = await _userController.Put(_companyId, _personId, _putUserRequest);

            await _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>());
            await _userManager.EditUser(Arg.Any<long>(), Arg.Any<long>(), Arg.Any<PutUserRequest>());
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(Resource.UserNotFound, ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task Put_WithValidRequest_AndHasAccessToFunctionality_AndResponseIsReturned_ShouldReturnOk()
        {
            _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>()).Returns(_loginProfileResponse);
            _userManager.EditUser(Arg.Any<long>(), Arg.Any<long>(), Arg.Any<PutUserRequest>()).Returns(_baseResponse);

            ActionResult<BaseResponse> result = await _userController.Put(_companyId, _personId, _putUserRequest);

            await _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>());
            await _userManager.EditUser(Arg.Any<long>(), Arg.Any<long>(), Arg.Any<PutUserRequest>());
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((ObjectResult)result.Result).StatusCode);
        }

        [TestMethod]
        public async Task Get_WithInvalidId_ShouldReturnBadRequest()
        {
            _personId = 0;

            ActionResult<UserResponse> result = await _userController.Get(_personId);

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(Resource.InvalidRequest, ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task Get_WithValidId_AndHasNoAccessToFunctionality_ShouldReturnUnauthorized()
        {
            _loginProfileResponse = new LoginProfileResponse(false, string.Format(Resource.ProfileDoesNotHaveAccessToFunctionality, FunctionalityEnum.CreateUser.GetName()));
            _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>()).Returns(_loginProfileResponse);

            ActionResult<UserResponse> result = await _userController.Get(_personId);

            await _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>());
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(string.Format(Resource.ProfileDoesNotHaveAccessToFunctionality, FunctionalityEnum.CreateUser.GetName()), ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task Get_WithValidId_AndHasAccessToFunctionality_AndResponseIsNull_ShouldReturnNotFound()
        {
            _userResponse = null;
            _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>()).Returns(_loginProfileResponse);
            _userManager.DetailUser(Arg.Any<long>()).Returns(_userResponse);

            ActionResult<UserResponse> result = await _userController.Get(_personId);

            await _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>());
            await _userManager.DetailUser(Arg.Any<long>());
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(Resource.UserNotFound, ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task Get_WithValidId_AndHasNoAccessToFunctionality_AndResponseIsReturned_ShouldReturnOk()
        {
            _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>()).Returns(_loginProfileResponse);
            _userManager.DetailUser(Arg.Any<long>()).Returns(_userResponse);

            ActionResult<UserResponse> result = await _userController.Get(_personId);

            await _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>());
            await _userManager.DetailUser(Arg.Any<long>());
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((ObjectResult)result.Result).StatusCode);
        }

        [TestMethod]
        public async Task GetList_WithInvalidCompanyId_ShouldReturnBadRequest()
        {
            _paginationRequest.CompanyId = 0;

            ActionResult<IEnumerable<PaginationResponse<UserResponse>>> result = await _userController.GetList(_paginationRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(Resource.InvalidRequest, ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task GetList_WithInvalidPageNumber_ShouldReturnBadRequest()
        {
            _paginationRequest.PageNumber = 0;

            ActionResult<IEnumerable<PaginationResponse<UserResponse>>> result = await _userController.GetList(_paginationRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(Resource.InvalidRequest, ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task GetList_WithInvalidPageSize_ShouldReturnBadRequest()
        {
            _paginationRequest.PageSize = 0;

            ActionResult<IEnumerable<PaginationResponse<UserResponse>>> result = await _userController.GetList(_paginationRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(Resource.InvalidRequest, ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task GetList_WithValidRequest_ShouldReturnBadRequest()
        {
            _loginProfileResponse = new LoginProfileResponse(false, string.Format(Resource.ProfileDoesNotHaveAccessToFunctionality, FunctionalityEnum.DeleteUser.GetName()));
            _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>()).Returns(_loginProfileResponse);

            ActionResult<IEnumerable<PaginationResponse<UserResponse>>> result = await _userController.GetList(_paginationRequest);

            await _loginManager.Received(1).ProfileHasFunctionality(Arg.Any<FunctionalityEnum>());
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(string.Format(Resource.ProfileDoesNotHaveAccessToFunctionality, FunctionalityEnum.DeleteUser.GetName()), ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task GetList_WithValidRequest_AndHasAccessToFunctionality_AndResponseIsNull_ShouldReturnNotFound()
        {
            _paginationResponse = null;
            _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>()).Returns(_loginProfileResponse);
            _userManager.GetList(Arg.Any<PaginationRequest>()).Returns(_paginationResponse);

            ActionResult<IEnumerable<PaginationResponse<UserResponse>>> result = await _userController.GetList(_paginationRequest);

            await _loginManager.Received(1).ProfileHasFunctionality(Arg.Any<FunctionalityEnum>());
            await _userManager.Received(1).GetList(Arg.Any<PaginationRequest>());
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(Resource.UserNotFound, ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task GetList_WithValidRequest_AndHasAccessToFunctionality_AndResponseIsReturned_ShouldReturnOk()
        {
            _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>()).Returns(_loginProfileResponse);
            _userManager.GetList(Arg.Any<PaginationRequest>()).Returns(_paginationResponse);

            ActionResult<IEnumerable<PaginationResponse<UserResponse>>> result = await _userController.GetList(_paginationRequest);

            await _loginManager.Received(1).ProfileHasFunctionality(Arg.Any<FunctionalityEnum>());
            await _userManager.Received(1).GetList(Arg.Any<PaginationRequest>());
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((ObjectResult)result.Result).StatusCode);
        }

        [TestMethod]
        public async Task Patch_WithInvalidId_ShouldReturnBadRequest()
        {
            _personId = 0;

            ActionResult<BaseResponse> result = await _userController.Patch(_personId, StatusEnum.Active);

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(Resource.InvalidRequest, ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task Patch_WithValidId_AndHasNoAccessToFunctionality_ShouldReturnUnauthorized()
        {
            _loginProfileResponse = new LoginProfileResponse(false, string.Format(Resource.ProfileDoesNotHaveAccessToFunctionality, FunctionalityEnum.DeleteUser.GetName()));
            _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>()).Returns(_loginProfileResponse);

            ActionResult<BaseResponse> result = await _userController.Patch(_personId, StatusEnum.Active);

            await _loginManager.Received(1).ProfileHasFunctionality(Arg.Any<FunctionalityEnum>());
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(string.Format(Resource.ProfileDoesNotHaveAccessToFunctionality, FunctionalityEnum.DeleteUser.GetName()), ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task Patch_WithValidId_AndHasAccessToFunctionality_AndResponseIsNull_ShouldReturnNotFound()
        {
            _baseResponse = null;
            _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>()).Returns(_loginProfileResponse);
            _userManager.DeleteUser(Arg.Any<long>(), Arg.Any<StatusEnum>()).Returns(_baseResponse);

            ActionResult<BaseResponse> result = await _userController.Patch(_personId, StatusEnum.Active);

            await _loginManager.Received(1).ProfileHasFunctionality(Arg.Any<FunctionalityEnum>());
            await _userManager.Received(1).DeleteUser(Arg.Any<long>(), Arg.Any<StatusEnum>());
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(Resource.UserNotFound, ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task Patch_WithValidId_AndHasAccessToFunctionality_AndResponseIsReturned_ShouldReturnOk()
        {
            _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>()).Returns(_loginProfileResponse);
            _userManager.DeleteUser(Arg.Any<long>(), Arg.Any<StatusEnum>()).Returns(_baseResponse);

            ActionResult<BaseResponse> result = await _userController.Patch(_personId, StatusEnum.Active);

            await _loginManager.Received(1).ProfileHasFunctionality(Arg.Any<FunctionalityEnum>());
            await _userManager.Received(1).DeleteUser(Arg.Any<long>(), Arg.Any<StatusEnum>());
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((ObjectResult)result.Result).StatusCode);
        }
    }
}
