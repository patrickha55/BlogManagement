using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogManagement.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Mobile = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Intro = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    Profile = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.UniqueConstraint("UC_Email", x => x.Email);
                    table.UniqueConstraint("UC_Mobile", x => x.Mobile);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "Intro", "LastLogin", "LastName", "MiddleName", "Mobile", "PasswordHash", "Profile", "RegisteredAt" },
                values: new object[,]
                {
                    { 1L, "phatHa@mail.com", "Phat", "Intro 1", null, "Ha", "Tan", "09812374657384", "14F8F4BB8C0E79A02670A5FEA5682DA717A5B3D3DC7B1706F7A4BAB9AFAE18C2", "This is an author's profile information", new DateTime(2021, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2L, "mrWick@mail.com", "John", "Intro 2", null, "Wick", null, "91283874571", "14F8F4BB8C0E79A02670A5FEA5682DA717A5B3D3DC7B1706F7A4BAB9AFAE18C2", "This is an author's profile information", new DateTime(2021, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3L, "uCantSeeMe@mail.com", "John", "Intro 2", null, "Cena", null, "112401867092", "14F8F4BB8C0E79A02670A5FEA5682DA717A5B3D3DC7B1706F7A4BAB9AFAE18C2", "This is an author's profile information", new DateTime(2021, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4L, "whatTheRockIsCooking@mail.com", "Dwayne", "Intro 2", null, "Johnson", null, "091510847671", "14F8F4BB8C0E79A02670A5FEA5682DA717A5B3D3DC7B1706F7A4BAB9AFAE18C2", "This is an author's profile information", new DateTime(2021, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5L, "mrWorldwide@mail.com", "World", "Intro 2", null, "Wide", null, "8917501901111", "14F8F4BB8C0E79A02670A5FEA5682DA717A5B3D3DC7B1706F7A4BAB9AFAE18C2", "This is an author's profile information", new DateTime(2021, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
