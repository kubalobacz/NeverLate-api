using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeverLate_api.Migrations.Migrations
{
    public partial class SeedUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "Email", "NormalizedEmail", "PasswordHash", "UserName" },
                values: new object[] { "033e56d0-e75d-4835-a4d5-44a844d846ce", "test_user_1@test.com", "TEST_USER_1@TEST.COM", "AQAAAAEAACcQAAAAEGFwqIuy4X3jodwMn62pYNZJXIxKJdsReyXATknTglsjJVz7GOosYaXhTt0J2gW/uw==", "test_user_1" });*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "033e56d0-e75d-4835-a4d5-44a844d846ce");*/
        }
    }
}
