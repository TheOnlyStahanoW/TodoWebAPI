using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoWebAPI.Migrations
{
    public partial class StoredProcedureUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetTodosIdAndName]
                AS
                BEGIN
                    SET NOCOUNT ON;
                    select [TodoId], [Name] from Todos order by [Created]
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
