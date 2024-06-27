using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using System.Net;
using WAGym.Common.Enum;
using WAGym.Common.Exception;
using WAGym.Common.Helper;
using WAGym.Common.Model.Base.Response;
using WAGym.Common.Model.Pagination.Request;
using WAGym.Common.Model.Pagination.Response;
using WAGym.Common.Model.User.Request;
using WAGym.Common.Model.User.Response;
using WAGym.Common.Resource;
using WAGym.Data.Data;
using WAGym.Data.Repository.Interface;
using WAGym.Domain.Manager.Interface;

namespace WAGym.Domain.Manager
{
    public class UserManager : Manager, IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _validator;
        private readonly IMapper _mapper;

        public UserManager(IUserRepository userRepository,
                          ISessionManager sessionManager,
                          IValidator<User> validator,
                          IMapper mapper) :
            base(sessionManager)
        {
            _userRepository = userRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<BaseResponse> CreateUser(UserRequest request)
        {
            User userEntity = _mapper.Map<User>(request);

            ValidationResult validation = await _validator.ValidateAsync(userEntity);

            if (validation == null || !validation.IsValid)
                throw new ValidationException(Resource.InvalidRequest, validation?.Errors);

            if (await _userRepository.UserExists(userEntity.PersonId, userEntity.Username, userEntity.CompanyId!.Value))
                throw new BusinessException(Resource.UserAlreadyExists);

            userEntity.UserUpdateId = LoggedUser?.UserId;
            await _userRepository.AddAsync(userEntity);

            if (!await _userRepository.SaveAsync())
                throw new BusinessException(Resource.ErrorSaving);

            return new BaseResponse(HttpStatusCode.OK);
        }

        public async Task<BaseResponse> EditUser(long companyId, long personId, PutUserRequest request)
        {
            if (request == null || companyId <= 0 || personId <= 0)
                throw new BusinessException(Resource.InvalidRequest);

            User? userEntity = await _userRepository.GetAsync(x => x.CompanyId == companyId && x.PersonId == personId);

            if (userEntity == null)
                throw new BusinessException(Resource.UserNotFound);

            if (request.Username != null && userEntity.Username != request.Username)
            {
                if (await _userRepository.AnyAsync(x => x.Username == request.Username))
                    throw new BusinessException(Resource.UserAlreadyExists);
            }

            if (!string.IsNullOrEmpty(request.Username))
                userEntity.Username = request.Username;

            if (!string.IsNullOrEmpty(request.Password))
                userEntity.Password = request.Password;

            if (userEntity.UserUpdateId != LoggedUser?.UserId)
                userEntity.UserUpdateId = LoggedUser?.UserId;

            if (userEntity.StatusId != (int)request.Status)
                userEntity.StatusId = (int)request.Status;

            _userRepository.Update(userEntity);

            if (!await _userRepository.SaveAsync())
                throw new BusinessException(Resource.ErrorSaving);

            return new BaseResponse(HttpStatusCode.OK);
        }

        public async Task<UserResponse> DetailUser(long id)
        {
            if (id <= 0)
                throw new BusinessException(Resource.InvalidRequest);

            User? userEntity = await _userRepository.GetUserDetail(id);

            if (userEntity == null)
                throw new BusinessException(Resource.UserNotFound);

            return _mapper.Map<UserResponse>(userEntity);
        }

        public async Task<PaginationResponse<UserResponse>> GetList(PaginationRequest request)
        {
            if (request == null)
                throw new BusinessException(Resource.InvalidRequest);

            int totalRecords = await _userRepository.CountAsync(x => x.CompanyId == request.CompanyId);
            IEnumerable<User> users = await _userRepository.GetPaginatedList(new PaginationRequest(request), x => x.CompanyId == request.CompanyId);

            if (totalRecords <= 0)
                throw new BusinessException("Não há registros no banco de dados.");

            return PaginationHelper.CreatePagedResponse(_mapper.Map<IEnumerable<UserResponse>>(users), request.PageNumber, request.PageSize, totalRecords);
        }

        public async Task<BaseResponse> DeleteUser(long id, StatusEnum status)
        {
            if (id <= 0)
                throw new BusinessException(Resource.InvalidRequest);

            if (!Enum.IsDefined(typeof(StatusEnum), status))
                throw new BusinessException(Resource.StatusNotFound);

            User? userEntity = await _userRepository.GetUserDetail(id);

            if (userEntity == null)
                throw new BusinessException(Resource.UserNotFound);

            userEntity.StatusId = (int)status;

            _userRepository.Update(userEntity);

            if (!await _userRepository.SaveAsync())
                throw new BusinessException(Resource.ErrorSaving);

            return new BaseResponse(HttpStatusCode.OK);
        }
    }
}