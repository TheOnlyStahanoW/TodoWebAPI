﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoModels.Core.DataModels;

namespace TodoWebAPI.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    partial class ApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TodoModels.Core.DataModels.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bug")
                        .HasMaxLength(250);

                    b.Property<bool>("Epic");

                    b.Property<string>("Task");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = new Guid("36d32656-8df1-437d-a323-28d9e099a82c"),
                            Bug = "Big Bug 12",
                            Epic = true,
                            Task = "Need to exterminate it!"
                        },
                        new
                        {
                            CategoryId = new Guid("3b916b1d-a7db-4d8b-be3a-b8df53e9e90a"),
                            Bug = "Little Bug 23232",
                            Epic = false,
                            Task = "aaaaaaaaaaaaaaaaaaaaaaaaaa!"
                        },
                        new
                        {
                            CategoryId = new Guid("82001101-248a-4500-a624-476329de3fc0"),
                            Bug = "Nem tudom mi ez 2342",
                            Epic = true,
                            Task = "Need to exterminate it!"
                        },
                        new
                        {
                            CategoryId = new Guid("e30d079b-30eb-41a5-b4ab-57628925554a"),
                            Bug = "Test Category 232",
                            Epic = false,
                            Task = "Need to exterminate it!"
                        });
                });

            modelBuilder.Entity("TodoModels.Core.DataModels.Todo", b =>
                {
                    b.Property<Guid>("TodoId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CategoryId");

                    b.Property<DateTime?>("Created");

                    b.Property<string>("Creator")
                        .HasMaxLength(120);

                    b.Property<DateTime>("Deadline");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("LastModified");

                    b.Property<string>("Modifier")
                        .HasMaxLength(120);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120);

                    b.Property<Guid?>("ParentId");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Responsible")
                        .HasMaxLength(120);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("TodoId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Todos");

                    b.HasData(
                        new
                        {
                            TodoId = new Guid("d59512b5-9d09-4877-ad69-936187def439"),
                            CategoryId = new Guid("36d32656-8df1-437d-a323-28d9e099a82c"),
                            Created = new DateTime(2019, 5, 7, 11, 20, 0, 0, DateTimeKind.Unspecified),
                            Creator = "Teszt Józska",
                            Deadline = new DateTime(2019, 5, 10, 15, 41, 0, 0, DateTimeKind.Unspecified),
                            Deleted = false,
                            Description = "Jó kis leírással",
                            LastModified = new DateTime(2019, 5, 10, 15, 41, 0, 0, DateTimeKind.Unspecified),
                            Modifier = "Teszt Józska",
                            Name = "Teszt 122 jó kis Data Seed teszt",
                            Priority = "Normal",
                            Status = "Started"
                        },
                        new
                        {
                            TodoId = new Guid("35aefa2b-d5d7-4d05-b53e-c51847e9e6ac"),
                            CategoryId = new Guid("3b916b1d-a7db-4d8b-be3a-b8df53e9e90a"),
                            Created = new DateTime(2019, 4, 12, 9, 14, 0, 0, DateTimeKind.Unspecified),
                            Creator = "Trab Antal",
                            Deadline = new DateTime(2019, 8, 21, 19, 25, 0, 0, DateTimeKind.Unspecified),
                            Deleted = false,
                            Description = "Jó kis leírással - még mindig.",
                            LastModified = new DateTime(2019, 4, 12, 9, 14, 0, 0, DateTimeKind.Unspecified),
                            Modifier = "Trab Antal",
                            Name = "Gyomlálás a betonházban",
                            Priority = "Normal",
                            Status = "Started"
                        },
                        new
                        {
                            TodoId = new Guid("d5e88836-addb-4d3b-9114-a91d009abd09"),
                            CategoryId = new Guid("e30d079b-30eb-41a5-b4ab-57628925554a"),
                            Created = new DateTime(2019, 1, 2, 11, 30, 12, 0, DateTimeKind.Unspecified),
                            Creator = "Havasi Gyopál",
                            Deadline = new DateTime(2019, 6, 19, 16, 30, 0, 0, DateTimeKind.Unspecified),
                            Deleted = false,
                            Description = "Tegnapelőttre kéne köszi.",
                            LastModified = new DateTime(2019, 4, 23, 22, 31, 31, 0, DateTimeKind.Unspecified),
                            Modifier = "Teszt Józska",
                            Name = "Vonatkerék pumpálása régi Ikarussal",
                            Priority = "ASAP",
                            Status = "InProgress"
                        },
                        new
                        {
                            TodoId = new Guid("992a47fb-4512-4e39-8670-7d9c18f59c8b"),
                            CategoryId = new Guid("e30d079b-30eb-41a5-b4ab-57628925554a"),
                            Created = new DateTime(2018, 12, 31, 23, 59, 30, 0, DateTimeKind.Unspecified),
                            Creator = "Teszt Elek",
                            Deadline = new DateTime(2019, 2, 12, 5, 30, 0, 0, DateTimeKind.Unspecified),
                            Deleted = false,
                            Description = "Mélyvízben.",
                            LastModified = new DateTime(2019, 2, 12, 5, 0, 0, 0, DateTimeKind.Unspecified),
                            Modifier = "Akárki",
                            Name = "Fedettpályás távolbalátás gyakorlása két vak egérrel.",
                            Priority = "High",
                            Status = "Completed"
                        });
                });

            modelBuilder.Entity("TodoModels.Core.DataModels.Todo", b =>
                {
                    b.HasOne("TodoModels.Core.DataModels.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });
#pragma warning restore 612, 618
        }
    }
}