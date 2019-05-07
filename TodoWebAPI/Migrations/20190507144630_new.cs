using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoWebAPI.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "TodoId", "CategoryId", "Created", "Creator", "Deadline", "Deleted", "Description", "LastModified", "Modifier", "Name", "ParentId", "Priority", "Responsible", "Status" },
                values: new object[,]
                {
                    { new Guid("e78d64e5-390e-4076-b7b7-4618c42a6c92"), new Guid("36d32656-8df1-437d-a323-28d9e099a82c"), new DateTime(2019, 5, 7, 11, 20, 0, 0, DateTimeKind.Unspecified), "Teszt Józska", new DateTime(2019, 5, 10, 15, 41, 0, 0, DateTimeKind.Unspecified), false, "Jó kis leírással", new DateTime(2019, 5, 10, 15, 41, 0, 0, DateTimeKind.Unspecified), "Teszt Józska", "Gyerektodo - 1 gyökérelem 1. gyereke", new Guid("992a47fb-4512-4e39-8670-7d9c18f59c8b"), "Normal", null, "Started" },
                    { new Guid("2d60d13f-84ef-4158-a485-0b141a1fac7b"), new Guid("3b916b1d-a7db-4d8b-be3a-b8df53e9e90a"), new DateTime(2019, 4, 12, 9, 14, 0, 0, DateTimeKind.Unspecified), "Trab Antal", new DateTime(2019, 8, 21, 19, 25, 0, 0, DateTimeKind.Unspecified), false, "Jó kis leírással - még mindig.", new DateTime(2019, 4, 12, 9, 14, 0, 0, DateTimeKind.Unspecified), "Trab Antal", "Gyerektodo - 1 gyökérelem 2. gyereke", new Guid("992a47fb-4512-4e39-8670-7d9c18f59c8b"), "Normal", null, "Started" },
                    { new Guid("05c8e0a6-fb14-41a2-87d2-e0b3a41dae04"), new Guid("e30d079b-30eb-41a5-b4ab-57628925554a"), new DateTime(2019, 1, 2, 11, 30, 12, 0, DateTimeKind.Unspecified), "Havasi Gyopál", new DateTime(2019, 6, 19, 16, 30, 0, 0, DateTimeKind.Unspecified), false, "Tegnapelőttre kéne köszi.", new DateTime(2019, 4, 23, 22, 31, 31, 0, DateTimeKind.Unspecified), "Teszt Józska", "Gyerektodo - 2 gyökérelem 1. gyereke", new Guid("35aefa2b-d5d7-4d05-b53e-c51847e9e6ac"), "ASAP", null, "InProgress" },
                    { new Guid("25bef90e-4624-4c31-a94f-8ee6b3ab9615"), new Guid("e30d079b-30eb-41a5-b4ab-57628925554a"), new DateTime(2018, 12, 31, 23, 59, 30, 0, DateTimeKind.Unspecified), "Teszt Elek", new DateTime(2019, 2, 12, 5, 30, 0, 0, DateTimeKind.Unspecified), false, "Mélyvízben.", new DateTime(2019, 2, 12, 5, 0, 0, 0, DateTimeKind.Unspecified), "Akárki", "Gyerektodo - 1 gyökérelem 3. gyereke", new Guid("992a47fb-4512-4e39-8670-7d9c18f59c8b"), "High", null, "Completed" },
                    { new Guid("e352fcb9-97f3-4eb4-b3a4-2f6436904996"), new Guid("36d32656-8df1-437d-a323-28d9e099a82c"), new DateTime(2019, 5, 7, 11, 20, 0, 0, DateTimeKind.Unspecified), "Teszt Józska", new DateTime(2019, 5, 10, 15, 41, 0, 0, DateTimeKind.Unspecified), false, "Jó kis leírással", new DateTime(2019, 5, 10, 15, 41, 0, 0, DateTimeKind.Unspecified), "Teszt Józska", "1. gyökérelem 1 gyerekének 1 algyereke", new Guid("e78d64e5-390e-4076-b7b7-4618c42a6c92"), "Normal", null, "Started" },
                    { new Guid("0e979331-df23-4867-96ee-507706ac4268"), new Guid("3b916b1d-a7db-4d8b-be3a-b8df53e9e90a"), new DateTime(2019, 4, 12, 9, 14, 0, 0, DateTimeKind.Unspecified), "Trab Antal", new DateTime(2019, 8, 21, 19, 25, 0, 0, DateTimeKind.Unspecified), false, "Jó kis leírással - még mindig.", new DateTime(2019, 4, 12, 9, 14, 0, 0, DateTimeKind.Unspecified), "Trab Antal", "1. gyökérelem 1 gyerekének 2 algyereke", new Guid("e78d64e5-390e-4076-b7b7-4618c42a6c92"), "Normal", null, "Started" },
                    { new Guid("212e0a73-e210-4d36-91b0-57b24138376c"), new Guid("e30d079b-30eb-41a5-b4ab-57628925554a"), new DateTime(2019, 1, 2, 11, 30, 12, 0, DateTimeKind.Unspecified), "Havasi Gyopál", new DateTime(2019, 6, 19, 16, 30, 0, 0, DateTimeKind.Unspecified), false, "Tegnapelőttre kéne köszi.", new DateTime(2019, 4, 23, 22, 31, 31, 0, DateTimeKind.Unspecified), "Teszt Józska", "1. gyökérelem 2 gyerekének 1 algyereke", new Guid("05c8e0a6-fb14-41a2-87d2-e0b3a41dae04"), "ASAP", null, "InProgress" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "TodoId",
                keyValue: new Guid("05c8e0a6-fb14-41a2-87d2-e0b3a41dae04"));

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "TodoId",
                keyValue: new Guid("0e979331-df23-4867-96ee-507706ac4268"));

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "TodoId",
                keyValue: new Guid("212e0a73-e210-4d36-91b0-57b24138376c"));

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "TodoId",
                keyValue: new Guid("25bef90e-4624-4c31-a94f-8ee6b3ab9615"));

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "TodoId",
                keyValue: new Guid("2d60d13f-84ef-4158-a485-0b141a1fac7b"));

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "TodoId",
                keyValue: new Guid("e352fcb9-97f3-4eb4-b3a4-2f6436904996"));

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "TodoId",
                keyValue: new Guid("e78d64e5-390e-4076-b7b7-4618c42a6c92"));
        }
    }
}
