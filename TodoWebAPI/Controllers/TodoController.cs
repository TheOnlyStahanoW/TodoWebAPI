using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoModels.Core.DataModels;
using TodoModels.Core.Enums;

namespace TodoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        //DevBranch?
        private readonly ApiDbContext _context;

        public TodoController(ApiDbContext context)
        {
            _context = context;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<Todo>> Create(Todo todo)
        {
            todo.Created = DateTime.Now;
            todo.Creator = "Teszt Pista"; //HttpContext.User?.Identity?.Name; -- majd a későbbi autentikált user neve kell ide
            todo.LastModified = todo.Created;
            todo.Modifier = "Teszt Pista"; //HttpContext.User?.Identity?.Name; -- majd a későbbi autentikált user neve kell ide

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return Ok(todo);
        }

        // PUT: api/Todo/5
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

            var originalTodo = await _context.Todos.AsNoTracking().FirstOrDefaultAsync(x => x.TodoId == id);

            if (originalTodo == null)
            {
                return NotFound();
            }

            if (originalTodo.Status == StatusEnum.Completed)
            {
                if (originalTodo.Priority != todo.Priority)
                {
                    ModelState.AddModelError("Priority", "You can't change the Priority after the todo has been completed!");
                    return BadRequest(ModelState);
                }
                if (todo.Status != originalTodo.Status)
                {
                    ModelState.AddModelError("Status", "This todo is already completed, you can't change the status of it!");
                    return BadRequest(ModelState);
                }
            }

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(todo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Todo>> DeleteTodo(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Deleted = true;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> Read()
        {
            return await _context.Todos.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Todo>> Read(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        [HttpGet("/categoryId={categoryId:guid}")]
        public async Task<ActionResult<IEnumerable<Todo>>> ReadByCategory(Guid categoryId)
        {
            var todo = await _context.Todos.Where((x => x.CategoryId == categoryId)).ToListAsync();

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        private bool TodoExists(Guid id)
        {
            return _context.Todos.Any(e => e.TodoId == id);
        }
    }
}
