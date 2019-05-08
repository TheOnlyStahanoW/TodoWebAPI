using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoWebAPI.Migrations
{
    public partial class _20190508_1239_Remigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(nullable: false),
                    Bug = table.Column<string>(maxLength: 250, nullable: true),
                    Task = table.Column<string>(nullable: true),
                    Epic = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    TodoId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 120, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Priority = table.Column<string>(maxLength: 100, nullable: false),
                    Responsible = table.Column<string>(maxLength: 120, nullable: true),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(maxLength: 100, nullable: false),
                    ParentId = table.Column<Guid>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(maxLength: 120, nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Modifier = table.Column<string>(maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.TodoId);
                    table.ForeignKey(
                        name: "FK_Todos_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Bug", "Epic", "Task" },
                values: new object[,]
                {
                    { new Guid("36d32656-8df1-437d-a323-28d9e099a82c"), "Big Bug 12", true, "Need to exterminate it!" },
                    { new Guid("3b916b1d-a7db-4d8b-be3a-b8df53e9e90a"), "Little Bug 23232", false, "aaaaaaaaaaaaaaaaaaaaaaaaaa!" },
                    { new Guid("82001101-248a-4500-a624-476329de3fc0"), "Nem tudom mi ez 2342", true, "Need to exterminate it!" },
                    { new Guid("e30d079b-30eb-41a5-b4ab-57628925554a"), "Test Category 232", false, "Need to exterminate it!" }
                });

            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "TodoId", "CategoryId", "Created", "Creator", "Deadline", "Deleted", "Description", "LastModified", "Modifier", "Name", "ParentId", "Priority", "Responsible", "Status" },
                values: new object[,]
                {
                    { new Guid("d59512b5-9d09-4877-ad69-936187def439"), new Guid("36d32656-8df1-437d-a323-28d9e099a82c"), new DateTime(2019, 5, 7, 11, 20, 0, 0, DateTimeKind.Unspecified), "Teszt Józska", new DateTime(2019, 5, 10, 15, 41, 0, 0, DateTimeKind.Unspecified), false, "Jó kis leírással", new DateTime(2019, 5, 10, 15, 41, 0, 0, DateTimeKind.Unspecified), "Teszt Józska", "Teszt 122 jó kis Data Seed teszt", null, "Normal", null, "Started" },
                    { new Guid("e78d64e5-390e-4076-b7b7-4618c42a6c92"), new Guid("36d32656-8df1-437d-a323-28d9e099a82c"), new DateTime(2019, 5, 7, 11, 20, 0, 0, DateTimeKind.Unspecified), "Teszt Józska", new DateTime(2019, 5, 10, 15, 41, 0, 0, DateTimeKind.Unspecified), false, "Jó kis leírással", new DateTime(2019, 5, 10, 15, 41, 0, 0, DateTimeKind.Unspecified), "Teszt Józska", "Gyerektodo - 1 gyökérelem 1. gyereke", new Guid("992a47fb-4512-4e39-8670-7d9c18f59c8b"), "Normal", null, "Started" },
                    { new Guid("e352fcb9-97f3-4eb4-b3a4-2f6436904996"), new Guid("36d32656-8df1-437d-a323-28d9e099a82c"), new DateTime(2019, 5, 7, 11, 20, 0, 0, DateTimeKind.Unspecified), "Teszt Józska", new DateTime(2019, 5, 10, 15, 41, 0, 0, DateTimeKind.Unspecified), false, "Jó kis leírással", new DateTime(2019, 5, 10, 15, 41, 0, 0, DateTimeKind.Unspecified), "Teszt Józska", "1. gyökérelem 1 gyerekének 1 algyereke", new Guid("e78d64e5-390e-4076-b7b7-4618c42a6c92"), "Normal", null, "Started" },
                    { new Guid("35aefa2b-d5d7-4d05-b53e-c51847e9e6ac"), new Guid("3b916b1d-a7db-4d8b-be3a-b8df53e9e90a"), new DateTime(2019, 4, 12, 9, 14, 0, 0, DateTimeKind.Unspecified), "Trab Antal", new DateTime(2019, 8, 21, 19, 25, 0, 0, DateTimeKind.Unspecified), false, "Jó kis leírással - még mindig.", new DateTime(2019, 4, 12, 9, 14, 0, 0, DateTimeKind.Unspecified), "Trab Antal", "Gyomlálás a betonházban", null, "Normal", null, "Started" },
                    { new Guid("2d60d13f-84ef-4158-a485-0b141a1fac7b"), new Guid("3b916b1d-a7db-4d8b-be3a-b8df53e9e90a"), new DateTime(2019, 4, 12, 9, 14, 0, 0, DateTimeKind.Unspecified), "Trab Antal", new DateTime(2019, 8, 21, 19, 25, 0, 0, DateTimeKind.Unspecified), false, "Jó kis leírással - még mindig.", new DateTime(2019, 4, 12, 9, 14, 0, 0, DateTimeKind.Unspecified), "Trab Antal", "Gyerektodo - 1 gyökérelem 2. gyereke", new Guid("992a47fb-4512-4e39-8670-7d9c18f59c8b"), "Normal", null, "Started" },
                    { new Guid("0e979331-df23-4867-96ee-507706ac4268"), new Guid("3b916b1d-a7db-4d8b-be3a-b8df53e9e90a"), new DateTime(2019, 4, 12, 9, 14, 0, 0, DateTimeKind.Unspecified), "Trab Antal", new DateTime(2019, 8, 21, 19, 25, 0, 0, DateTimeKind.Unspecified), false, "Jó kis leírással - még mindig.", new DateTime(2019, 4, 12, 9, 14, 0, 0, DateTimeKind.Unspecified), "Trab Antal", "1. gyökérelem 1 gyerekének 2 algyereke", new Guid("e78d64e5-390e-4076-b7b7-4618c42a6c92"), "Normal", null, "Started" },
                    { new Guid("d5e88836-addb-4d3b-9114-a91d009abd09"), new Guid("e30d079b-30eb-41a5-b4ab-57628925554a"), new DateTime(2019, 1, 2, 11, 30, 12, 0, DateTimeKind.Unspecified), "Havasi Gyopál", new DateTime(2019, 6, 19, 16, 30, 0, 0, DateTimeKind.Unspecified), false, "Tegnapelőttre kéne köszi.", new DateTime(2019, 4, 23, 22, 31, 31, 0, DateTimeKind.Unspecified), "Teszt Józska", "Vonatkerék pumpálása régi Ikarussal", null, "ASAP", null, "InProgress" },
                    { new Guid("992a47fb-4512-4e39-8670-7d9c18f59c8b"), new Guid("e30d079b-30eb-41a5-b4ab-57628925554a"), new DateTime(2018, 12, 31, 23, 59, 30, 0, DateTimeKind.Unspecified), "Teszt Elek", new DateTime(2019, 2, 12, 5, 30, 0, 0, DateTimeKind.Unspecified), false, "Mélyvízben.", new DateTime(2019, 2, 12, 5, 0, 0, 0, DateTimeKind.Unspecified), "Akárki", "Fedettpályás távolbalátás gyakorlása két vak egérrel.", null, "High", null, "Completed" },
                    { new Guid("05c8e0a6-fb14-41a2-87d2-e0b3a41dae04"), new Guid("e30d079b-30eb-41a5-b4ab-57628925554a"), new DateTime(2019, 1, 2, 11, 30, 12, 0, DateTimeKind.Unspecified), "Havasi Gyopál", new DateTime(2019, 6, 19, 16, 30, 0, 0, DateTimeKind.Unspecified), false, "Tegnapelőttre kéne köszi.", new DateTime(2019, 4, 23, 22, 31, 31, 0, DateTimeKind.Unspecified), "Teszt Józska", "Gyerektodo - 2 gyökérelem 1. gyereke", new Guid("35aefa2b-d5d7-4d05-b53e-c51847e9e6ac"), "ASAP", null, "InProgress" },
                    { new Guid("25bef90e-4624-4c31-a94f-8ee6b3ab9615"), new Guid("e30d079b-30eb-41a5-b4ab-57628925554a"), new DateTime(2018, 12, 31, 23, 59, 30, 0, DateTimeKind.Unspecified), "Teszt Elek", new DateTime(2019, 2, 12, 5, 30, 0, 0, DateTimeKind.Unspecified), false, "Mélyvízben.", new DateTime(2019, 2, 12, 5, 0, 0, 0, DateTimeKind.Unspecified), "Akárki", "Gyerektodo - 1 gyökérelem 3. gyereke", new Guid("992a47fb-4512-4e39-8670-7d9c18f59c8b"), "High", null, "Completed" },
                    { new Guid("212e0a73-e210-4d36-91b0-57b24138376c"), new Guid("e30d079b-30eb-41a5-b4ab-57628925554a"), new DateTime(2019, 1, 2, 11, 30, 12, 0, DateTimeKind.Unspecified), "Havasi Gyopál", new DateTime(2019, 6, 19, 16, 30, 0, 0, DateTimeKind.Unspecified), false, "Tegnapelőttre kéne köszi.", new DateTime(2019, 4, 23, 22, 31, 31, 0, DateTimeKind.Unspecified), "Teszt Józska", "1. gyökérelem 2 gyerekének 1 algyereke", new Guid("05c8e0a6-fb14-41a2-87d2-e0b3a41dae04"), "ASAP", null, "InProgress" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todos_CategoryId",
                table: "Todos",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
