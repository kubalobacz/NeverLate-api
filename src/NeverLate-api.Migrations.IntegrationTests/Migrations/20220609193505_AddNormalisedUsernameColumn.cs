using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeverLate_api.Migrations.IntegrationTests.Migrations
{
    public partial class AddNormalisedUsernameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "033e56d0-e75d-4835-a4d5-44a844d846ce");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "AspNetUsers",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "4d6a04f5-f5f5-4267-9797-6db304f04105", "test_user_1@test.com", "TEST_USER_1@TEST.COM", null, "AQAAAAEAACcQAAAAEOQDwWuMGBlh/rmc2WTAM86K0+js0GxcPzISxN4dGan+4epO334YviOGICRArUZhgw==", "test_user_1" });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d6a04f5-f5f5-4267-9797-6db304f04105");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "Email", "NormalizedEmail", "PasswordHash", "UserName" },
                values: new object[] { "033e56d0-e75d-4835-a4d5-44a844d846ce", "test_user_1@test.com", "TEST_USER_1@TEST.COM", "AQAAAAEAACcQAAAAEGFwqIuy4X3jodwMn62pYNZJXIxKJdsReyXATknTglsjJVz7GOosYaXhTt0J2gW/uw==", "test_user_1" });
        }
    }
}
