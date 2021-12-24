﻿// <auto-generated />
using System;
using BlogManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlogManagement.Data.Migrations
{
    [DbContext(typeof(BlogManagementContext))]
    [Migration("20211218134755_AddPostCategoryPivotTable")]
    partial class AddPostCategoryPivotTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlogManagement.Data.Entities.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MetaTitle")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<long>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("varchar(75)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BlogManagement.Data.Entities.CategoryPost", b =>
                {
                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.HasKey("CategoryId", "PostId");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("IX_Post_Category_CategoryId");

                    b.HasIndex("PostId")
                        .HasDatabaseName("IX_Post_Category_PostId");

                    b.ToTable("Post_Category");
                });

            modelBuilder.Entity("BlogManagement.Data.Entities.Post", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("MetaTitle")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<byte>("Published")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)0);

                    b.Property<DateTime?>("PublishedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Summary")
                        .HasMaxLength(2000)
                        .HasColumnType("varchar(2000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("varchar(75)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasAlternateKey("Slug")
                        .HasName("UC_Slug");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ParentId");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AuthorId = 1L,
                            CreatedAt = new DateTime(2021, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MetaTitle = "Best Shape",
                            Published = (byte)0,
                            Slug = "what-shape-is-the-best",
                            Sumary = "Find out which shape is the best shape.",
                            Title = "What shape is the best?"
                        },
                        new
                        {
                            Id = 2L,
                            AuthorId = 2L,
                            CreatedAt = new DateTime(2021, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MetaTitle = "Best Way To Board A Plane",
                            Published = (byte)0,
                            Slug = "best-way-to-board-a-plane",
                            Sumary = "Find out which way is the best way to board a plane.",
                            Title = "What is the best way to board a plane?"
                        },
                        new
                        {
                            Id = 3L,
                            AuthorId = 3L,
                            CreatedAt = new DateTime(2021, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MetaTitle = "Hexagon",
                            ParentId = 1L,
                            Published = (byte)0,
                            Slug = "hexagon",
                            Sumary = "Let's talk about hexagon",
                            Title = "The hexagon"
                        });
                });

            modelBuilder.Entity("BlogManagement.Data.Entities.PostComment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.Property<byte>("Published")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)0);

                    b.Property<DateTime?>("PublishedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("PostId");

                    b.ToTable("PostComments");
                });

            modelBuilder.Entity("BlogManagement.Data.Entities.PostMeta", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasAlternateKey("PostId", "Key");

                    b.ToTable("PostMetas");
                });

            modelBuilder.Entity("BlogManagement.Data.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Intro")
                        .HasMaxLength(2000)
                        .HasColumnType("varchar(2000)");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Profile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisteredAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasAlternateKey("Email")
                        .HasName("UC_Email");

                    b.HasAlternateKey("Mobile")
                        .HasName("UC_Mobile");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Email = "phatHa@mail.com",
                            FirstName = "Phat",
                            Intro = "Intro 1",
                            LastName = "Ha",
                            MiddleName = "Tan",
                            Mobile = "09812374657384",
                            PasswordHash = "14F8F4BB8C0E79A02670A5FEA5682DA717A5B3D3DC7B1706F7A4BAB9AFAE18C2",
                            Profile = "This is an author's profile information",
                            RegisteredAt = new DateTime(2021, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2L,
                            Email = "mrWick@mail.com",
                            FirstName = "John",
                            Intro = "Intro 2",
                            LastName = "Wick",
                            Mobile = "91283874571",
                            PasswordHash = "14F8F4BB8C0E79A02670A5FEA5682DA717A5B3D3DC7B1706F7A4BAB9AFAE18C2",
                            Profile = "This is an author's profile information",
                            RegisteredAt = new DateTime(2021, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3L,
                            Email = "uCantSeeMe@mail.com",
                            FirstName = "John",
                            Intro = "Intro 2",
                            LastName = "Cena",
                            Mobile = "112401867092",
                            PasswordHash = "14F8F4BB8C0E79A02670A5FEA5682DA717A5B3D3DC7B1706F7A4BAB9AFAE18C2",
                            Profile = "This is an author's profile information",
                            RegisteredAt = new DateTime(2021, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4L,
                            Email = "whatTheRockIsCooking@mail.com",
                            FirstName = "Dwayne",
                            Intro = "Intro 2",
                            LastName = "Johnson",
                            Mobile = "091510847671",
                            PasswordHash = "14F8F4BB8C0E79A02670A5FEA5682DA717A5B3D3DC7B1706F7A4BAB9AFAE18C2",
                            Profile = "This is an author's profile information",
                            RegisteredAt = new DateTime(2021, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5L,
                            Email = "mrWorldwide@mail.com",
                            FirstName = "World",
                            Intro = "Intro 2",
                            LastName = "Wide",
                            Mobile = "8917501901111",
                            PasswordHash = "14F8F4BB8C0E79A02670A5FEA5682DA717A5B3D3DC7B1706F7A4BAB9AFAE18C2",
                            Profile = "This is an author's profile information",
                            RegisteredAt = new DateTime(2021, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("BlogManagement.Data.Entities.Category", b =>
                {
                    b.HasOne("BlogManagement.Data.Entities.Category", "ParentCategory")
                        .WithMany("ChildCategories")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("BlogManagement.Data.Entities.CategoryPost", b =>
                {
                    b.HasOne("BlogManagement.Data.Entities.Category", "Category")
                        .WithMany("CategoryPosts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BlogManagement.Data.Entities.Post", "Post")
                        .WithMany("CategoryPosts")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("BlogManagement.Data.Entities.Post", b =>
                {
                    b.HasOne("BlogManagement.Data.Entities.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BlogManagement.Data.Entities.Post", "ParentPost")
                        .WithMany("ChildPosts")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("ParentPost");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BlogManagement.Data.Entities.PostComment", b =>
                {
                    b.HasOne("BlogManagement.Data.Entities.PostComment", "ParentPostComment")
                        .WithMany("ChildPostComments")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("BlogManagement.Data.Entities.Post", "Post")
                        .WithMany("PostComments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ParentPostComment");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("BlogManagement.Data.Entities.PostMeta", b =>
                {
                    b.HasOne("BlogManagement.Data.Entities.Post", "Post")
                        .WithMany("PostMetas")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("BlogManagement.Data.Entities.Category", b =>
                {
                    b.Navigation("CategoryPosts");

                    b.Navigation("ChildCategories");
                });

            modelBuilder.Entity("BlogManagement.Data.Entities.Post", b =>
                {
                    b.Navigation("CategoryPosts");

                    b.Navigation("ChildPosts");

                    b.Navigation("PostComments");

                    b.Navigation("PostMetas");
                });

            modelBuilder.Entity("BlogManagement.Data.Entities.PostComment", b =>
                {
                    b.Navigation("ChildPostComments");
                });

            modelBuilder.Entity("BlogManagement.Data.Entities.User", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}