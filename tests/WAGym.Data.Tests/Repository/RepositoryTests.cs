using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Common.Tests.Mock;
using WAGym.Data.Repository.Interfaces;
using WAGym.Data.Repository;
using WAGym.Data.Tests.Data;

namespace WAGym.Data.Tests.Repository
{
    [TestClass]
    public class RepositoryTests
    {
        private IRepository<MockClass> _repository;
        private IEnumerable<MockClass> _data;

        [TestInitialize]
        public void Setup()
        {
            AppDbContextTests context = new AppDbContextTests();
            _repository = new Repository<MockClass>(context);

            Cleanup();

            _data = new List<MockClass>()
            {
                new MockClass() { Id = 1, Description = "Test 123" },
                new MockClass() { Id = 2, Description = "Test 321" },
                new MockClass() { Id = 3, Description = "Abcd" }
            };
            _repository.AddRange(_data);
            _repository.Save();
        }

        [TestCleanup]
        public void Cleanup()
        {
            IEnumerable<MockClass> result = _repository.GetAll();

            if (result.Any())
            {
                _repository.DeleteRange(result);
                _repository.Save();
            }
        }

        [TestMethod]
        public void Add_ValidRequest_ShouldReturnMockClass()
        {
            MockClass result = _repository.Add(_data.First());

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(MockClass));
            Assert.AreEqual(_data.First().Id, result.Id);
        }

        [TestMethod]
        public async Task AddAsync_ValidRequest_ShouldReturnMockClass()
        {
            MockClass result = await _repository.AddAsync(_data.First());

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(MockClass));
            Assert.AreEqual(_data.First().Id, result.Id);
        }

        [TestMethod]
        public void AddRange_ValidRequest_ShouldReturnListOfMockClass()
        {
            IEnumerable<MockClass> result = _repository.AddRange(_data);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<MockClass>));
            Assert.AreEqual(_data.Count(), result.Count());
        }

        [TestMethod]
        public async Task AddRangeAsync_ValidRequest_ShouldReturnListOfMockClass()
        {
            IEnumerable<MockClass> result = await _repository.AddRangeAsync(_data);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<MockClass>));
            Assert.AreEqual(_data.Count(), result.Count());
        }

        [TestMethod]
        public void GetAll_ShouldReturnListOfMockClass()
        {
            IEnumerable<MockClass> result = _repository.GetAll();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<MockClass>));
            Assert.AreEqual(_data.Count(), result.Count());
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnListOfMockClass()
        {
            IEnumerable<MockClass> result = await _repository.GetAllAsync();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<MockClass>));
            Assert.AreEqual(_data.Count(), result.Count());
        }

        [TestMethod]
        public void GetList_ShouldReturnListOfMockClass()
        {
            IEnumerable<MockClass> result = _repository.GetList(x => x.Description.Contains("Test"));

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<MockClass>));
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetListAsync_ShouldReturnListOfMockClass()
        {
            IEnumerable<MockClass> result = await _repository.GetListAsync(x => x.Description.Contains("Test"));

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<MockClass>));
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void Get_ValidRequest_ShouldReturnMockClass()
        {
            MockClass? result = _repository.Get(x => x.Id == 1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(MockClass));
            Assert.AreEqual(_data.First().Id, result.Id);
        }

        [TestMethod]
        public void Get_InvalidRequest_ShouldReturnNull() => Assert.IsNull(_repository.Get(x => x.Id == 4));

        [TestMethod]
        public async Task GetAsync_ValidRequest_ShouldReturnMockClass()
        {
            MockClass? result = await _repository.GetAsync(x => x.Id == 1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(MockClass));
            Assert.AreEqual(_data.First().Id, result.Id);
        }

        [TestMethod]
        public async Task GetAsync_InvalidRequest_ShouldReturnNull() => Assert.IsNull(await _repository.GetAsync(x => x.Id == 4));

        [TestMethod]
        public void Any_WithExistingId_ShouldReturnTrue()
        {
            bool result = _repository.Any(x => x.Id == 3);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Any_WithNonExistingId_ShouldReturnFalse()
        {
            bool result = _repository.Any(x => x.Id == 4);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task AnyAsync_WithExistingId_ShouldReturnTrue()
        {
            bool result = await _repository.AnyAsync(x => x.Id == 3);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task AnyAsync_WithNonExistingId_ShouldReturnFalse()
        {
            bool result = await _repository.AnyAsync(x => x.Id == 4);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Update_ValidRequest_ShouldReturnMockkClass()
        {
            MockClass updateData = _repository.Get(x => x.Id == 1);

            updateData.Description = "Abcd";
            _repository.Update(updateData);
            _repository.Save();
            MockClass result = _repository.Get(x => x.Id == 1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(MockClass));
        }

        [TestMethod]
        public void UpdateRange_ValidRequest_ShouldReturnListOfMockkClass()
        {
            IEnumerable<MockClass> updateData = _repository.GetAll();

            for (int i = 1; i < updateData.Count(); i++)
                updateData.ToList()[i].Description = $"Test {i}";

            _repository.UpdateRange(updateData);
            _repository.Save();
            IEnumerable<MockClass> result = _repository.GetAll();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<MockClass>));
            Assert.AreEqual(_data.Count(), result.Count());
        }

        [TestMethod]
        public void Delete_ShouldRemoveItemFromTable()
        {
            MockClass deleteItem = _repository.Get(x => x.Id == 1);

            _repository.Delete(deleteItem);
            _repository.Save();
            MockClass result = _repository.Get(x => x.Id == 1);

            Assert.IsNull(result);
            Assert.AreEqual(2, _repository.GetAll().Count());
        }

        [TestMethod]
        public void DeleteRange_ShouldRemoveMultipleItemsFromTable()
        {
            _repository.DeleteRange(_data);
            _repository.Save();
            IEnumerable<MockClass> result = _repository.GetAll();

            Assert.IsInstanceOfType(result, typeof(IEnumerable<MockClass>));
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void Save_WhenTheresItem_ShouldReturnTrue()
        {
            MockClass newData = new MockClass() { Id = 4, Description = "Test Description" };
            _repository.Add(newData);

            bool result = _repository.Save();

            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Save_WhenTheresNoItem_ShouldReturnFalse()
        {
            bool result = _repository.Save();

            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task SaveAsync_WhenTheresItem_ShouldReturnTrue()
        {
            MockClass newData = new MockClass() { Id = 4, Description = "Test Description" };
            await _repository.AddAsync(newData);

            bool result = await _repository.SaveAsync();

            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task SaveAsync_WhenTheresNoItem_ShouldReturnFalse()
        {
            bool result = await _repository.SaveAsync();

            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Count_ShouldReturnAmountOfItems()
        {
            Assert.AreEqual(2, _repository.Count(x => x.Description!.Contains("Test")));
        }

        [TestMethod]
        public async Task CountAsync_ShouldReturnAmountOfItems()
        {
            Assert.AreEqual(2, await _repository.CountAsync(x => x.Description!.Contains("Test")));
        }
    }
}
