using Microsoft.EntityFrameworkCore;
using System;

namespace TodoModels.Core.DataModels
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Category> Categories { get; set; }

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

            //for testing purposes
            modelBuilder
                .Entity<Category>()
                .HasData(
                    new Category() { CategoryId = Guid.Parse("36d32656-8df1-437d-a323-28d9e099a82c"), Bug = "Big Bug 12", Task = "Need to exterminate it!", Epic = true },
                    new Category() { CategoryId = Guid.Parse("3b916b1d-a7db-4d8b-be3a-b8df53e9e90a"), Bug = "Little Bug 23232", Task = "aaaaaaaaaaaaaaaaaaaaaaaaaa!", Epic = false },
                    new Category() { CategoryId = Guid.Parse("82001101-248a-4500-a624-476329de3fc0"), Bug = "Nem tudom mi ez 2342", Task = "Need to exterminate it!", Epic = true },
                    new Category() { CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"), Bug = "Test Category 232", Task = "Need to exterminate it!", Epic = false }
                );

            modelBuilder
                .Entity<Todo>()
                .HasData(
                    new Todo()
                    {
                        TodoId = Guid.Parse("d59512b5-9d09-4877-ad69-936187def439"),
                        CategoryId = Guid.Parse("36d32656-8df1-437d-a323-28d9e099a82c"),
                        Name = "Teszt 122 jó kis Data Seed teszt",
                        Description = "Jó kis leírással",
                        Deadline = new System.DateTime(2019, 5, 10, 15, 41, 0),
                        Created = new System.DateTime(2019, 5, 7, 11, 20, 0),
                        Creator = "Teszt Józska",
                        LastModified = new System.DateTime(2019, 5, 10, 15, 41, 0),
                        Modifier = "Teszt Józska"
                    },
                    new Todo()
                    {
                        TodoId = Guid.Parse("35aefa2b-d5d7-4d05-b53e-c51847e9e6ac"),
                        CategoryId = Guid.Parse("3b916b1d-a7db-4d8b-be3a-b8df53e9e90a"),
                        Name = "Gyomlálás a betonházban",
                        Description = "Jó kis leírással - még mindig.",
                        Deadline = new System.DateTime(2019, 8, 21, 19, 25, 0),
                        Created = new System.DateTime(2019, 4, 12, 09, 14, 0),
                        Creator = "Trab Antal",
                        LastModified = new System.DateTime(2019, 4, 12, 09, 14, 0),
                        Modifier = "Trab Antal"
                    },
                    new Todo()
                    {
                        TodoId = Guid.Parse("d5e88836-addb-4d3b-9114-a91d009abd09"),
                        CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                        Name = "Vonatkerék pumpálása régi Ikarussal",
                        Description = "Tegnapelőttre kéne köszi.",
                        Deadline = new System.DateTime(2019, 6, 19, 16, 30, 0),
                        Priority = Enums.PriorityEnum.ASAP,
                        Status = Enums.StatusEnum.InProgress,
                        Created = new System.DateTime(2019, 1, 2, 11, 30, 12),
                        Creator = "Havasi Gyopál",
                        LastModified = new System.DateTime(2019, 4, 23, 22, 31, 31),
                        Modifier = "Teszt Józska"
                    },
                    new Todo()
                    {
                        TodoId = Guid.Parse("992a47fb-4512-4e39-8670-7d9c18f59c8b"),
                        CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                        Name = "Fedettpályás távolbalátás gyakorlása két vak egérrel.",
                        Description = "Mélyvízben.",
                        Priority = Enums.PriorityEnum.High,
                        Status = Enums.StatusEnum.Completed,
                        Deadline = new System.DateTime(2019, 2, 12, 5, 30, 0),
                        Created = new System.DateTime(2018, 12, 31, 23, 59, 30),
                        Creator = "Teszt Elek",
                        LastModified = new System.DateTime(2019, 2, 12, 05, 0, 0),
                        Modifier = "Akárki"
                    }
                );

            base.OnModelCreating(modelBuilder);
        }
    }

}
