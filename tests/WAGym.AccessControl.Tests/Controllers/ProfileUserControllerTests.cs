using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Net;
using WAGym.AccessControl.Controllers;
using WAGym.Common.Enum;
using WAGym.Common.Model.Base.Response;
using WAGym.Common.Model.Login.Response;
using WAGym.Common.Model.ProfileUser.Request;
using WAGym.Common.Resource;
using WAGym.Domain.Extension;
using WAGym.Domain.Manager.Interface;

namespace WAGym.AccessControl.Tests.Controllers
{
    [TestClass]
    public class ProfileUserControllerTests
    {
        private ProfileUserController _controller;
        private ILoginManager _loginManager;
        private IProfileUserManager _profileUserManager;
        private ProfileUserRequest? _profileUserRequest;
        private LoginProfileResponse _loginProfileResponse;
        private BaseResponse? _baseResponse;

        [TestInitialize]
        public void Setup()
        {
            _loginManager = Substitute.For<ILoginManager>();
            _profileUserManager = Substitute.For<IProfileUserManager>();
            _controller = new ProfileUserController(_loginManager, _profileUserManager);

            _profileUserRequest = new ProfileUserRequest
            {
                CompanyId = 1,
                PersonId = 1,
                UserId = 1
            };
            _loginProfileResponse = new LoginProfileResponse(true);
            _baseResponse = new BaseResponse(HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task Post_WithInvalidRequest_ShouldReturnBadRequest()
        {
            _profileUserRequest = null;

            ActionResult<BaseResponse> result = await _controller.Post(_profileUserRequest!);

            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((ObjectResult)result.Result!).StatusCode);
            Assert.AreEqual(Resource.InvalidRequest, ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task Post_WithValidRequest_AndHasNoAccessToFunctionality_ShouldReturnUnauthorized()
        {
            _loginProfileResponse = new LoginProfileResponse(false, string.Format(Resource.ProfileDoesNotHaveAccessToFunctionality, FunctionalityEnum.CreateProfileUser.GetName()));
            _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>()).Returns(_loginProfileResponse);

            ActionResult<BaseResponse> result = await _controller.Post(_profileUserRequest!);

            await _loginManager.Received(1).ProfileHasFunctionality(Arg.Any<FunctionalityEnum>());
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, ((ObjectResult)result.Result!).StatusCode);
        }

        [TestMethod]
        public async Task Post_WithValidRequest_AndHasAccessToFunctionality_AndResponseIsNull_ShouldReturnNotFound()
        {
            _baseResponse = null;
            _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>()).Returns(_loginProfileResponse);
            _profileUserManager.Create(Arg.Any<ProfileUserRequest>()).Returns(_baseResponse!);

            ActionResult<BaseResponse> result = await _controller.Post(_profileUserRequest!);

            await _loginManager.Received(1).ProfileHasFunctionality(Arg.Any<FunctionalityEnum>());
            await _profileUserManager.Received(1).Create(Arg.Any<ProfileUserRequest>());
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ((ObjectResult)result.Result!).StatusCode);
        }

        [TestMethod]
        public async Task Post_WithValidRequest_AndHasAccessToFunctionality_ShouldReturnOk()
        {
            _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>()).Returns(_loginProfileResponse);
            _profileUserManager.Create(Arg.Any<ProfileUserRequest>()).Returns(_baseResponse!);

            ActionResult<BaseResponse> result = await _controller.Post(_profileUserRequest!);

            await _loginManager.Received(1).ProfileHasFunctionality(Arg.Any<FunctionalityEnum>());
            await _profileUserManager.Received(1).Create(Arg.Any<ProfileUserRequest>());
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((ObjectResult)result.Result!).StatusCode);
            Assert.IsNull(((BaseResponse)((ObjectResult)result.Result).Value!).Message);
        }
    }
}