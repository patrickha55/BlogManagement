using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using BlogManagement.Common.Common;

namespace BlogManagement.Data.EntityConfiguration
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
                    Name = Roles.Administrator.ToString(),
                    NormalizedName = Roles.Administrator.ToString().ToUpper()
                },
                new IdentityRole<long>
                {
                    Id = 2L,
                    Name = Roles.Author.ToString(),
                    NormalizedName = Roles.Author.ToString().ToUpper()
                },
                new IdentityRole<long>
                {
                    Id = 3L,
                    Name = Roles.User.ToString(),
                    NormalizedName = Roles.User.ToString().ToUpper()
                }
            );
        }
    }
}
