using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Common.Enum;
using WAGym.Common.Model.Pagination.Response;
using WAGym.Common.Model.User.Response;
using WAGym.Domain.Extension;

namespace WAGym.Common.Tests.Model.Pagination.Response
{
    [TestClass]
    public class PaginationResponseTests
    {
        private PaginationResponse<UserResponse> _paginationResponse;
        private IEnumerable<UserResponse> _users;
        private int _expectedPageNumber;
        private int _expectedPageSize;
        private int _expectedTotalPages;
        private int _expectedTotalRecords;

        [TestInitialize]
        public void Setup()
        {
            _expectedPageNumber = 1;
            _expectedPageSize = 1;
            _expectedTotalPages = 3;
            _expectedTotalRecords = 3;

            _users = new List<UserResponse>()
            {
                new UserResponse(1, 1, "user1", (int)StatusEnum.Active, StatusEnum.Active.GetName(), 10, "user10", 1),
                new UserResponse(2, 2, "user2", (int)StatusEnum.Active, StatusEnum.Active.GetName(), 10, "user10", 1),
                new UserResponse(3, 3, "user3", (int)StatusEnum.Active, StatusEnum.Active.GetName(), 10, "user10", 1),
            };

            _paginationResponse = new PaginationResponse<UserResponse>(
                _users, 
                _expectedPageNumber, 
                _expectedPageSize, 
                _expectedTotalPages, 
                _expectedTotalRecords);
        }


        [TestMethod]
        public void Data_StoresCorrectly()
        {
            Assert.AreEqual(_users.Count(), _paginationResponse.Data.Count());
            Assert.IsInstanceOfType(_paginationResponse.Data, typeof(IEnumerable<UserResponse>));
        }

        [TestMethod]
        public void PageNumber_StoresCorrectly()
        {
            Assert.AreEqual(_expectedPageNumber, _paginationResponse.PageNumber);
            Assert.IsInstanceOfType(_paginationResponse.PageNumber, typeof(int));
        }

        [TestMethod]
        public void PageSize_StoresCorrectly()
        {
            Assert.AreEqual(_expectedPageSize, _paginationResponse.PageSize);
            Assert.IsInstanceOfType(_paginationResponse.PageSize, typeof(int));
        }

        [TestMethod]
        public void TotalPages_StoresCorrectly()
        {
            Assert.AreEqual(_expectedTotalPages, _paginationResponse.TotalPages);
            Assert.IsInstanceOfType(_paginationResponse.TotalPages, typeof(int));
        }

        [TestMethod]
        public void TotalRecords_StoresCorrectly()
        {
            Assert.AreEqual(_expectedTotalRecords, _paginationResponse.TotalRecords);
            Assert.IsInstanceOfType(_paginationResponse.TotalRecords, typeof(int));
        }
    }
}
