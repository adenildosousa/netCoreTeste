using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Common.Model.Login.Response;

namespace WAGym.Common.Tests.Model.Login.Response
{
    [TestClass]
    public class LoginProfileResponseTests
    {
        private LoginProfileResponse _loginProfileResponse;

        [TestInitialize] 
        public void Setup() 
        {
            _loginProfileResponse = new LoginProfileResponse(true);
        }

        [TestMethod]
        public void SetMessage_StoresCorrectly()
        {
            string expectedMessage = Resource.Resource.ErrorSaving;
            _loginProfileResponse.SetMessage(expectedMessage);

            Assert.AreEqual(Resource.Resource.ErrorSaving, _loginProfileResponse.Message);
            Assert.IsInstanceOfType(_loginProfileResponse.Message, typeof(string));
        }
    }
}
