using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Design;
using WAGym.Common.Enum;
using WAGym.Common.Model.User.Request;
using WAGym.Common.Model.User.Response;
using WAGym.Data.Data;
using WAGym.Domain.AutoMapper;
using WAGym.Domain.Extension;

namespace WAGym.Domain.Tests.AutoMapper
{
    [TestClass]
    public class UserMappingTests
    {
        private IMapper _mapper;
        private string _expectedUsername;
        private string _expectedPassword;
        private long _expectedCompanyId;

        [TestInitialize]
        public void Setup()
        {
            if (_mapper == null)
            {
                MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new UserMapping());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _expectedUsername = "username";
            _expectedPassword = "123";
            _expectedCompanyId = 1;
        }

        [TestMethod]
        public void UserMapping_ShouldMap_UserRequestToUser()
        {
            UserRequest dto = new UserRequest(1, _expectedUsername, _expectedPassword, _expectedCompanyId);
            User entity = _mapper.Map<User>(dto);

            Assert.IsNotNull(entity);
            Assert.IsInstanceOfType(entity, typeof(User));
            Assert.AreEqual(dto.PersonId, entity.PersonId);
            Assert.AreEqual(dto.Username, entity.Username);
            Assert.AreEqual(dto.Password, entity.Password);
        }

        [TestMethod]
        public void UserMapping_ShouldMap_PutUserRequestToUser()
        {
            PutUserRequest dto = new PutUserRequest(_expectedUsername, _expectedPassword, StatusEnum.Active);
            User entity = _mapper.Map<User>(dto);

            Assert.IsNotNull(entity);
            Assert.IsInstanceOfType(entity, typeof(User));
            Assert.AreEqual(dto.Username, entity.Username);
            Assert.AreEqual(dto.Password, entity.Password);
            Assert.AreEqual((int)dto.Status, entity.StatusId);
        }

        [TestMethod]
        public void UserMapping_ShouldMap_UserToUserResponse()
        {
            User entity = new User
            {
                Id = 1,
                PersonId = 1,
                Username = "username",
                Password = "123",
                StatusId = (int)StatusEnum.Active,
                UserUpdateId = 1,
                UserUpdate = new User
                {
                    Id = 1,
                    Username = "username2"
                },
                CompanyId = 1
            };
            UserResponse dto = _mapper.Map<UserResponse>(entity);

            Assert.IsNotNull(dto);
            Assert.IsInstanceOfType(dto, typeof(UserResponse));
            Assert.AreEqual(entity.StatusId, dto.StatusId);
            Assert.AreEqual(((StatusEnum)entity.StatusId).GetName(), dto.Status);
            Assert.AreEqual(entity.UserUpdate.Username, dto.UserUpdate);
            Assert.AreEqual(entity.UserUpdateId, dto.UserUpdateId);
        }
    }
}
