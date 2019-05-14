using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoModels.Core.DataModels;
using TodoServices.Interfaces;
using TodoServices.Services;
using TodoUnitTest.TestDb;
using TodoWebAPI.Controllers;

namespace TodoUnitTest.ServiceTests
{
    [TestClass]
    public class TodoReadServiceUnitTest
    {
        private readonly ICrudService<Todo> _todoService;
        private readonly ITodoReadService _todoReadService;
        public readonly string connectionString = "Server=(local);Database=TodoDB;Trusted_Connection=True;MultipleActiveResultSets=True";

        public DbContextOptions<ApiDbContext> _apiDbContextOptions { get; private set; }

        public TodoReadServiceUnitTest()
        {
            _apiDbContextOptions = new DbContextOptionsBuilder<ApiDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var apiDbContext = new ApiDbContext(_apiDbContextOptions);

            TestDbContext testDbContext = new TestDbContext();
            testDbContext.CreateTestDb(apiDbContext);

            _todoService = new TodoService(apiDbContext);
            _todoReadService = new TodoReadService(_todoService);
        }

        [TestMethod]
        public void ReadByCategory_ValidId()
        {
            var category = TestData.Categories.ElementAt(0);
            var todos = _todoReadService.ReadByCategory(category.CategoryId).Result;

            Assert.IsInstanceOfType(todos, typeof(IQueryable<Todo>));
            Assert.IsTrue(todos.Count() != 0);
        }

        [TestMethod]
        public void ReadByCategory_InvalidId()
        {
            var todos = _todoReadService.ReadByCategory(Guid.Empty).Result;

            Assert.IsTrue(todos.Count() == 0);
        }

        [TestMethod]
        public void ReadOpenTodosAndLatest_ReturnValueIsValid()
        {
            var todos = _todoReadService.ReadOpenTodosAndLatests(5).Result;

            Assert.IsInstanceOfType(todos, typeof(IQueryable<Todo>));
            Assert.IsTrue(todos.Count() != 0);
        }

        [TestMethod]
        public void ReadGroupByCategory_ReturnValueIsValid()
        {
            var todos = _todoReadService.ReadGroupByCategory().Result;

            Assert.IsInstanceOfType(todos, typeof(Dictionary<string, List<Todo>>));
            Assert.IsTrue(todos.Count() != 0);
        }

        [TestMethod]
        public void ReadTree_ReturnValueIsValid()
        {
            var todos = _todoReadService.ReadTree().Result;

            Assert.IsInstanceOfType(todos, typeof(IEnumerable<TreeModel>));
            Assert.IsTrue(todos.Count() != 0);
            Assert.IsTrue(todos.ElementAt(0).ChildItems.ElementAt(0).ChildItems.Count != 0);
        }
    }
}
