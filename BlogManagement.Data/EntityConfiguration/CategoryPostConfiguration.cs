using BlogManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Data.EntityConfiguration
{
    /// <summary>
    /// This class stores CategoryPost entity's configuration for Fluent API.
    /// Contains:
    ///     + Entity configurations.
    ///     + Relationship configurations.
    /// </summary>
    public class CategoryPostConfiguration : IEntityTypeConfiguration<CategoryPost>
    {
        public void Configure(EntityTypeBuilder<CategoryPost> builder)
        {
            #region Entity configurations

            builder.ToTable("Post_Category");

            builder.HasKey(cp => new
            {
                cp.CategoryId,
                cp.PostId
            });

            builder.HasIndex(cp => cp.PostId)
                .HasDatabaseName("IX_Post_Category_PostId");
            builder.HasIndex(cp => cp.CategoryId)
                .HasDatabaseName("IX_Post_Category_CategoryId");

            #endregion

            #region Relationship configurations.

            builder.HasOne(cp => cp.Category)
                .WithMany(c => c.CategoryPosts)
                .HasForeignKey(cp => cp.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(cp => cp.Post)
                .WithMany(c => c.CategoryPosts)
                .HasForeignKey(cp => cp.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion
        }
    }
}
