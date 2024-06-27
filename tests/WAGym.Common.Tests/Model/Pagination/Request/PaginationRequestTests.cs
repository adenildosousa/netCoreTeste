using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Common.Model.Pagination.Request;

namespace WAGym.Common.Tests.Model.Pagination.Request
{
    [TestClass]
    public class PaginationRequestTests
    {
        private PaginationRequest _paginationRequest;
        private long _expectedCompanyId;
        private int _expectedPageNumber;
        private int _expectedPageSize;

        [TestInitialize]
        public void Setup()
        {
            _expectedCompanyId = 1;
            _expectedPageNumber = 1;
            _expectedPageSize = 2;

            _paginationRequest = new PaginationRequest
            {
                CompanyId = _expectedCompanyId,
                PageNumber = _expectedPageNumber,
                PageSize = _expectedPageSize
            };
        }

        [TestMethod]
        public void CompanyId_StoresCorrectly()
        {
            Assert.AreEqual(_expectedCompanyId, _paginationRequest.CompanyId);
            Assert.IsInstanceOfType(_paginationRequest.CompanyId, typeof(long));
        }

        [TestMethod]
        public void PageNumber_StoresCorrectly()
        {
            Assert.AreEqual(_expectedPageNumber, _paginationRequest.PageNumber);
            Assert.IsInstanceOfType(_paginationRequest.PageNumber, typeof(int));
        }

        [TestMethod]
        public void PageSize_StoresCorrectly()
        {
            Assert.AreEqual(_expectedPageSize, _paginationRequest.PageSize);
            Assert.IsInstanceOfType(_paginationRequest.PageSize, typeof(int));
        }
    }
}
