using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using WAGym.Data.Data;

namespace WAGym.Data.Tests.Data
{
    [TestClass]
    public class FunctionalityTests
    {
        private Functionality _functionality;

        [TestInitialize]
        public void Setup()
        {
            _functionality = new Functionality();
        }

        [TestMethod]
        public void Id_StoresCorrectly()
        {
            int expected = 1;

            _functionality.Id = expected;

            Assert.AreEqual(expected, _functionality.Id);
            Assert.IsInstanceOfType(_functionality.Id, typeof(int));
        }

        [TestMethod]
        public void Name_StoresCorrectly() 
        {
            string expected = "Name Test";

            _functionality.Name = expected;

            Assert.AreEqual(expected, _functionality.Name);
            Assert.IsInstanceOfType(_functionality.Name, typeof(string));
        }

        [TestMethod]
        public void Description_StoresCorrectly()
        {
            string expected = "Description Test";

            _functionality.Description = expected;

            Assert.AreEqual(_functionality.Description, expected);
            Assert.IsInstanceOfType(_functionality.Description, typeof(string));
        }

        [TestMethod]
        public void Description_IsNull()
        {
            string? expected = null;

            _functionality.Description = expected;

            Assert.AreEqual(_functionality.Description, expected);
            Assert.IsNull(_functionality.Description);
        }

        [TestMethod]
        public void FunctionalityProfiles_StoresCollectionOfFunctionalityProfile()
        {
            _functionality.FunctionalityProfiles = new Collection<FunctionalityProfile>();

            Assert.IsInstanceOfType(_functionality.FunctionalityProfiles, typeof(Collection<FunctionalityProfile>));
        }
    }
}
