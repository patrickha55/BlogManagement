using System;
using BlogManagement.Common.Extensions;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Data.Configuration.EntityConfiguration
{
    /// <summary>
    /// This class stores user entity's configuration for Fluent API.
    /// Contains:
    ///     + Entity and property configuration.
    ///     + Data seeding.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            #region Entity configuration.

            builder.HasKey(u => u.Id);

            #endregion

            #region Property configuration.

            builder.Property(u => u.Id).UseIdentityColumn();
            builder.Property(u => u.FirstName).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(u => u.MiddleName).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(u => u.LastName).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(u => u.PhoneNumber).HasColumnType("varchar").HasMaxLength(15);
            builder.Property(u => u.Intro).HasColumnType("varchar").HasMaxLength(2000);

            #endregion


            #region Seed data

            var hasher = new PasswordHasher<User>();

            builder.HasData(
                new User
                {
                    UserName = "PhatHa",
                    NormalizedUserName = "PHATHA",
                    NormalizedEmail = "PHATHA@MAIL.COM",
                    EmailConfirmed = true,
                    Id = 1L,
                    FirstName = "Phat",
                    MiddleName = "Tan",
                    LastName = "Ha",
                    PhoneNumber = "09812374657384",
                    Email = "phatHa@mail.com",
                    PasswordHash = hasher.HashPassword(null, "Abc@123"),
                    RegisteredAt = new DateTime(2021, 12, 18),
                    Intro = "Intro 1",
                    Profile = "This is an author's profile information",
                    SecurityStamp = "32661E8D-66BD-42FB-9308-4E38D22E3051"
                },
                new User
                {
                    UserName = "MrWick",
                    NormalizedUserName = "MRWICK",
                    NormalizedEmail = "MRWICK@MAIL.COM",
                    EmailConfirmed = true,
                    Id = 2L,
                    FirstName = "John",
                    MiddleName = null,
                    LastName = "Wick",
                    PhoneNumber = "91283874571",
                    Email = "mrWick@mail.com",
                    PasswordHash = hasher.HashPassword(null, "Abc@123"),
                    RegisteredAt = new DateTime(2021, 10, 01),
                    Intro = "Intro 2",
                    Profile = "This is an website admin",
                    SecurityStamp = "1253A78A-FDBB-404F-B1D6-FD5BA90B72E7"
                },
                new User
                {
                    UserName = "AnonymousUser",
                    NormalizedUserName = "ANONYMOUSUSER",
                    NormalizedEmail = "ANONYMOUSUSER@MAIL.COM",
                    EmailConfirmed = true,
                    Id = 3L,
                    FirstName = "John",
                    MiddleName = null,
                    LastName = "Cena",
                    PhoneNumber = "12654274571",
                    Email = "AnonymousUser@mail.com",
                    PasswordHash = hasher.HashPassword(null, "Abc@123"),
                    RegisteredAt = new DateTime(2021, 10, 01),
                    Intro = "Intro 3",
                    Profile = "This is a normal user",
                    SecurityStamp = "CC0F6F9B-03ED-48F1-B618-B2E078009296"
                }
            );

            #endregion
        }
    }
}
