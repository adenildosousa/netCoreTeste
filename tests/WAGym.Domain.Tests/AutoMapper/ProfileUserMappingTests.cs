using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Common.Model.ProfileUser.Request;
using WAGym.Data.Data;
using WAGym.Domain.AutoMapper;

namespace WAGym.Domain.Tests.AutoMapper
{
    [TestClass]
    public class ProfileUserMappingTests
    {
        private IMapper _mapper;
        private long _expectedCompanyId;
        private long _expectedPersonId;
        private long _expectedUserId;

        [TestInitialize]
        public void Setup()
        {
            if (_mapper == null)
            {
                MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new ProfileUserMapping());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _expectedCompanyId = 1;
            _expectedPersonId = 1;
            _expectedUserId = 1;
        }

        [TestMethod]
        public void ProfileUserMapping_ShouldMap_ProfileUserRequestToProfileUser()
        {
            ProfileUserRequest dto = new ProfileUserRequest
            {
                CompanyId = _expectedCompanyId,
                PersonId = _expectedPersonId,
                UserId = _expectedUserId
            };
            ProfileUser entity = _mapper.Map<ProfileUser>(dto);

            Assert.IsNotNull(entity);
            Assert.IsInstanceOfType(entity, typeof(ProfileUser));
            Assert.AreEqual(dto.CompanyId, entity.CompanyId);
            Assert.AreEqual(dto.PersonId, entity.PersonId);
            Assert.AreEqual(dto.UserId, entity.UserId);
        }
    }
}
