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

namespace TodoUnitTest.ServiceTests
{
    [TestClass]
    public class TodoStoredProcedureUnitTest
    {
        private readonly ITodoStoredProcedureService _todoStoredProcedureService;

        public readonly string connectionString = "Server=(local);Database=TodoDB;Trusted_Connection=True;MultipleActiveResultSets=True";

        public DbContextOptions<ApiDbContext> _apiDbContextOptions { get; private set; }

        public TodoStoredProcedureUnitTest()
        {
            _apiDbContextOptions = new DbContextOptionsBuilder<ApiDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var apiDbContext = new ApiDbContext(_apiDbContextOptions);

            TestDbContext testDbContext = new TestDbContext();
            testDbContext.CreateTestDb(apiDbContext);

            _todoStoredProcedureService = new TodoStoredProcedureService(apiDbContext);
        }

        [TestMethod]
        public void ReadOnlyIdAndName__ReturnValueIsValid()
        {
            var todos = _todoStoredProcedureService.ReadOnlyIdAndName().Result;

            Assert.IsInstanceOfType(todos, typeof(IQueryable<TodoHeader>));
            Assert.IsTrue(todos.Count() != 0);
        }
    }
}
