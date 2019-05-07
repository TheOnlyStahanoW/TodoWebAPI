using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoModels.Core.DataModels;
using TodoModels.Core.Enums;
using TodoServices.Interfaces;
using TodoWebAPI.Extensions;

namespace TodoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        protected readonly ITodoService<Todo> _todoService;

        public TodoController(ITodoService<Todo> todoService)
        {
            _todoService = todoService;
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

        // DELETE: api/Todo/5
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

        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> Read()
        {
            var list = await _todoService.Read();
            return Ok(list);
        }

        // GET: api/Todo/5
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
    }
}
