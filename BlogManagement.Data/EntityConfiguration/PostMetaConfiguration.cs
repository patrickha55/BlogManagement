using BlogManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Data.EntityConfiguration
{
    /// <summary>
    /// This class stores PostMeta entity's configuration for Fluent API.
    /// Contains:
    ///     + Entity configurations.
    ///     + Property configurations.
    ///     + Relationship configurations.
    /// </summary>
    public class PostMetaConfiguration : IEntityTypeConfiguration<PostMeta>
    {
        public void Configure(EntityTypeBuilder<PostMeta> builder)
        {
            #region Entity configurations.

            builder.HasKey(pm => pm.Id);
            builder.HasAlternateKey(pm => new
            {
                pm.PostId,
                pm.Key
            });

            #endregion

            #region MyRegion

            builder.Property(pm => pm.Id).UseIdentityColumn();

            builder.Property(pm => pm.Key)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            #endregion


            #region Relationship configurations.

            builder.HasOne(pm => pm.Post)
                .WithMany(p => p.PostMetas)
                .HasForeignKey(pm => pm.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion
        }
    }
}
