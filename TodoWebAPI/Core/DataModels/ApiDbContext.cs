using Microsoft.EntityFrameworkCore;

namespace TodoWebAPI.Core.DataModels
{
    public class ApiDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public Microsoft.EntityFrameworkCore.DbSet<Todo> Todos { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
    }

}
