using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WAGym.Common.Enum;
using WAGym.Common.Model.Login.Request;
using WAGym.Common.Model.Login.Response;
using WAGym.Common.Resource;
using WAGym.Domain.Manager.Interface;

namespace WAGym.AccessControl.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginManager _loginManager;

        public LoginController(ILoginManager loginManager)
        {
            _loginManager = loginManager;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<ActionResult<LoginResponse>> Authenticate([FromBody] LoginRequest request)
        {
            if (request == null)
                return BadRequest(Resource.InvalidRequest);

            LoginResponse response = await _loginManager.Authenticate(request);

            if (response == null || response.Token == null || !response.Token.Authenticated)
                return Unauthorized();

            return Ok(response);
        }

        [HttpPost("RefreshToken")]
        public ActionResult<LoginResponse> RefreshToken()
        {
            LoginResponse response = _loginManager.RefreshToken();

            if (response == null || response.Token == null || !response.Token.Authenticated)
                return Unauthorized();

            return Ok(response);
        }

        [HttpPost("Revoke")]
        public ActionResult<LoginResponse> RevokeToken()
        {
            LoginResponse response = _loginManager.RevokeToken();

            if (response == null || response.Token == null || !response.Token.Authenticated)
                return Unauthorized();

            return Ok(response);
        }

        [HttpGet("ProfileHasFunctionality")]
        public async Task<ActionResult<LoginProfileResponse>> ProfileHasFunctionality([FromQuery] FunctionalityEnum functionality) 
        {
            return Ok(await _loginManager.ProfileHasFunctionality(functionality));
        }
    }
}