using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Common.Model.Profile.Request;

namespace WAGym.Common.Tests.Model.Profile.Request
{
    [TestClass]
    public class ProfileRequestTests
    {
        private ProfileRequest _profileRequest;
        private long _expectedId;
        private bool _expectedIsDefault;


        [TestInitialize]
        public void Setup()
        {
            _expectedId = 1;
            _expectedIsDefault = true;

            _profileRequest = new ProfileRequest
            {
                Id = _expectedId,
                Default = _expectedIsDefault
            };
        }

        [TestMethod]
        public void Id_StoresCorrectly()
        {
            Assert.AreEqual(_expectedId, _profileRequest.Id);
            Assert.IsInstanceOfType(_profileRequest.Id, typeof(long));
        }

        [TestMethod]
        public void Default_StoresCorrectly()
        {
            Assert.AreEqual(_expectedIsDefault, _profileRequest.Default);
            Assert.IsInstanceOfType(_profileRequest.Default, typeof(bool));
        }
    }
}
