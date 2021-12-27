using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogManagement.Data.Migrations
{
    public partial class AddIsPublicColumnToUserAndTotalViewCountToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalViewed",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "ba5c36fd-a684-4b1a-81f3-5eefd6367a39");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "239d43d4-6042-4205-96bb-751377ad1655");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "85b1a843-bd1c-434d-91aa-320215f6f573");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3c384a78-a6a4-4981-a2c4-017bee5d8e82", "AQAAAAEAACcQAAAAEMnFzJhhaCHib0ewFJCqfYEPYHiYQRehHa0rdCt9PfpsZPEQ++xWGHKyMYPpzveE0w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "23c147c4-72b1-4ec2-ab00-79dda06d8763", "AQAAAAEAACcQAAAAEPtr68WNRWwZw4FWtyif0INOXzHoFVbYu/moTE9xSkNzhIJDyCTtyKB9rsr60NCWUA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3fe0eceb-115d-41a1-8067-46c75b877904", "AQAAAAEAACcQAAAAEKPpIuE1jkn+hcatLB8tsIxIOCFdoe4Mlchi+2zWdCDHCr2/e54HP0TfyyVUQxSgtg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalViewed",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "1cd6592e-66d8-4b09-8f66-c6f9908060d2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "64ac304d-3eac-4249-a8dd-b770c8d0c30b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "9e527318-fba6-4406-a472-e19a1b82f7d8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a8c0c6dc-89fa-4a19-b3f1-d1f4b4f30bbf", "AQAAAAEAACcQAAAAEKvpUU8AFBd/z5awefJ8riHsdl8AFT5dNRvcPJnRHLSfBG0p5NHI/MvKWMug3rikiQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3f62dbfb-7e18-466e-8055-8613b1f3deff", "AQAAAAEAACcQAAAAEHrTFNJhRg8QpsSL2/amwz00XhFYCZvfWebOL61GNRzLROMAUb5Qa0LSi7ddq+BcZw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7a0771ee-2d90-4be5-87bd-d217d7c071d1", "AQAAAAEAACcQAAAAEB6rYFnMRNYyhMTVhTAw/6cfiX58/s0lG/oiFiE9IzM5kcJSY03C3XpNL2COC3IOvg==" });
        }
    }
}
