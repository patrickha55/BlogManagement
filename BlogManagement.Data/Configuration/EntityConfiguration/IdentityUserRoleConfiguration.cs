using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Data.Configuration.EntityConfiguration
{
    /// <summary>
    /// This class stores Identity User Role entity's configuration for Fluent API.
    /// Contains:
    ///     + Relationship configurations.
    /// </summary>
    public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<long>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<long>> builder)
        {
            builder.HasData(
                new IdentityUserRole<long>
                {
                    UserId = 2L,
                    RoleId = 1L
                },
                new IdentityUserRole<long>
                {
                    UserId = 1L,
                    RoleId = 2L
                },
                new IdentityUserRole<long>
                {
                    UserId = 3L,
                    RoleId = 3L
                });
        }
    }
}
