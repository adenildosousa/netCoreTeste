using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WAGym.Common.Enum;
using WAGym.Common.Model.Base.Response;
using WAGym.Common.Model.Login.Response;
using WAGym.Common.Model.Pagination.Request;
using WAGym.Common.Model.Pagination.Response;
using WAGym.Common.Model.User.Request;
using WAGym.Common.Model.User.Response;
using WAGym.Common.Resource;
using WAGym.Data.Data;
using WAGym.Domain.Manager.Interface;

namespace WAGym.AccessControl.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ILoginManager _loginManager;

        public UserController(IUserManager userManager,
                              ILoginManager loginManager)
        {
            _userManager = userManager;
            _loginManager = loginManager;
        }

        //[HttpPost]
        //public async Task<ActionResult<BaseResponse>> Post([FromBody] UserRequest request)
        //{
        //    if (request == null)
        //        return BadRequest(Resource.InvalidRequest);

        //        LoginProfileResponse loginProfile = await _loginManager.ProfileHasFunctionality(FunctionalityEnum.CreateUser);

        //        if (!loginProfile.HasAccess)
        //            return Unauthorized(loginProfile.Message);

        //        BaseResponse response = await _userManager.CreateUser(request);

        //        if (response == null)
        //            return NotFound(Resource.UserNotFound);
                
        //        return Ok(response);
        //}

        [HttpPut("companyId/{companyId}/personId/{personId}")]
        public async Task<ActionResult<BaseResponse>> Put(long companyId, long personId, [FromBody] PutUserRequest request)
        {
            if (request == null || companyId <= 0 || personId <= 0)
                return BadRequest(Resource.InvalidRequest);
            
            LoginProfileResponse loginProfile = await _loginManager.ProfileHasFunctionality(FunctionalityEnum.EditUser);

            if (!loginProfile.HasAccess)
                return Unauthorized(loginProfile.Message);

            BaseResponse response = await _userManager.EditUser(companyId, personId, request);

            if (response == null)
                return NotFound(Resource.UserNotFound);

            return Ok(response);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<UserResponse>> Get(long id)
        {
            if (id <= 0)
                return BadRequest(Resource.InvalidRequest);

            LoginProfileResponse loginProfileResponse = await _loginManager.ProfileHasFunctionality(FunctionalityEnum.DetailUser);

            if (!loginProfileResponse.HasAccess)
                return Unauthorized(loginProfileResponse.Message);

            UserResponse response = await _userManager.DetailUser(id);

            if (response == null)
                return NotFound(Resource.UserNotFound);

            return Ok(response);
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<IEnumerable<PaginationResponse<UserResponse>>>> GetList([FromQuery] PaginationRequest request)
        {
            if (request.CompanyId <= 0 || request.PageNumber <= 0 || request.PageSize <= 0)
                return BadRequest(Resource.InvalidRequest);

            LoginProfileResponse loginProfileResponse = await _loginManager.ProfileHasFunctionality(FunctionalityEnum.DetailUser);

            if (!loginProfileResponse.HasAccess)
                return Unauthorized(loginProfileResponse.Message);

            PaginationResponse<UserResponse> response = await _userManager.GetList(request);

            if (response == null)
                return NotFound(Resource.UserNotFound);

            return Ok(response);
        }

        [HttpPatch("id/{id}")]
        public async Task<ActionResult<BaseResponse>> Patch(long id, [FromBody] StatusEnum status)
        {
            if (id <= 0)
                return BadRequest(Resource.InvalidRequest);

            LoginProfileResponse loginProfileResponse = await _loginManager.ProfileHasFunctionality(FunctionalityEnum.DeleteUser);

            if (!loginProfileResponse.HasAccess)
                return Unauthorized(loginProfileResponse.Message);

            BaseResponse response = await _userManager.DeleteUser(id, status);

            if (response == null)
                return NotFound(Resource.UserNotFound);

            return Ok(response);
        }



        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User request)
        {

            return Ok();
        }


    }
}
