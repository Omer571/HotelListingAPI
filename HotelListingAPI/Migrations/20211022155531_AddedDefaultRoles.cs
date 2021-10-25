using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelListingAPI.Migrations
{
    public partial class AddedDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f70e778d-f6f9-476f-9674-60158c0a97f4", "c95e47cc-bf42-41e4-86d8-830bf303a367", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b0c0e1b6-2913-4842-84c8-25b28df94d9d", "87ea54b0-5a9c-445e-8f12-e6b3d0d043c8", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0c0e1b6-2913-4842-84c8-25b28df94d9d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f70e778d-f6f9-476f-9674-60158c0a97f4");
        }
    }
}
