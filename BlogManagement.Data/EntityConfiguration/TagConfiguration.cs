using BlogManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Data.EntityConfiguration
{
    /// <summary>
    /// This class stores Tag entity's configuration for Fluent API.
    /// Contains:
    ///     + Entity configurations.
    ///     + Property configurations.
    ///     + Relationship configurations.
    /// </summary>
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            #region Entity configurations.

            builder.HasKey(t => t.Id);

            #endregion

            #region Property configurations.

            builder.Property(t => t.Id)
                .UseIdentityColumn();

            builder.Property(t => t.Title)
                .HasColumnType("varchar")
                .HasMaxLength(75);
            builder.Property(t => t.MetaTitle)
                .HasColumnType("varchar")
                .HasMaxLength(100);
            builder.Property(t => t.Slug)
                .HasColumnType("varchar")
                .HasMaxLength(100);

            #endregion
        }
    }
}
