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
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace TodoUnitTest.ControllerTests
{
    [TestClass]
    public class TodoControllerUnitTest
    {
        private readonly Mock<IOptions<TodoControllerSettings>> mockTodoControllerSettings;

        public TodoControllerUnitTest()
        {
            mockTodoControllerSettings = new Mock<IOptions<TodoControllerSettings>>();
            mockTodoControllerSettings.Setup(x => x.Value).Returns(new TodoControllerSettings() { ReadLatestsIntervalInHours = 5 });
        }

        [TestMethod]
        public void Create_ValidData()
        {
            var mockTodoService = new Mock<ICrudService<Todo>>();
            var mockCategoryService = new Mock<ICrudService<Category>>();
            var mockTodoReadService = new Mock<ITodoReadService>();
            var mockTodoStoredProcedureService = new Mock<ITodoStoredProcedureService>();
            TodoController todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockTodoStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);

            var todo = new Todo()
            {
                TodoId = Guid.NewGuid(),
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

            var createActionResult = todoController.Create(todo).Result as OkObjectResult;
            var todoResult = createActionResult.Value as Todo;

            Assert.IsInstanceOfType(createActionResult, typeof(OkObjectResult));
            Assert.AreNotEqual(Guid.Empty, todoResult.TodoId);
        }

        [TestMethod]
        public void Create_CreationFailed()
        {
            var mockTodoService = new Mock<ICrudService<Todo>>();
            var mockCategoryService = new Mock<ICrudService<Category>>();
            var mockTodoReadService = new Mock<ITodoReadService>();
            var mockTodoStoredProcedureService = new Mock<ITodoStoredProcedureService>();
            TodoController todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockTodoStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);

            var todo = new Todo()
            {
                TodoId = Guid.NewGuid(),
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

            mockTodoService.Setup(x => x.Create(It.IsAny<Todo>())).Returns(Task.FromResult<Todo>(null));
            var createActionResult = todoController.Create(todo).Result as BadRequestObjectResult;

            Assert.IsInstanceOfType(createActionResult, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void Update_ValidUpdate()
        {
            var mockTodoService = new Mock<ICrudService<Todo>>();
            var mockCategoryService = new Mock<ICrudService<Category>>();
            var mockTodoReadService = new Mock<ITodoReadService>();
            var mockTodoStoredProcedureService = new Mock<ITodoStoredProcedureService>();
            TodoController todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockTodoStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);

            var todo = new Todo()
            {
                TodoId = Guid.NewGuid(),
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Name = "Tök másra átnevezzük Unit Test-ben.",
                Description = "Egy vonatkerékabroncs gyárban.",
                Priority = PriorityEnum.High,
                Status = StatusEnum.InProgress,
                Deadline = new DateTime(2019, 2, 12, 5, 30, 0),
                Created = new DateTime(2018, 12, 31, 23, 59, 30),
                Creator = "Teszt Elek",
                LastModified = new DateTime(2018, 12, 31, 23, 59, 30),
                Modifier = "Teszt Elek"
            };

            var originalTodo = new Todo()
            {
                TodoId = Guid.NewGuid(),
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Name = "Update előtt voltam.",
                Description = "Egy vonatkerékabroncs gyárban.",
                Priority = PriorityEnum.High,
                Status = StatusEnum.InProgress,
                Deadline = new DateTime(2019, 2, 12, 5, 30, 0),
                Created = new DateTime(2018, 12, 31, 23, 59, 30),
                Creator = "Teszt Elek",
                LastModified = new DateTime(2018, 12, 31, 23, 59, 30),
                Modifier = "Teszt Elek"
            };

            mockTodoService.Setup(x => x.ReadNotTracked(It.IsAny<Guid>())).Returns(Task.FromResult(originalTodo));
            mockTodoService.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<Todo>())).Returns(Task.FromResult(todo));

            var updateActionResult = todoController.Update(todo.TodoId, todo).Result as OkObjectResult;
            var todoResult = updateActionResult.Value as Todo;

            Assert.IsInstanceOfType(updateActionResult, typeof(OkObjectResult));
            Assert.IsNotNull(todoResult);
            Assert.AreNotEqual(todoResult.Name, originalTodo.Name);
        }

        [TestMethod]
        public void Update_InvalidTodoId()
        {
            var mockTodoService = new Mock<ICrudService<Todo>>();
            var mockCategoryService = new Mock<ICrudService<Category>>();
            var mockTodoReadService = new Mock<ITodoReadService>();
            var mockTodoStoredProcedureService = new Mock<ITodoStoredProcedureService>();
            TodoController todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockTodoStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);

            var todo = new Todo()
            {
                TodoId = Guid.NewGuid(),
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Name = "Tök másra átnevezzük Unit Test-ben.",
                Description = "Egy vonatkerékabroncs gyárban.",
                Priority = PriorityEnum.High,
                Status = StatusEnum.InProgress,
                Deadline = new DateTime(2019, 2, 12, 5, 30, 0),
                Created = new DateTime(2018, 12, 31, 23, 59, 30),
                Creator = "Teszt Elek",
                LastModified = new DateTime(2018, 12, 31, 23, 59, 30),
                Modifier = "Teszt Elek"
            };

            var updateActionResult = todoController.Update(Guid.NewGuid(), todo).Result as BadRequestResult;

            Assert.IsInstanceOfType(updateActionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Update_InvalidData()
        {
            var mockTodoService = new Mock<ICrudService<Todo>>();
            var mockCategoryService = new Mock<ICrudService<Category>>();
            var mockTodoReadService = new Mock<ITodoReadService>();
            var mockTodoStoredProcedureService = new Mock<ITodoStoredProcedureService>();
            TodoController todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockTodoStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);

            var todo = new Todo()
            {
                TodoId = Guid.NewGuid(),
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Description = "Egy vonatkerékabroncs gyárban.",
                Priority = PriorityEnum.High,
                Status = StatusEnum.InProgress,
                Deadline = new DateTime(2019, 2, 12, 5, 30, 0),
                Created = new DateTime(2018, 12, 31, 23, 59, 30),
                Creator = "Teszt Elek",
                LastModified = new DateTime(2018, 12, 31, 23, 59, 30),
                Modifier = "Teszt Elek"
            };

            todoController.ModelState.AddModelError("teszt", "nincs név");
            var updateActionResult = todoController.Update(todo.TodoId, todo).Result as BadRequestObjectResult;
            var updateResponseValue = updateActionResult.Value;

            Assert.IsInstanceOfType(updateActionResult, typeof(BadRequestObjectResult));
            Assert.IsNotNull(updateResponseValue);
        }

        [TestMethod]
        public void Update_ValidData()
        {
            var todo = new Todo()
            {
                TodoId = Guid.NewGuid(),
                Name = "Csattogóslepke kielemzése két hurkapálcával",
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Description = "Egy vonatkerékabroncs gyárban.",
                Priority = PriorityEnum.High,
                Status = StatusEnum.InProgress,
                Deadline = new DateTime(2019, 2, 12, 5, 30, 0),
                Created = new DateTime(2018, 12, 31, 23, 59, 30),
                Creator = "Teszt Elek",
                LastModified = new DateTime(2018, 12, 31, 23, 59, 30),
                Modifier = "Teszt Elek"
            };

            var context = new ValidationContext(todo, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(todo, context, results, true);

            Assert.IsTrue(isModelStateValid && results.Count == 0);
        }

        [TestMethod]
        public void Update_TodoNotInDb()
        {
            var mockTodoService = new Mock<ICrudService<Todo>>();
            var mockCategoryService = new Mock<ICrudService<Category>>();
            var mockTodoReadService = new Mock<ITodoReadService>();
            var mockTodoStoredProcedureService = new Mock<ITodoStoredProcedureService>();
            TodoController todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockTodoStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);

            var todo = new Todo()
            {
                TodoId = Guid.NewGuid(),
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Name = "Tök másra átnevezzük Unit Test-ben.",
                Description = "Egy vonatkerékabroncs gyárban.",
                Priority = PriorityEnum.High,
                Status = StatusEnum.InProgress,
                Deadline = new DateTime(2019, 2, 12, 5, 30, 0),
                Created = new DateTime(2018, 12, 31, 23, 59, 30),
                Creator = "Teszt Elek",
                LastModified = new DateTime(2018, 12, 31, 23, 59, 30),
                Modifier = "Teszt Elek"
            };

            mockTodoService.Setup(x => x.ReadNotTracked(It.IsAny<Guid>())).Returns(Task.FromResult<Todo>(null));

            var updateActionResult = todoController.Update(todo.TodoId, todo).Result as NotFoundResult;

            Assert.IsInstanceOfType(updateActionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Update_CompletedOriginalWithDifferentStatusTodo()
        {
            var mockTodoService = new Mock<ICrudService<Todo>>();
            var mockCategoryService = new Mock<ICrudService<Category>>();
            var mockTodoReadService = new Mock<ITodoReadService>();
            var mockTodoStoredProcedureService = new Mock<ITodoStoredProcedureService>();
            TodoController todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockTodoStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);

            var todo = new Todo()
            {
                TodoId = Guid.NewGuid(),
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Name = "Tök másra átnevezzük Unit Test-ben.",
                Description = "Egy vonatkerékabroncs gyárban.",
                Priority = PriorityEnum.High,
                Status = StatusEnum.InProgress,
                Deadline = new DateTime(2019, 2, 12, 5, 30, 0),
                Created = new DateTime(2018, 12, 31, 23, 59, 30),
                Creator = "Teszt Elek",
                LastModified = new DateTime(2018, 12, 31, 23, 59, 30),
                Modifier = "Teszt Elek"
            };

            var originalTodo = new Todo()
            {
                TodoId = Guid.NewGuid(),
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Name = "Update előtt voltam.",
                Description = "Egy vonatkerékabroncs gyárban.",
                Priority = PriorityEnum.High,
                Status = StatusEnum.Completed,
                Deadline = new DateTime(2019, 2, 12, 5, 30, 0),
                Created = new DateTime(2018, 12, 31, 23, 59, 30),
                Creator = "Teszt Elek",
                LastModified = new DateTime(2018, 12, 31, 23, 59, 30),
                Modifier = "Teszt Elek"
            };

            mockTodoService.Setup(x => x.ReadNotTracked(It.IsAny<Guid>())).Returns(Task.FromResult(originalTodo));

            var updateActionResult = todoController.Create(todo).Result as BadRequestObjectResult;
            var updateResponseValue = updateActionResult.Value;

            Assert.IsInstanceOfType(updateActionResult, typeof(BadRequestObjectResult));
            Assert.IsNotNull(updateResponseValue);
        }

        [TestMethod]
        public void Update_CompletedOriginalWithDifferentPriorityTodo()
        {
            var mockTodoService = new Mock<ICrudService<Todo>>();
            var mockCategoryService = new Mock<ICrudService<Category>>();
            var mockTodoReadService = new Mock<ITodoReadService>();
            var mockTodoStoredProcedureService = new Mock<ITodoStoredProcedureService>();
            TodoController todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockTodoStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);

            var todo = new Todo()
            {
                TodoId = Guid.NewGuid(),
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Name = "Tök másra átnevezzük Unit Test-ben.",
                Description = "Egy vonatkerékabroncs gyárban.",
                Priority = PriorityEnum.High,
                Status = StatusEnum.InProgress,
                Deadline = new DateTime(2019, 2, 12, 5, 30, 0),
                Created = new DateTime(2018, 12, 31, 23, 59, 30),
                Creator = "Teszt Elek",
                LastModified = new DateTime(2018, 12, 31, 23, 59, 30),
                Modifier = "Teszt Elek"
            };

            var originalTodo = new Todo()
            {
                TodoId = Guid.NewGuid(),
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Name = "Update előtt voltam.",
                Description = "Egy vonatkerékabroncs gyárban.",
                Priority = PriorityEnum.High,
                Status = StatusEnum.Completed,
                Deadline = new DateTime(2019, 2, 12, 5, 30, 0),
                Created = new DateTime(2018, 12, 31, 23, 59, 30),
                Creator = "Teszt Elek",
                LastModified = new DateTime(2018, 12, 31, 23, 59, 30),
                Modifier = "Teszt Elek"
            };

            mockTodoService.Setup(x => x.ReadNotTracked(It.IsAny<Guid>())).Returns(Task.FromResult(originalTodo));

            var updateActionResult = todoController.Create(todo).Result as BadRequestObjectResult;
            var updateResponseValue = updateActionResult.Value;

            Assert.IsInstanceOfType(updateActionResult, typeof(BadRequestObjectResult));
            Assert.IsNotNull(updateResponseValue);
        }

        [TestMethod]
        public void Update_CompletedOriginalWithDifferentPriorityAndStatusTodo()
        {
            var mockTodoService = new Mock<ICrudService<Todo>>();
            var mockCategoryService = new Mock<ICrudService<Category>>();
            var mockTodoReadService = new Mock<ITodoReadService>();
            var mockTodoStoredProcedureService = new Mock<ITodoStoredProcedureService>();
            TodoController todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockTodoStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);

            var todo = new Todo()
            {
                TodoId = Guid.NewGuid(),
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Name = "Tök másra átnevezzük Unit Test-ben.",
                Description = "Egy vonatkerékabroncs gyárban.",
                Priority = PriorityEnum.Low,
                Status = StatusEnum.Completed,
                Deadline = new DateTime(2019, 2, 12, 5, 30, 0),
                Created = new DateTime(2018, 12, 31, 23, 59, 30),
                Creator = "Teszt Elek",
                LastModified = new DateTime(2018, 12, 31, 23, 59, 30),
                Modifier = "Teszt Elek"
            };

            var originalTodo = new Todo()
            {
                TodoId = Guid.NewGuid(),
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Name = "Update előtt voltam.",
                Description = "Egy vonatkerékabroncs gyárban.",
                Priority = PriorityEnum.High,
                Status = StatusEnum.Completed,
                Deadline = new DateTime(2019, 2, 12, 5, 30, 0),
                Created = new DateTime(2018, 12, 31, 23, 59, 30),
                Creator = "Teszt Elek",
                LastModified = new DateTime(2018, 12, 31, 23, 59, 30),
                Modifier = "Teszt Elek"
            };

            mockTodoService.Setup(x => x.ReadNotTracked(It.IsAny<Guid>())).Returns(Task.FromResult(originalTodo));

            var updateActionResult = todoController.Create(todo).Result as BadRequestObjectResult;
            var updateResponseValue = updateActionResult.Value;

            Assert.IsInstanceOfType(updateActionResult, typeof(BadRequestObjectResult));
            Assert.IsNotNull(updateResponseValue);
        }

        [TestMethod]
        public void Delete_ValidId()
        {
            var mockTodoService = new Mock<ICrudService<Todo>>();
            var mockCategoryService = new Mock<ICrudService<Category>>();
            var mockTodoReadService = new Mock<ITodoReadService>();
            var mockTodoStoredProcedureService = new Mock<ITodoStoredProcedureService>();
            TodoController todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockTodoStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);

            var todo = new Todo()
            {
                TodoId = Guid.NewGuid(),
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Name = "Tök másra átnevezzük Unit Test-ben.",
                Description = "Egy vonatkerékabroncs gyárban.",
                Priority = PriorityEnum.High,
                Status = StatusEnum.InProgress,
                Deadline = new DateTime(2019, 2, 12, 5, 30, 0),
                Created = new DateTime(2018, 12, 31, 23, 59, 30),
                Creator = "Teszt Elek",
                LastModified = new DateTime(2018, 12, 31, 23, 59, 30),
                Modifier = "Teszt Elek"
            };

            mockTodoService.Setup(x => x.Delete(It.IsAny<Guid>())).Returns(Task.FromResult(todo));

            var deleteActionResult = todoController.DeleteTodo(todo.TodoId).Result as NoContentResult;

            Assert.IsInstanceOfType(deleteActionResult, typeof(NoContentResult));
        }

        [TestMethod]
        public void Delete_InvalidId()
        {
            var mockTodoService = new Mock<ICrudService<Todo>>();
            var mockCategoryService = new Mock<ICrudService<Category>>();
            var mockTodoReadService = new Mock<ITodoReadService>();
            var mockTodoStoredProcedureService = new Mock<ITodoStoredProcedureService>();
            TodoController todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockTodoStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);

            var todo = new Todo()
            {
                TodoId = Guid.NewGuid(),
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Name = "Tök másra átnevezzük Unit Test-ben.",
                Description = "Egy vonatkerékabroncs gyárban.",
                Priority = PriorityEnum.High,
                Status = StatusEnum.InProgress,
                Deadline = new DateTime(2019, 2, 12, 5, 30, 0),
                Created = new DateTime(2018, 12, 31, 23, 59, 30),
                Creator = "Teszt Elek",
                LastModified = new DateTime(2018, 12, 31, 23, 59, 30),
                Modifier = "Teszt Elek"
            };

            mockTodoService.Setup(x => x.Delete(It.IsAny<Guid>())).Returns(Task.FromResult<Todo>(null));
            var deleteActionResult = todoController.DeleteTodo(todo.TodoId).Result as NotFoundResult;

            Assert.IsInstanceOfType(deleteActionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Read_ReadAll()
        {
            var mockTodoService = new Mock<ICrudService<Todo>>();
            var mockCategoryService = new Mock<ICrudService<Category>>();
            var mockTodoReadService = new Mock<ITodoReadService>();
            var mockTodoStoredProcedureService = new Mock<ITodoStoredProcedureService>();
            TodoController todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockTodoStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);

            mockTodoService.Setup(x => x.Read()).Returns(Task.FromResult(TestData.Todos.AsQueryable()));

            var readActionResult = todoController.Read().Result as OkObjectResult;
            var todoResult = readActionResult.Value as IEnumerable<Todo>;

            Assert.IsInstanceOfType(readActionResult, typeof(OkObjectResult));
            Assert.IsTrue(todoResult.Count() != 0);
        }

        [TestMethod]
        public void Read_ValidId()
        {
            var mockTodoService = new Mock<ICrudService<Todo>>();
            var mockCategoryService = new Mock<ICrudService<Category>>();
            var mockTodoReadService = new Mock<ITodoReadService>();
            var mockTodoStoredProcedureService = new Mock<ITodoStoredProcedureService>();
            TodoController todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockTodoStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);

            var todo = new Todo()
            {
                TodoId = Guid.NewGuid(),
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Name = "Tök másra átnevezzük Unit Test-ben.",
                Description = "Egy vonatkerékabroncs gyárban.",
                Priority = PriorityEnum.High,
                Status = StatusEnum.InProgress,
                Deadline = new DateTime(2019, 2, 12, 5, 30, 0),
                Created = new DateTime(2018, 12, 31, 23, 59, 30),
                Creator = "Teszt Elek",
                LastModified = new DateTime(2018, 12, 31, 23, 59, 30),
                Modifier = "Teszt Elek"
            };

            mockTodoService.Setup(x => x.Read(It.IsAny<Guid>())).Returns(Task.FromResult(todo));

            var readActionResult = todoController.Read(Guid.NewGuid()).Result as OkObjectResult;
            var todoResult = readActionResult.Value as Todo;

            Assert.IsInstanceOfType(readActionResult, typeof(OkObjectResult));
            Assert.IsNotNull(todoResult);
        }

        [TestMethod]
        public void Read_InvalidId()
        {
            var mockTodoService = new Mock<ICrudService<Todo>>();
            var mockCategoryService = new Mock<ICrudService<Category>>();
            var mockTodoReadService = new Mock<ITodoReadService>();
            var mockTodoStoredProcedureService = new Mock<ITodoStoredProcedureService>();
            TodoController todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockTodoStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);

            mockTodoService.Setup(x => x.Read(It.IsAny<Guid>())).Returns(Task.FromResult<Todo>(null));

            var readActionResult = todoController.Read(Guid.NewGuid()).Result as NotFoundResult;

            Assert.IsInstanceOfType(readActionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void ReadReadByCategory_ValidCategoryId()
        {
            var mockTodoService = new Mock<ICrudService<Todo>>();
            var mockCategoryService = new Mock<ICrudService<Category>>();
            var mockTodoReadService = new Mock<ITodoReadService>();
            var mockTodoStoredProcedureService = new Mock<ITodoStoredProcedureService>();
            TodoController todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockTodoStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);

            var category = new Category()
            {
                CategoryId = Guid.Parse("36d32656-8df1-437d-a323-28d9e099a82c"),
                Bug = "Big Bug 12", Task = "Need to exterminate it!",
                Epic = true
            };

            mockCategoryService.Setup(x => x.Read(It.IsAny<Guid>())).Returns(Task.FromResult(category));
            mockTodoReadService.Setup(x => x.ReadByCategory(It.IsAny<Guid>())).Returns(Task.FromResult(TestData.Todos.AsQueryable().Where(x => x.CategoryId == category.CategoryId)));

            var readActionResult = todoController.ReadByCategory(Guid.NewGuid()).Result as OkObjectResult;
            var todoResult = readActionResult.Value as IEnumerable<Todo>;

            Assert.IsInstanceOfType(readActionResult, typeof(OkObjectResult));
            Assert.IsTrue(todoResult.Count() != 0);
        }

        [TestMethod]
        public void ReadReadByCategory_InvalidCategoryId()
        {
            var mockTodoService = new Mock<ICrudService<Todo>>();
            var mockCategoryService = new Mock<ICrudService<Category>>();
            var mockTodoReadService = new Mock<ITodoReadService>();
            var mockTodoStoredProcedureService = new Mock<ITodoStoredProcedureService>();
            TodoController todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockTodoStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);

            mockCategoryService.Setup(x => x.Read(It.IsAny<Guid>())).Returns(Task.FromResult<Category>(null));

            var readActionResult = todoController.ReadByCategory(Guid.NewGuid()).Result as NotFoundResult;

            Assert.IsInstanceOfType(readActionResult, typeof(NotFoundResult));
        }
        
        [TestMethod]
        public void ReadOpenTodosAndLatests_ReturnValueIsValid()
        {
            var mockTodoService = new Mock<ICrudService<Todo>>();
            var mockCategoryService = new Mock<ICrudService<Category>>();
            var mockTodoReadService = new Mock<ITodoReadService>();
            var mockTodoStoredProcedureService = new Mock<ITodoStoredProcedureService>();
            TodoController todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockTodoStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);

            mockTodoReadService.Setup(x => x.ReadOpenTodosAndLatests(It.IsAny<int>())).Returns(Task.FromResult(TestData.Todos.AsQueryable().Where(x => x.Status != StatusEnum.Completed && x.LastModified >= DateTime.Now.AddHours(-5))));

            var readActionResult = todoController.ReadOpenTodosAndLatests().Result as OkObjectResult;
            var todoResult = readActionResult.Value as IEnumerable<Todo>;

            Assert.IsInstanceOfType(readActionResult, typeof(OkObjectResult));
            Assert.IsTrue(todoResult.Count() != 0);
        }

        [TestMethod]
        public void ReadGroupByCategory_ReturnValueIsValid()
        {
            var mockTodoService = new Mock<ICrudService<Todo>>();
            var mockCategoryService = new Mock<ICrudService<Category>>();
            var mockTodoReadService = new Mock<ITodoReadService>();
            var mockTodoStoredProcedureService = new Mock<ITodoStoredProcedureService>();
            TodoController todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockTodoStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);

            mockTodoReadService.Setup(x => x.ReadGroupByCategory()).Returns(Task.FromResult(
                TestData.Todos.GroupBy(x => x.Category.Bug).OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.ToList())));

            var readActionResult = todoController.ReadGroupByCategory().Result as OkObjectResult;
            var todoResult = readActionResult.Value as Dictionary<string, List<Todo>>;

            Assert.IsInstanceOfType(readActionResult, typeof(OkObjectResult));
            Assert.IsTrue(todoResult.Count() != 0);
        }

        [TestMethod]
        public void ReadTree_ReturnValueIsValid()
        {
            var mockTodoService = new Mock<ICrudService<Todo>>();
            var mockCategoryService = new Mock<ICrudService<Category>>();
            var mockTodoReadService = new Mock<ITodoReadService>();
            var mockTodoStoredProcedureService = new Mock<ITodoStoredProcedureService>();
            TodoController todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockTodoStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);

            Dictionary<Guid, TreeModel> treeDataStructure = new Dictionary<Guid, TreeModel>();
            TestData.Todos.ToList().ForEach(x => treeDataStructure.Add((Guid)x.TodoId, new TreeModel { NodeTodo = x }));

            foreach (var item in treeDataStructure.Values)
            {
                if (item.NodeTodo.ParentId != null)
                {
                    treeDataStructure[(Guid)item.NodeTodo.ParentId.Value].ChildItems.Add(item);
                }
            }

            var returnList = treeDataStructure.Values.Where(x => x.NodeTodo.ParentId == null);

            mockTodoReadService.Setup(x => x.ReadTree()).Returns(Task.FromResult(returnList));

            var readActionResult = todoController.ReadTree().Result as OkObjectResult;
            var todoResult = readActionResult.Value as IEnumerable<TreeModel>;

            Assert.IsInstanceOfType(readActionResult, typeof(OkObjectResult));
            Assert.IsTrue(todoResult.Count() != 0);
        }

        [TestMethod]
        public void ReadOnlyIdAndName_ReturnValueIsValid()
        {
            var mockTodoService = new Mock<ICrudService<Todo>>();
            var mockCategoryService = new Mock<ICrudService<Category>>();
            var mockTodoReadService = new Mock<ITodoReadService>();
            var mockTodoStoredProcedureService = new Mock<ITodoStoredProcedureService>();
            TodoController todoController = new TodoController(mockTodoService.Object, mockTodoReadService.Object, mockTodoStoredProcedureService.Object, mockCategoryService.Object, mockTodoControllerSettings.Object);

            var returnList = TestData.Todos.Select(x => new TodoHeader() { Name = x.Name, TodoId = x.TodoId });

            mockTodoStoredProcedureService.Setup(x => x.ReadOnlyIdAndName()).Returns(Task.FromResult(returnList.AsQueryable()));

            var readActionResult = todoController.ReadOnlyIdAndName().Result as OkObjectResult;
            var todoResult = readActionResult.Value as IQueryable<TodoHeader>;

            Assert.IsInstanceOfType(readActionResult, typeof(OkObjectResult));
            Assert.IsTrue(todoResult.Count() != 0); 
        }
    }
}
