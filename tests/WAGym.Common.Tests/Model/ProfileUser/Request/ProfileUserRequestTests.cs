using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Common.Model.Profile.Request;
using WAGym.Common.Model.ProfileUser.Request;

namespace WAGym.Common.Tests.Model.ProfileUser.Request
{
    [TestClass]
    public class ProfileUserRequestTests
    {
        private ProfileUserRequest _profileUserRequest;
        private long _expectedCompanyId;
        private long _expectedPersonId;
        private long _expectedUserId;

        [TestInitialize]
        public void Setup()
        {
            _expectedCompanyId = 1;
            _expectedPersonId = 1;
            _expectedUserId = 1;

            _profileUserRequest = new ProfileUserRequest
            {
                CompanyId = _expectedCompanyId,
                PersonId = _expectedPersonId,
                UserId = _expectedUserId
            };
        }

        [TestMethod]
        public void CompanyId_StoresCorrectly()
        {
            Assert.AreEqual(_expectedCompanyId, _profileUserRequest.CompanyId);
            Assert.IsInstanceOfType(_profileUserRequest.CompanyId, typeof(long));
        }

        [TestMethod]
        public void PersonId_StoresCorrectly()
        {
            Assert.AreEqual(_expectedPersonId, _profileUserRequest.PersonId);
            Assert.IsInstanceOfType(_profileUserRequest.PersonId, typeof(long));
        }

        [TestMethod]
        public void UserId_StoresCorrectly()
        {
            Assert.AreEqual(_expectedUserId, _profileUserRequest.UserId);
            Assert.IsInstanceOfType(_profileUserRequest.UserId, typeof(long));
        }

        [TestMethod]
        public void Profiles_StoresCorrectly()
        {
            Assert.IsInstanceOfType(_profileUserRequest.Profiles, typeof(List<ProfileRequest>));
        }
    }
}
