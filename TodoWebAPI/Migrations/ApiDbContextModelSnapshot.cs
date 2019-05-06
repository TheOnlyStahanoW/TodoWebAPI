﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoWebAPI.Core.DataModels;

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

            modelBuilder.Entity("TodoWebAPI.Core.DataModels.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bug")
                        .HasMaxLength(250);

                    b.Property<bool>("Epic");

                    b.Property<string>("Task");

                    b.HasKey("CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("TodoWebAPI.Core.DataModels.Todo", b =>
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
                });

            modelBuilder.Entity("TodoWebAPI.Core.DataModels.Todo", b =>
                {
                    b.HasOne("TodoWebAPI.Core.DataModels.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });
#pragma warning restore 612, 618
        }
    }
}
