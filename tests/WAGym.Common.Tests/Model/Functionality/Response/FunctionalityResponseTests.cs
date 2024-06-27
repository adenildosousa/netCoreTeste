using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Common.Model.Functionality.Response;

namespace WAGym.Common.Tests.Model.Functionality.Response
{
    [TestClass]
    public class FunctionalityResponseTests
    {
        private FunctionalityResponse _response;

        [TestInitialize]
        public void Setup()
        {
            _response = new FunctionalityResponse();
        }

        [TestMethod]
        public void Id_StoresCorrectly()
        {
            int expected = 1;

            _response.Id = expected;

            Assert.AreEqual(expected, _response.Id);
            Assert.IsInstanceOfType(_response.Id, typeof(int));
        }

        [TestMethod]
        public void Name_StoresCorrectly()
        {
            string expected = "Name Test";

            _response.Name = expected;

            Assert.AreEqual(expected, _response.Name);
            Assert.IsInstanceOfType(_response.Name, typeof(string));
        }
    }
}
