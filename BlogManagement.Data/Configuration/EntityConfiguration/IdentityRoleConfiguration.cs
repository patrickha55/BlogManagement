using BlogManagement.Common.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Data.Configuration.EntityConfiguration
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole<long>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<long>> builder)
        {
            builder.Property(r => r.Id).UseIdentityColumn();

            builder.HasData(
                new IdentityRole<long>
                {
                    Id = 1L,
                    Name = Roles.Administrator,
                    NormalizedName = Roles.Administrator.ToUpper()
                },
                new IdentityRole<long>
                {
                    Id = 2L,
                    Name = Roles.Author,
                    NormalizedName = Roles.Author.ToUpper()
                },
                new IdentityRole<long>
                {
                    Id = 3L,
                    Name = Roles.User,
                    NormalizedName = Roles.User.ToUpper()
                }
            );
        }
    }
}
