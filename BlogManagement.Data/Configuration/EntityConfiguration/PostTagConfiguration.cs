using BlogManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Data.Configuration.EntityConfiguration
{
    /// <summary>
    /// This class stores PostTag entity's configuration for Fluent API.
    /// Contains:
    ///     + Entity configurations.
    ///     + Relationship configurations.
    /// </summary>
    public class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
    {
        public void Configure(EntityTypeBuilder<PostTag> builder)
        {
            #region Entity Configurations

            builder.ToTable("Post_Tag");

            builder.HasKey(cp => new
            {
                cp.PostId,
                cp.TagId
            });

            builder.HasIndex(cp => cp.PostId).HasDatabaseName("IX_Post_Category_PostId");
            builder.HasIndex(cp => cp.TagId).HasDatabaseName("IX_Post_Category_TagId");

            #endregion

            #region Relationship configurations.

            builder.HasOne(cp => cp.Post)
                .WithMany(c => c.PostTags)
                .HasForeignKey(cp => cp.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cp => cp.Tag)
                .WithMany(c => c.PostTags)
                .HasForeignKey(cp => cp.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}
