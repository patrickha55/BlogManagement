using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogManagement.Data.Migrations
{
    public partial class AddIsDisableColumnToUserTableAndChangesSomeConstraintsInDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Category_Categories_CategoryId",
                table: "Post_Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Category_Posts_PostId",
                table: "Post_Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Tag_Posts_PostId",
                table: "Post_Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Tag_Tags_TagId",
                table: "Post_Tag");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "8aa60b89-0330-47e3-90a7-244a40d53f17");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "e3d17be2-6c54-406c-972c-dd536e016ddc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "0c9c702e-aec8-48ea-a826-1fefd022f50f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "79454a04-75d5-4eed-894a-f70c19fa69a8", "AQAAAAEAACcQAAAAEOZhGkE6IXus9R6H9zjr8/HSID+td8i69AJQ1JNJt3xYCQzmHsopyCMkUCGDi7tZog==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cf5d5f6d-a3d2-4c79-bc75-1789e214347e", "AQAAAAEAACcQAAAAEEfuSuSQnwNl9/mlBF6QlyoFq4fwGYIdKCYgNGZIn4oh849mXzglHawTIzhBz7Iz4A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "72e74455-b3f0-4ff3-9f19-e3b460f6737d", "AQAAAAEAACcQAAAAELJICHb++PprOCyKGnxMgIW771tLPuPusqvFRDsTAKo70uquwWdTEz0HPjmauXBb0Q==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Category_Categories_CategoryId",
                table: "Post_Category",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Category_Posts_PostId",
                table: "Post_Category",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Tag_Posts_PostId",
                table: "Post_Tag",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Tag_Tags_TagId",
                table: "Post_Tag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Category_Categories_CategoryId",
                table: "Post_Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Category_Posts_PostId",
                table: "Post_Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Tag_Posts_PostId",
                table: "Post_Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Tag_Tags_TagId",
                table: "Post_Tag");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "AspNetUsers");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Category_Categories_CategoryId",
                table: "Post_Category",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Category_Posts_PostId",
                table: "Post_Category",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Tag_Posts_PostId",
                table: "Post_Tag",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Tag_Tags_TagId",
                table: "Post_Tag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id");
        }
    }
}
