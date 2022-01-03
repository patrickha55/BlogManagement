using BlogManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Data.Configuration.EntityConfiguration
{
    /// <summary>
    /// This class stores PostRating entity's configuration for Fluent API.
    /// Contains:
    ///     + Entity configurations.
    ///     + Property configurations.
    ///     + Relationship configurations.
    /// </summary>
    public class PostRatingConfiguration : IEntityTypeConfiguration<PostRating>
    {
        public void Configure(EntityTypeBuilder<PostRating> builder)
        {
            #region MyRegion

            builder.ToTable("PostRatings");
            builder.HasKey(pu => pu.Id);
            builder.HasAlternateKey(pu => new
            {
                pu.PostId,
                pu.UserId
            });

            #endregion

            #region Property configurations

            builder.Property(pu => pu.Id).UseIdentityColumn();
            builder.Property(pu => pu.Rating).HasColumnType("float").IsRequired();

            #endregion

            #region Relationship configurations

            builder.HasOne(pu => pu.Post)
                .WithMany(p => p.PostRatings)
                .HasForeignKey(pu => pu.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pu => pu.User)
                .WithMany(u => u.PostUserRatings)
                .HasForeignKey(pu => pu.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion
        }
    }
}
