using BlogManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Data.Configuration.EntityConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        /// <summary>
        /// This class stores Category entity's configuration for Fluent API.
        /// Contains:
        ///     + Entity configurations.
        ///     + Property configurations.
        ///     + Relationship configurations.
        /// </summary>
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            #region Entity configuration.

            builder.HasKey(c => c.Id);

            #endregion
            
            #region Property configurations

            builder.Property(c => c.Id).UseIdentityColumn();

            builder.Property(c => c.Title).HasColumnType("varchar").HasMaxLength(75);
            builder.Property(c => c.MetaTitle).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(c => c.Slug).HasColumnType("varchar").HasMaxLength(100);

            #endregion

            #region Relationship configuration

            builder.HasMany(c => c.ChildCategories)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion
        }
    }
}
