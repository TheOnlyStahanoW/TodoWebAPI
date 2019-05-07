using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TodoModels.Core.DataModels;
using TodoModels.Core.Enums;
using TodoModels.Core.Settings;
using TodoServices.Interfaces;
using TodoWebAPI.Extensions;

namespace TodoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        protected readonly ICrudService<Todo> _todoService;
        protected readonly ICrudService<Category> _categoryService;
        protected readonly IOptions<TodoControllerSettings> _todoControllerSettings;

        public TodoController(ICrudService<Todo> todoService, ICrudService<Category> categoryService, IOptions<TodoControllerSettings> todoControllerSettings)
        {
            _todoService = todoService;
            _categoryService = categoryService;
            _todoControllerSettings = todoControllerSettings;
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> Create(Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrors());
            }

            todo = await _todoService.Create(todo);

            if (todo == null)
            {
                ModelState.AddModelError("todoItem", "Item is not created!");
                return BadRequest(ModelState.GetErrors());
            }

            return Ok(todo);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(Guid id, Todo todo)
        {
            if (id != todo.TodoId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var originalTodo = await _todoService.ReadNotTracked(id);

            if (originalTodo == null)
            {
                return NotFound();
            }

            if (originalTodo.Status == StatusEnum.Completed)
            {
                if (originalTodo.Priority != todo.Priority)
                {
                    ModelState.AddModelError("Priority", "You can't change the Priority after the todo has been completed!");
                    return BadRequest(ModelState.GetErrors());
                }
                if (todo.Status != originalTodo.Status)
                {
                    ModelState.AddModelError("Status", "This todo is already completed, you can't change the status of it!");
                    return BadRequest(ModelState.GetErrors());
                }
            }

            todo = await _todoService.Update(id, todo);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Todo>> DeleteTodo(Guid id)
        {
            var todo = await _todoService.Delete(id);

            if (todo == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> Read()
        {
            var list = await _todoService.Read();
            return Ok(list);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Todo>> Read(Guid id)
        {
            var todo = await _todoService.Read(id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        [HttpGet("readbycategory/{categoryId:guid}")]
        public async Task<ActionResult<IEnumerable<Todo>>> ReadByCategory(Guid categoryId)
        {
            var category = await _categoryService.Read(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            var todoList = await _todoService.Read();

            if (todoList == null)
            {
                return NotFound();
            }

            return todoList.Where(x => x.CategoryId == categoryId).ToList();
        }

        [HttpGet("readopentodosandlatests")]
        public async Task<ActionResult<IEnumerable<Todo>>> ReadOpenTodosAndLatests()
        {
            var todoList = await _todoService.Read();

            return todoList
                .Where(x => x.Status != StatusEnum.Completed || x.LastModified >= DateTime.Now.AddHours(-1 * _todoControllerSettings.Value.ReadLatestsIntervalInHours))
                .OrderByDescending(x => x.LastModified)
                .ToList(); 
        }

        //Original solution: https://docs.microsoft.com/en-us/dotnet/csharp/linq/group-query-results
        [HttpGet("readgroupbycategory")]
        public async Task<ActionResult> ReadGroupByCategory()
        {
            var todoList = await _todoService.Read();
            var categoryList = await _categoryService.Read();
            List<CategoryGroupModel> categoryGroupModelList = new List<CategoryGroupModel>();

            var queryGroupByCategory =
                    from todoItem in todoList
                    where todoItem.CategoryId != null
                    group todoItem by todoItem.CategoryId into newGroup
                    orderby newGroup.Key
                    select newGroup;

            foreach (var categoryGroup in queryGroupByCategory)
            {
                CategoryGroupModel newCategoryGroupItem = new CategoryGroupModel() { CategoryId = (Guid)categoryGroup.Key};

                foreach (var cGroupItem in categoryGroup)
                {
                    newCategoryGroupItem.TodoItems.Add(cGroupItem);
                }

                categoryGroupModelList.Add(newCategoryGroupItem);
            }

            return Ok(categoryGroupModelList);
        }

        //Original solution link: https://stackoverflow.com/questions/444296/how-to-efficiently-build-a-tree-from-a-flat-structure
        [HttpGet("readtree")]
        public async Task<ActionResult<IEnumerable<TreeModel>>> ReadTree()
        {
            var todoList = await _todoService.Read();

            if (todoList == null)
            {
                return NotFound();
            }

            Dictionary<Guid, TreeModel> treeDataStructure = new Dictionary<Guid, TreeModel>();
            todoList.ToList().ForEach(x => treeDataStructure.Add((Guid)x.TodoId, new TreeModel { NodeTodo = x }));

            foreach (var item in treeDataStructure.Values)
            {
                if (item.NodeTodo.ParentId != null)
                {
                    treeDataStructure[(Guid)item.NodeTodo.ParentId.Value].ChildItems.Add(item);
                }
            }

            return Ok(treeDataStructure.Values.Where(x => x.NodeTodo.ParentId == null));
        }
    }
}
