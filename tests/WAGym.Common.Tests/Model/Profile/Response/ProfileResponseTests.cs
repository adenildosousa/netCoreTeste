using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Common.Model.Functionality.Response;
using WAGym.Common.Model.Profile.Response;

namespace WAGym.Common.Tests.Model.Profile.Response
{
    [TestClass]
    public class ProfileResponseTests
    {
        private ProfileResponse _response;

        [TestInitialize]
        public void Setup()
        {
            _response = new ProfileResponse();
        }

        [TestMethod]
        public void Id_StoresCorrectly()
        {
            long expected = 1;

            _response.Id = expected;

            Assert.AreEqual(expected, _response.Id);
            Assert.IsInstanceOfType(_response.Id, typeof(long));
        }

        [TestMethod]
        public void Name_StoresCorrectly()
        {
            string expected = "Name Test";

            _response.Name = expected;

            Assert.AreEqual(expected, _response.Name);
            Assert.IsInstanceOfType(_response.Name, typeof(string));
        }

        [TestMethod]
        public void Functionalities_StoresCorrectly()
        {
            IEnumerable<FunctionalityResponse> expected = new List<FunctionalityResponse>()
            {
                new FunctionalityResponse { Id = 1, Name = "Functionality 1" },
                new FunctionalityResponse { Id = 2, Name = "Functionality 2" },
            };

            _response.Functionalities = expected;

            Assert.AreEqual(expected, _response.Functionalities);
            Assert.IsInstanceOfType(_response.Functionalities, typeof(IEnumerable<FunctionalityResponse>));
        }
    }
}
