using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoModels.Core.DataModels;
using TodoServices.Interfaces;
using TodoServices.Services;
using TodoModels.Core.Enums;
using TodoUnitTest.TestDb;

namespace TodoUnitTest.ServiceTests
{
    [TestClass]
    public class TodoServiceUnitTest
    {
        private readonly ICrudService<Todo> _todoService;
        public readonly string connectionString = "Server=(local);Database=TodoDB;Trusted_Connection=True;MultipleActiveResultSets=True";

        public DbContextOptions<ApiDbContext> _apiDbContextOptions { get; private set; }

        public TodoServiceUnitTest()
        {
            _apiDbContextOptions = new DbContextOptionsBuilder<ApiDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var apiDbContext = new ApiDbContext(_apiDbContextOptions);

            TestDbContext testDbContext = new TestDbContext();
            testDbContext.CreateTestDb(apiDbContext);

            _todoService = new TodoService(apiDbContext);
        }

        [TestMethod]
        public void Create_ValidData()
        {
            var todo = new Todo()
            {
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Name = "Csattogóslepke kielemzése két hurkapálcával",
                Description = "Egy vonatkerékabroncs gyárban.",
                Priority = PriorityEnum.High,
                Status = StatusEnum.Completed,
                Deadline = new DateTime(2019, 2, 12, 5, 30, 0),
                Created = new DateTime(2018, 12, 31, 23, 59, 30),
                Creator = "Teszt Elek",
                LastModified = new DateTime(2018, 12, 31, 23, 59, 30),
                Modifier = "Teszt Elek"
            };

            var createdTodo = _todoService.Create(todo).Result;
            Assert.AreNotEqual(Guid.Empty, createdTodo.TodoId);
        }

        [TestMethod]
        public void Create_InvalidData()
        {
            var todo = new Todo()
            {
                TodoId = Guid.Parse("151d0698-0253-4c3b-a11b-86902d225fe4"),
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Description = "Egy vonatkerékabroncs gyárban.",
                Priority = PriorityEnum.High,
                Status = StatusEnum.Completed,
                Deadline = new DateTime(2019, 2, 12, 5, 30, 0),
                Created = new DateTime(2018, 12, 31, 23, 59, 30),
                Creator = "Teszt Elek",
                LastModified = new DateTime(2018, 12, 31, 23, 59, 30),
                Modifier = "Teszt Elek"
            };

            Assert.ThrowsException<AggregateException>(() => _todoService.Create(todo).Result);
        }

        [TestMethod]
        public void Update_ValidData()
        {
            var todoId = TestData.Todos.ElementAt(1).TodoId;
            var newName = "Tök másra átnevezzük Unit Test-ben.";
            var todo = _todoService.ReadNotTracked(todoId).Result;

            todo.Name = newName;
            todo = _todoService.Update(todoId, todo).Result;

            Assert.AreEqual(todoId, todo.TodoId);
            Assert.AreEqual(newName, todo.Name);
        }

        [TestMethod]
        public void Update_InvalidData()
        {
            var todoId = TestData.Todos.ElementAt(1).TodoId;
            string newName = null;
            var todo = _todoService.ReadNotTracked(todoId).Result;
            todo.Name = newName;

            Assert.ThrowsException<AggregateException>(() => _todoService.Update(todoId, todo).Result);
        }

        [TestMethod]
        public void Update_InvalidId()
        {
            var todo = _todoService.Update(Guid.Empty, null).Result;

            Assert.IsNull(todo);
        }

        [TestMethod]
        public void Delete_ValidId()
        {
            var todoId = TestData.Todos.ElementAt(10).TodoId;
            
            var todo = _todoService.Delete(todoId).Result;

            Assert.AreEqual(todoId, todo.TodoId);
            Assert.IsTrue(todo.Deleted);
        }

        [TestMethod]
        public void Delete_InvalidId()
        {
            var todo = _todoService.Delete(Guid.Empty).Result;

            Assert.IsNull(todo);
        }

        [TestMethod]
        public void Read_ReadAll()
        {
            var todos = _todoService.Read().Result;

            Assert.IsInstanceOfType(todos, typeof(IQueryable<Todo>));
            Assert.IsTrue(todos.Count() != 0);
        }

        [TestMethod]
        public void Read_ValidId()
        {
            var todo = TestData.Todos.ElementAt(0);
            var todoInDb = _todoService.Read(todo.TodoId).Result;
 
            Assert.AreNotSame(todo, todoInDb);
        }

        [TestMethod]
        public void Read_InvalidId()
        {
            var todo = _todoService.Read(Guid.Empty).Result;

            Assert.IsNull(todo);
        }
    }
}
