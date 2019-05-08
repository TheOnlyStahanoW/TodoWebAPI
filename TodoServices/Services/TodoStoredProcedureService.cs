using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoModels.Core.DataModels;
using TodoServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TodoServices.Services
{
    public class TodoStoredProcedureService : ITodoStoredProcedureService
    {
        protected readonly ApiDbContext _apiDbContext;

        public TodoStoredProcedureService(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public Task<IQueryable<TodoHeader>> ReadOnlyIdAndName()
        {
            var todoList = _apiDbContext.Query<TodoHeader>().FromSql("GetTodosIdAndName");

            return Task.FromResult(todoList);
        }
    }
}
