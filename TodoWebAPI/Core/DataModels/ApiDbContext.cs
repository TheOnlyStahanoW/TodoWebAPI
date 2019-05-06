using Microsoft.EntityFrameworkCore;
using System;

namespace TodoWebAPI.Core.DataModels
{
    public class ApiDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public Microsoft.EntityFrameworkCore.DbSet<Todo> Todos { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Todo>()
                .Property(e => e.Priority)
                .HasConversion(
                    v => v.ToString(),
                    v => (PriorityEnum)Enum.Parse(typeof(PriorityEnum), v));

            modelBuilder
                .Entity<Todo>()
                .Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusEnum)Enum.Parse(typeof(StatusEnum), v));
        }
    }

}
