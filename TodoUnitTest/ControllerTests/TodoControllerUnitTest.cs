using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using TodoModels.Core.DataModels;
using TodoModels.Core.Settings;
using TodoServices.Interfaces;
using TodoServices.Services;
using TodoUnitTest.TestDb;
using TodoWebAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using TodoModels.Core.Enums;

namespace TodoUnitTest.ControllerTests
{
    [TestClass]
    public class TodoControllerUnitTest
    {
        private readonly Mock<ICrudService<Todo>> mockTodoService;
        private readonly Mock<ICrudService<Category>> mockCategoryService;
        private readonly Mock<ITodoReadService> mockTodoReadService;
        private readonly Mock<ITodoStoredProcedureService> mockStoredProcedureService;
        private readonly TodoController _todoController;

        public readonly string connectionString = "Server=(local);Database=TodoDB;Trusted_Connection=True;MultipleActiveResultSets=True";

        public DbContextOptions<ApiDbContext> _apiDbContextOptions { get; private set; }

        public TodoControllerUnitTest()
        {
            _apiDbContextOptions = new DbContextOptionsBuilder<ApiDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var apiDbContext = new ApiDbContext(_apiDbContextOptions);

            TestDbContext testDbContext = new TestDbContext();
            testDbContext.CreateTestDb(apiDbContext);

            mockTodoService = new Mock<ICrudService<Todo>>();
            mockCategoryService = new Mock<ICrudService<Category>>();
            mockTodoReadService = new Mock<ITodoReadService>();
            mockStoredProcedureService = new Mock<ITodoStoredProcedureService>();

            var mockTodoControllerSettings = new Mock<IOptions<TodoControllerSettings>>();
            mockTodoControllerSettings.Setup(x => x.Value).Returns(new TodoControllerSettings() { ReadLatestsIntervalInHours = 5 });

            mockTodoService.Setup(x => x.Read(It.IsAny<Guid>())).Returns(Task.FromResult(new Todo() { }));


            _todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);
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

            mockTodoService.Setup(x => x.Create(It.IsAny<Todo>())).Returns(Task.FromResult(todo));

            var createActionResult = _todoController.Create(todo).Result;
            var todoResult = createActionResult.Value;

            Assert.IsInstanceOfType(createActionResult.Result, typeof(OkObjectResult));
            Assert.AreNotEqual(Guid.Empty, todoResult.TodoId);
        }

        public void Create_InvalidData()
        {

        }
    }
}
