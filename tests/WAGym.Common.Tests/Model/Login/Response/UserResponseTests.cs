using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Common.Enum;
using WAGym.Common.Model.User.Response;

namespace WAGym.Common.Tests.Model.Login.Response
{
    [TestClass]
    public class UserResponseTests
    {
        private UserResponse _userResponse;

        [TestInitialize]
        public void Setup()
        {
            _userResponse = new UserResponse(1, 1, "username", (int)StatusEnum.Active, StatusEnum.Active.ToString(), 1, "username", 1);
        }

        [TestMethod]
        public void Id_StoresCorrectly()
        {
            Assert.IsNotNull(_userResponse);
            Assert.IsInstanceOfType(_userResponse.Id, typeof(long));
        }

        [TestMethod]
        public void PersonId_StoresCorrectly()
        {
            Assert.IsNotNull(_userResponse);
            Assert.IsInstanceOfType(_userResponse.PersonId, typeof(long));
        }

        [TestMethod]
        public void Username_StoresCorrectly()
        {
            Assert.IsNotNull(_userResponse);
            Assert.IsInstanceOfType(_userResponse.Username, typeof(string));
        }

        [TestMethod]
        public void StatusId_StoresCorrectly()
        {
            Assert.IsNotNull(_userResponse);
            Assert.IsInstanceOfType(_userResponse.StatusId, typeof(int));
        }

        [TestMethod]
        public void Status_StoresCorrectly()
        {
            Assert.IsNotNull(_userResponse);
            Assert.IsInstanceOfType(_userResponse.Status, typeof(string));
        }

        [TestMethod]
        public void UserUpdateId_StoresCorrectly()
        {
            Assert.IsNotNull(_userResponse);
            Assert.IsInstanceOfType(_userResponse.UserUpdateId, typeof(long));
        }

        [TestMethod]
        public void UserUpdate_StoresCorrectly()
        {
            Assert.IsNotNull(_userResponse);
            Assert.IsInstanceOfType(_userResponse.UserUpdate, typeof(string));
        }

        [TestMethod]
        public void CompanyId_StoresCorrectly()
        {
            Assert.IsNotNull(_userResponse);
            Assert.IsInstanceOfType(_userResponse.CompanyId, typeof(long));
        }
    }
}
