using BlogManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Data.Configuration.EntityConfiguration
{
    public class PostRatingConfiguration : IEntityTypeConfiguration<PostRating>
    {
        public void Configure(EntityTypeBuilder<PostRating> builder)
        {
            builder.ToTable("PostRatings");
            builder.HasKey(pu => pu.Id);
            builder.HasAlternateKey(pu => new
            {
                pu.PostId,
                pu.UserId
            });

            builder.Property(pu => pu.Id).UseIdentityColumn();
            builder.Property(pu => pu.Rating).HasColumnType("float").IsRequired();

            builder.HasOne(pu => pu.Post)
                .WithMany(p => p.PostRatings)
                .HasForeignKey(pu => pu.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pu => pu.User)
                .WithMany(u => u.PostUserRatings)
                .HasForeignKey(pu => pu.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
