using System;
using Microsoft.EntityFrameworkCore;
using TodoModels.Core.DataModels;

namespace TodoUnitTest.TestDb
{
    public class TestDbContext
    {
        public void CreateTestDb(ApiDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            dbContext.Database.ExecuteSqlCommand(@"CREATE PROCEDURE [dbo].[GetTodosIdAndName]
                AS
                BEGIN
                    SET NOCOUNT ON;
            select[TodoId], [Name] from Todos order by[Created]
                END");

            //dbContext.Categories.AddRange(TestData.Categories);
            //dbContext.Todos.AddRange(TestData.Todos);

            dbContext.SaveChanges();
        }
    }
}
