using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WAGym.Common.Enum;
using WAGym.Common.Model.Base.Response;
using WAGym.Common.Model.Login.Response;
using WAGym.Common.Model.ProfileUser.Request;
using WAGym.Common.Resource;
using WAGym.Domain.Manager.Interface;

namespace WAGym.AccessControl.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileUserController : ControllerBase
    {
        private readonly ILoginManager _loginManager;
        private readonly IProfileUserManager _profileUserManager;

        public ProfileUserController(ILoginManager loginManager,
                                     IProfileUserManager profileUserManager)
        {
            _loginManager = loginManager;
            _profileUserManager = profileUserManager;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Post([FromBody] ProfileUserRequest request)
        {
            if (request == null)
                return BadRequest(Resource.InvalidRequest);

            LoginProfileResponse loginProfile = await _loginManager.ProfileHasFunctionality(FunctionalityEnum.CreateProfileUser);

            if (!loginProfile.HasAccess)
                return Unauthorized(loginProfile.Message);

            BaseResponse response = await _profileUserManager.Create(request);

            if (response == null)
                return NotFound(Resource.UserNotFound);

            return Ok(response);
        }
    }
}
