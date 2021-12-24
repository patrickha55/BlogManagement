using System;
using BlogManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Data.Configuration.EntityConfiguration
{
    /// <summary>
    /// This class stores post entity's configuration for Fluent API.
    /// Contains:
    ///     + Entity configurations.
    ///     + Property configurations.
    ///     + Relationship configurations.
    ///     + Data seeding.
    /// </summary>
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            #region Entity configurations

            builder.HasKey(p => p.Id);
            builder.HasAlternateKey(p => p.Slug).HasName("UC_Slug");

            #endregion

            #region Property configurations

            builder.Property(p => p.Id)
                .UseIdentityColumn();
            builder.Property(p => p.Title)
                .HasColumnType("varchar")
                .HasMaxLength(75);
            builder.Property(p => p.MetaTitle)
                .HasColumnType("varchar")
                .HasMaxLength(100);
            builder.Property(p => p.Slug)
                .HasColumnType("varchar")
                .HasMaxLength(100);
            builder.Property(p => p.Summary)
                .HasColumnType("varchar")
                .HasMaxLength(2000);
            builder.Property(p => p.Published)
                .HasDefaultValue(0);
            builder.Property(p => p.CreatedAt)
                .ValueGeneratedOnAdd();


            #endregion

            #region Relationship configurations

            builder.HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(childPost => childPost.ParentPost)
                .WithMany(p => p.ChildPosts)
                .HasForeignKey(childPost => childPost.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Seed Data

            builder.HasData(
                new Post
                {
                    Id = 1L,
                    AuthorId = 1L,
                    Title = "What shape is the best?",
                    MetaTitle = "Best Shape",
                    Slug = "what-shape-is-the-best",
                    Summary = "Find out which shape is the best shape.",
                    CreatedAt = new DateTime(2021, 12, 18)
                },
                new Post
                {
                    Id = 2L,
                    AuthorId = 2L,
                    Title = "What is the best way to board a plane?",
                    MetaTitle = "Best Way To Board A Plane",
                    Slug = "best-way-to-board-a-plane",
                    Summary = "Find out which way is the best way to board a plane.",
                    CreatedAt = new DateTime(2021, 12, 18)
                },
                new Post
                {
                    Id = 3L,
                    AuthorId = 3L,
                    ParentId = 1L,
                    Title = "The hexagon",
                    MetaTitle = "Hexagon",
                    Slug = "hexagon",
                    Summary = "Let's talk about hexagon",
                    CreatedAt = new DateTime(2021, 12, 18)
                }
            );

            #endregion
        }
    }
}
