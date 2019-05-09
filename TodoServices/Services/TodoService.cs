using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using TodoServices.Interfaces;
using TodoModels.Core.DataModels;
using TodoModels.Core.Enums;

namespace TodoServices.Services
{
    public class TodoService : ICrudService<Todo>
    {
        protected readonly ApiDbContext _apiDbContext;

        public TodoService(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public async Task<Todo> Create(Todo todo)
        {
            todo.Created = DateTime.Now;
            todo.Creator = "Teszt Pista"; //HttpContext.User?.Identity?.Name; -- majd a későbbi autentikált user neve kell ide
            todo.LastModified = todo.Created;
            todo.Modifier = "Teszt Pista"; //HttpContext.User?.Identity?.Name; -- majd a későbbi autentikált user neve kell ide

            _apiDbContext.Todos.Add(todo);
            await _apiDbContext.SaveChangesAsync();

            return todo;
        }

        public async Task<Todo> Update(Guid id, Todo todo)
        {
            var originalTodo = await this.ReadNotTracked(id);

            if (originalTodo == null)
            {
                return null;
            }

            _apiDbContext.Entry(todo).State = EntityState.Modified;

            await _apiDbContext.SaveChangesAsync();
            return todo;
         }

        public async Task<Todo> Delete(Guid id)
        {
            var todo = await _apiDbContext.Todos.FindAsync(id);

            if (todo == null)
            {
                return null;
            }

            todo.Deleted = true;
            await _apiDbContext.SaveChangesAsync();

            return todo;
        }

        public Task<IQueryable<Todo>> Read()
        {
            return Task.FromResult(_apiDbContext.Todos.AsQueryable());
        }

        public async Task<Todo> Read(Guid id)
        {
            return await _apiDbContext.Todos.FindAsync(id);
        }

        public async Task<Todo> ReadNotTracked(Guid id)
        {
            return await _apiDbContext.Todos.AsNoTracking().FirstOrDefaultAsync(x => x.TodoId == id);
        }

        private bool TodoExists(Guid id)
        {
            return _apiDbContext.Todos.Any(e => e.TodoId == id);
        }
    }
}
