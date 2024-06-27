using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Common.Model.Functionality.Response;
using WAGym.Common.Model.Login.Response;
using WAGym.Common.Model.Profile.Response;
using WAGym.Common.Model.Token;

namespace WAGym.Common.Tests.Model.Login.Response
{
    [TestClass]
    public class LoginResponseTests
    {
        private LoginResponse _response;
        private long _expectedCompanyId;
        private long _expectedPersonId;
        private long _expectedUserId;
        private Common.Model.Token.Token _expectedToken;
        private ProfileResponse _expectedProfile;

        [TestInitialize]
        public void Setup()
        {
            _expectedToken = new Common.Model.Token.Token(true, DateTime.Now, DateTime.Now, "accessToken", "refreshToken");
            _expectedProfile = new ProfileResponse()
            {
                Id = 1,
                Name = "Profile 1",
                Functionalities = new List<FunctionalityResponse>()
                {
                    new FunctionalityResponse
                    {
                        Id = 1,
                        Name = "Functionality 1"
                    },
                    new FunctionalityResponse
                    {
                        Id = 2,
                        Name = "Functionality 2"
                    }
                }
            };
            _expectedCompanyId = 1;
            _expectedPersonId = 1;
            _expectedUserId = 1;
            _response = new LoginResponse(_expectedCompanyId, _expectedPersonId, _expectedUserId, _expectedToken, _expectedProfile);
        }

        [TestMethod]
        public void CompanyId_StoresCorrectly()
        {
            Assert.AreEqual(_expectedCompanyId, _response.CompanyId);
            Assert.IsInstanceOfType(_response.CompanyId, typeof(long));
        }

        [TestMethod]
        public void PersonId_StoresCorrectly()
        {
            Assert.AreEqual(_expectedPersonId, _response.PersonId);
            Assert.IsInstanceOfType(_response.PersonId, typeof(long));
        }

        [TestMethod]
        public void UserId_StoresCorrectly()
        {
            Assert.AreEqual(_expectedUserId, _response.PersonId);
            Assert.IsInstanceOfType(_response.PersonId, typeof(long));
        }

        [TestMethod]
        public void Token_StoresCorrectly()
        {
            Assert.AreEqual(_expectedToken, _response.Token);
            Assert.IsInstanceOfType(_response.Token, typeof(Common.Model.Token.Token));
        }

        [TestMethod]
        public void Profiles_StoresCorrectly()
        {
            Assert.AreEqual(_expectedProfile, _response.Profile);
            Assert.IsInstanceOfType(_response.Profile, typeof(ProfileResponse));
        }
    }
}
