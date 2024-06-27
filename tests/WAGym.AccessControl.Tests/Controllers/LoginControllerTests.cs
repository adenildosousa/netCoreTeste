using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Net;
using WAGym.AccessControl.Controllers;
using WAGym.Common.Enum;
using WAGym.Common.Model.Login.Request;
using WAGym.Common.Model.Login.Response;
using WAGym.Common.Model.Token;
using WAGym.Common.Resource;
using WAGym.Domain.Manager.Interface;

namespace WAGym.AccessControl.Tests.Controllers
{
    [TestClass]
    public class LoginControllerTests
    {
        private LoginController _controller;
        private ILoginManager _loginManager;
        private LoginRequest _request;
        private LoginResponse _response;
        private LoginProfileResponse _loginProfileResponse;

        [TestInitialize]
        public void Setup()
        {
            _loginManager = Substitute.For<ILoginManager>();
            _controller = new LoginController(_loginManager);

            _request = new LoginRequest(1, "username", "123", 1);
            _response = new LoginResponse(1, 1, 1, new Token(true, DateTime.Now, DateTime.Now, "accessToken", "refreshToken"), null);
            _loginProfileResponse = new LoginProfileResponse(true);
        }

        [TestMethod]
        public async Task Authenticate_WithInvalidRequest_ShouldReturnBadRequest()
        {
            _request = null;

            ActionResult<LoginResponse> result = await _controller.Authenticate(_request);

            Assert.AreEqual((int)HttpStatusCode.BadRequest, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(Resource.InvalidRequest, ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task Authenticate_WithValidRequest_AndResponseIsNull_ShouldReturnUnauthorized()
        {
            _response = null;
            _loginManager.Authenticate(Arg.Any<LoginRequest>()).Returns(Task.FromResult(_response));

            ActionResult<LoginResponse> result = await _controller.Authenticate(_request);

            await _loginManager.Received(1).Authenticate(Arg.Any<LoginRequest>());
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, ((StatusCodeResult)result.Result).StatusCode);
        }

        [TestMethod]
        public async Task Authenticate_WithValidRequest_AndTokeIsNull_ShouldReturnUnauthorized()
        {
            _response = new LoginResponse(1, 1, 1, null, null);
            _loginManager.Authenticate(Arg.Any<LoginRequest>()).Returns(Task.FromResult(_response));

            ActionResult<LoginResponse> result = await _controller.Authenticate(_request);

            await _loginManager.Received(1).Authenticate(Arg.Any<LoginRequest>());
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, ((StatusCodeResult)result.Result).StatusCode);
        }

        [TestMethod]
        public async Task Authenticate_WithValidRequest_AndTokenIsNotAuthenticated_ShouldReturnUnauthorized()
        {
            _response = new LoginResponse(1, 1, 1, new Token(false, DateTime.Now, DateTime.Now, "", ""), null);
            _loginManager.Authenticate(Arg.Any<LoginRequest>()).Returns(Task.FromResult(_response));

            ActionResult<LoginResponse> result = await _controller.Authenticate(_request);

            await _loginManager.Received(1).Authenticate(Arg.Any<LoginRequest>());
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, ((StatusCodeResult)result.Result).StatusCode);
        }

        [TestMethod]
        public async Task Authenticate_WithValidRequest_ShouldReturnOk()
        {
            _loginManager.Authenticate(Arg.Any<LoginRequest>()).Returns(Task.FromResult(_response));

            ActionResult<LoginResponse> result = await _controller.Authenticate(_request);

            await _loginManager.Received(1).Authenticate(Arg.Any<LoginRequest>());
            Assert.AreEqual((int)HttpStatusCode.OK, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(_response, ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public void RefreshToken_WhenResponseIsNull_ShouldReturnUnauthorized()
        {
            _response = null;
            _loginManager.RefreshToken().Returns(_response);

            ActionResult<LoginResponse> result = _controller.RefreshToken();

            _loginManager.Received(1).RefreshToken();
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, ((StatusCodeResult)result.Result).StatusCode);
        }

        [TestMethod]
        public void RefreshToken_WhenTokenIsNull_ShouldReturnUnauthorized()
        {
            _response = new LoginResponse(1, 1, 1, null, null);
            _loginManager.RefreshToken().Returns(_response);

            ActionResult<LoginResponse> result = _controller.RefreshToken();

            _loginManager.Received(1).RefreshToken();
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, ((StatusCodeResult)result.Result).StatusCode);
        }

        [TestMethod]
        public async Task RefreshToken_WhenTokenIsNotAuthenticated_ShouldReturnUnauthorized()
        {
            _response = new LoginResponse(1, 1, 1, new Token(false, DateTime.Now, DateTime.Now, "", ""), null);
            _loginManager.RefreshToken().Returns(_response);

            ActionResult<LoginResponse> result = _controller.RefreshToken();

            _loginManager.Received(1).RefreshToken();
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, ((StatusCodeResult)result.Result).StatusCode);
        }

        [TestMethod]
        public async Task RefreshToken_WithValidRequest_ShouldReturnOk()
        {
            _response = new LoginResponse(1, 1, 1, new Token(true, DateTime.Now, DateTime.Now, "accessToken", "refreshToken"), null);
            _loginManager.RefreshToken().Returns(_response);

            ActionResult<LoginResponse> result = _controller.RefreshToken();

            _loginManager.Received(1).RefreshToken();
            Assert.AreEqual((int)HttpStatusCode.OK, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(_response, ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public void Revoke_WhenResponseIsNull_ShouldReturnUnauthorized()
        {
            _response = null;
            _loginManager.RevokeToken().Returns(_response);

            ActionResult<LoginResponse> result = _controller.RevokeToken();

            _loginManager.Received(1).RevokeToken();
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, ((StatusCodeResult)result.Result).StatusCode);
        }

        [TestMethod]
        public void Revoke_WhenTokenIsNull_ShouldReturnUnauthorized()
        {
            _response = new LoginResponse(1, 1, 1, null, null);
            _loginManager.RevokeToken().Returns(_response);

            ActionResult<LoginResponse> result = _controller.RevokeToken();

            _loginManager.Received(1).RevokeToken();
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, ((StatusCodeResult)result.Result).StatusCode);
        }

        [TestMethod]
        public async Task Revoke_WhenTokenIsNotAuthenticated_ShouldReturnUnauthorized()
        {
            _response = new LoginResponse(1, 1, 1, new Token(false, DateTime.Now, DateTime.Now, "", ""), null);
            _loginManager.RevokeToken().Returns(_response);

            ActionResult<LoginResponse> result = _controller.RevokeToken();

            _loginManager.Received(1).RevokeToken();
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, ((StatusCodeResult)result.Result).StatusCode);
        }

        [TestMethod]
        public async Task Revoke_WithValidRequest_ShouldReturnOk()
        {
            _response = new LoginResponse(1, 1, 1, new Token(true, DateTime.Now, DateTime.Now, "accessToken", "refreshToken"), null);
            _loginManager.RevokeToken().Returns(_response);

            ActionResult<LoginResponse> result = _controller.RevokeToken();

            _loginManager.Received(1).RevokeToken();
            Assert.AreEqual((int)HttpStatusCode.OK, ((ObjectResult)result.Result).StatusCode);
            Assert.AreEqual(_response, ((ObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task ProfileHasFunctionality_ShouldReturnOk()
        {
            _loginManager.ProfileHasFunctionality(Arg.Any<FunctionalityEnum>()).Returns(_loginProfileResponse);

            ActionResult<LoginProfileResponse> result = await _controller.ProfileHasFunctionality(FunctionalityEnum.CreateUser);

            await _loginManager.Received(1).ProfileHasFunctionality(Arg.Any<FunctionalityEnum>());
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, ((ObjectResult)result.Result).StatusCode);
        }
    }
}
