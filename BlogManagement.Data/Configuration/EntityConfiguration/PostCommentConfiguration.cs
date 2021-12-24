using BlogManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Data.Configuration.EntityConfiguration
{
    /// <summary>
    /// This class stores PostComment entity's configuration for Fluent API.
    /// Contains:
    ///     + Entity configurations.
    ///     + Property configurations.
    ///     + Relationship configurations.
    /// </summary>
    public class PostCommentConfiguration : IEntityTypeConfiguration<PostComment>
    {
        public void Configure(EntityTypeBuilder<PostComment> builder)
        {
            #region Entity configuration

            builder.HasKey(pc => pc.Id);

            #endregion

            #region Property configurations

            builder.Property(pc => pc.Id)
                .UseIdentityColumn();

            builder.Property(pc => pc.Title)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(pc => pc.Published)
                .HasDefaultValue(0);

            #endregion

            #region Relationship configurations

            builder.HasOne(pc => pc.Post)
                .WithMany(p => p.PostComments)
                .HasForeignKey(pc => pc.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(pc => pc.ChildPostComments)
                .WithOne(p => p.ParentPostComment)
                .HasForeignKey(pc => pc.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion
        }
    }
}
