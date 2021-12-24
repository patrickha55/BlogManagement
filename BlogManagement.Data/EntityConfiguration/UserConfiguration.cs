using System;
using BlogManagement.Common.Extensions;
using BlogManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Data.EntityConfiguration
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
            builder.HasAlternateKey(u => u.PhoneNumber).HasName("UC_Mobile");
            builder.HasAlternateKey(u => u.Email).HasName("UC_Email");

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
                    PasswordHash = StringExtensions.HashString("Abc@123"),
                    RegisteredAt = new DateTime(2021, 12, 18),
                    Intro = "Intro 1",
                    Profile = "This is an author's profile information"
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
                    PasswordHash = StringExtensions.HashString("Abc@123"),
                    RegisteredAt = new DateTime(2021, 10, 01),
                    Intro = "Intro 2",
                    Profile = "This is an website admin"
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
                    PasswordHash = StringExtensions.HashString("Abc@123"),
                    RegisteredAt = new DateTime(2021, 10, 01),
                    Intro = "Intro 3",
                    Profile = "This is a normal user"
                }
            );

            #endregion
        }
    }
}
