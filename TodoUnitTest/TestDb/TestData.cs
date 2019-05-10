using System;
using System.Collections.Generic;
using System.Text;
using TodoModels.Core.DataModels;
using TodoModels.Core.Enums;

namespace TodoUnitTest.TestDb
{
    public class TestData
    {
        public static List<Category> Categories = new List<Category>()
        {
            new Category() { CategoryId = Guid.Parse("36d32656-8df1-437d-a323-28d9e099a82c"), Bug = "Big Bug 12", Task = "Need to exterminate it!", Epic = true },
            new Category() { CategoryId = Guid.Parse("3b916b1d-a7db-4d8b-be3a-b8df53e9e90a"), Bug = "Little Bug 23232", Task = "aaaaaaaaaaaaaaaaaaaaaaaaaa!", Epic = false },
            new Category() { CategoryId = Guid.Parse("82001101-248a-4500-a624-476329de3fc0"), Bug = "Nem tudom mi ez 2342", Task = "Need to exterminate it!", Epic = true },
            new Category() { CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"), Bug = "Test Category 232", Task = "Need to exterminate it!", Epic = false }
        };
        public static List<Todo> Todos = new List<Todo>()
        {
            new Todo()
            {
                TodoId = Guid.Parse("d59512b5-9d09-4877-ad69-936187def439"),
                CategoryId = Guid.Parse("36d32656-8df1-437d-a323-28d9e099a82c"),
                Category = new Category() { CategoryId = Guid.Parse("36d32656-8df1-437d-a323-28d9e099a82c"), Bug = "Big Bug 12", Task = "Need to exterminate it!", Epic = true },
                Name = "Teszt 122 jó kis Data Seed teszt",
                Description = "Jó kis leírással",
                Deadline = new DateTime(2019, 5, 10, 15, 41, 0),
                Created = new DateTime(2019, 5, 7, 11, 20, 0),
                Creator = "Teszt Józska",
                LastModified = new DateTime(2019, 5, 10, 15, 41, 0),
                Modifier = "Teszt Józska"
            },
            new Todo()
            {
                TodoId = Guid.Parse("35aefa2b-d5d7-4d05-b53e-c51847e9e6ac"),
                CategoryId = Guid.Parse("3b916b1d-a7db-4d8b-be3a-b8df53e9e90a"),
                Category = new Category() { CategoryId = Guid.Parse("3b916b1d-a7db-4d8b-be3a-b8df53e9e90a"), Bug = "Little Bug 23232", Task = "aaaaaaaaaaaaaaaaaaaaaaaaaa!", Epic = false },
                Name = "Gyomlálás a betonházban",
                Description = "Jó kis leírással - még mindig.",
                Deadline = new DateTime(2019, 8, 21, 19, 25, 0),
                Created = new DateTime(2019, 4, 12, 09, 14, 0),
                Creator = "Trab Antal",
                LastModified = new DateTime(2019, 4, 12, 09, 14, 0),
                Modifier = "Trab Antal"
            },
            new Todo()
            {
                TodoId = Guid.Parse("d5e88836-addb-4d3b-9114-a91d009abd09"),
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Category = new Category() { CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"), Bug = "Test Category 232", Task = "Need to exterminate it!", Epic = false },
                Name = "Vonatkerék pumpálása régi Ikarussal",
                Description = "Tegnapelőttre kéne köszi.",
                Deadline = new DateTime(2019, 6, 19, 16, 30, 0),
                Priority = PriorityEnum.ASAP,
                Status = StatusEnum.InProgress,
                Created = new DateTime(2019, 1, 2, 11, 30, 12),
                Creator = "Havasi Gyopál",
                LastModified = new DateTime(2019, 4, 23, 22, 31, 31),
                Modifier = "Teszt Józska"
            },
            new Todo()
            {
                TodoId = Guid.Parse("992a47fb-4512-4e39-8670-7d9c18f59c8b"),
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Category = new Category() { CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"), Bug = "Test Category 232", Task = "Need to exterminate it!", Epic = false },
                Name = "Fedettpályás távolbalátás gyakorlása két vak egérrel.",
                Description = "Mélyvízben.",
                Priority = PriorityEnum.High,
                Status = StatusEnum.Completed,
                Deadline = new DateTime(2019, 2, 12, 5, 30, 0),
                Created = new DateTime(2018, 12, 31, 23, 59, 30),
                Creator = "Teszt Elek",
                LastModified = new DateTime(2019, 2, 12, 05, 0, 0),
                Modifier = "Akárki"
            },
            //childitems
            new Todo()
            {
                TodoId = Guid.Parse("e78d64e5-390e-4076-b7b7-4618c42a6c92"),
                CategoryId = Guid.Parse("36d32656-8df1-437d-a323-28d9e099a82c"),
                Category = new Category() { CategoryId = Guid.Parse("36d32656-8df1-437d-a323-28d9e099a82c"), Bug = "Big Bug 12", Task = "Need to exterminate it!", Epic = true },
                Name = "Gyerektodo - 1 gyökérelem 1. gyereke",
                Description = "Jó kis leírással",
                Deadline = new DateTime(2019, 5, 10, 15, 41, 0),
                Created = new DateTime(2019, 5, 7, 11, 20, 0),
                Creator = "Teszt Józska",
                LastModified = new DateTime(2019, 5, 10, 15, 41, 0),
                Modifier = "Teszt Józska",
                ParentId = Guid.Parse("992a47fb-4512-4e39-8670-7d9c18f59c8b")
            },
            new Todo()
            {
                TodoId = Guid.Parse("2d60d13f-84ef-4158-a485-0b141a1fac7b"),
                CategoryId = Guid.Parse("3b916b1d-a7db-4d8b-be3a-b8df53e9e90a"),
                Category = new Category() { CategoryId = Guid.Parse("3b916b1d-a7db-4d8b-be3a-b8df53e9e90a"), Bug = "Little Bug 23232", Task = "aaaaaaaaaaaaaaaaaaaaaaaaaa!", Epic = false },
                Name = "Gyerektodo - 1 gyökérelem 2. gyereke",
                Description = "Jó kis leírással - még mindig.",
                Deadline = new DateTime(2019, 8, 21, 19, 25, 0),
                Created = new DateTime(2019, 4, 12, 09, 14, 0),
                Creator = "Trab Antal",
                LastModified = new DateTime(2019, 4, 12, 09, 14, 0),
                Modifier = "Trab Antal",
                ParentId = Guid.Parse("992a47fb-4512-4e39-8670-7d9c18f59c8b")
            },
            new Todo()
            {
                TodoId = Guid.Parse("05c8e0a6-fb14-41a2-87d2-e0b3a41dae04"),
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Category = new Category() { CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"), Bug = "Test Category 232", Task = "Need to exterminate it!", Epic = false },
                Name = "Gyerektodo - 2 gyökérelem 1. gyereke",
                Description = "Tegnapelőttre kéne köszi.",
                Deadline = new DateTime(2019, 6, 19, 16, 30, 0),
                Priority = PriorityEnum.ASAP,
                Status = StatusEnum.InProgress,
                Created = new DateTime(2019, 1, 2, 11, 30, 12),
                Creator = "Havasi Gyopál",
                LastModified = new DateTime(2019, 4, 23, 22, 31, 31),
                Modifier = "Teszt Józska",
                ParentId = Guid.Parse("35aefa2b-d5d7-4d05-b53e-c51847e9e6ac")
            },
            new Todo()
            {
                TodoId = Guid.Parse("25bef90e-4624-4c31-a94f-8ee6b3ab9615"),
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Category = new Category() { CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"), Bug = "Test Category 232", Task = "Need to exterminate it!", Epic = false },
                Name = "Gyerektodo - 1 gyökérelem 3. gyereke",
                Description = "Mélyvízben.",
                Priority = PriorityEnum.High,
                Status = StatusEnum.Completed,
                Deadline = new DateTime(2019, 2, 12, 5, 30, 0),
                Created = new DateTime(2018, 12, 31, 23, 59, 30),
                Creator = "Teszt Elek",
                LastModified = new DateTime(2019, 5, 31, 05, 0, 0),
                Modifier = "Akárki",
                ParentId = Guid.Parse("992a47fb-4512-4e39-8670-7d9c18f59c8b")
            },
            //lower level childitems
            new Todo()
            {
                TodoId = Guid.Parse("e352fcb9-97f3-4eb4-b3a4-2f6436904996"),
                CategoryId = Guid.Parse("36d32656-8df1-437d-a323-28d9e099a82c"),
                Category = new Category() { CategoryId = Guid.Parse("36d32656-8df1-437d-a323-28d9e099a82c"), Bug = "Big Bug 12", Task = "Need to exterminate it!", Epic = true },
                Name = "1. gyökérelem 1 gyerekének 1 algyereke",
                Description = "Jó kis leírással",
                Deadline = new DateTime(2019, 5, 10, 15, 41, 0),
                Created = new DateTime(2019, 5, 7, 11, 20, 0),
                Creator = "Teszt Józska",
                LastModified = new DateTime(2019, 5, 31, 15, 41, 0),
                Modifier = "Teszt Józska",
                ParentId = Guid.Parse("e78d64e5-390e-4076-b7b7-4618c42a6c92")
            },
            new Todo()
            {
                TodoId = Guid.Parse("0e979331-df23-4867-96ee-507706ac4268"),
                CategoryId = Guid.Parse("3b916b1d-a7db-4d8b-be3a-b8df53e9e90a"),
                Category = new Category() { CategoryId = Guid.Parse("3b916b1d-a7db-4d8b-be3a-b8df53e9e90a"), Bug = "Little Bug 23232", Task = "aaaaaaaaaaaaaaaaaaaaaaaaaa!", Epic = false },
                Name = "1. gyökérelem 1 gyerekének 2 algyereke",
                Description = "Jó kis leírással - még mindig.",
                Deadline = new DateTime(2019, 8, 21, 19, 25, 0),
                Created = new DateTime(2019, 4, 12, 09, 14, 0),
                Creator = "Trab Antal",
                LastModified = new DateTime(2019, 4, 12, 09, 14, 0),
                Modifier = "Trab Antal",
                ParentId = Guid.Parse("e78d64e5-390e-4076-b7b7-4618c42a6c92")
            },
            new Todo()
            {
                TodoId = Guid.Parse("212e0a73-e210-4d36-91b0-57b24138376c"),
                CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"),
                Category = new Category() { CategoryId = Guid.Parse("e30d079b-30eb-41a5-b4ab-57628925554a"), Bug = "Test Category 232", Task = "Need to exterminate it!", Epic = false },
                Name = "1. gyökérelem 2 gyerekének 1 algyereke",
                Description = "Tegnapelőttre kéne köszi.",
                Deadline = new DateTime(2019, 6, 19, 16, 30, 0),
                Priority = PriorityEnum.ASAP,
                Status = StatusEnum.InProgress,
                Created = new DateTime(2019, 1, 2, 11, 30, 12),
                Creator = "Havasi Gyopál",
                LastModified = new DateTime(2019, 4, 23, 22, 31, 31),
                Modifier = "Teszt Józska",
                ParentId = Guid.Parse("05c8e0a6-fb14-41a2-87d2-e0b3a41dae04")
            }
        };
    }
}
