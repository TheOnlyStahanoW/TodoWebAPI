using Microsoft.EntityFrameworkCore;

namespace TodoWebAPI.Core.DataModels
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Todo>()
                .Property(e => e.Priority)
                .HasConversion<string>()
                .HasMaxLength(100);

            modelBuilder
                .Entity<Todo>()
                .Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(100);

            base.OnModelCreating(modelBuilder);
        }
    }

}
