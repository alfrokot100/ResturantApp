using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResturantBooking.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "PasswordHash", "Username" },
                values: new object[] { 1, "testhash123", "admin" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Name", "PhoneNbr" },
                values: new object[,]
                {
                    { 1, "Lasse Ricardo", "0703873563" },
                    { 2, "Alfie Smith", "0765376534" }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Description", "ImageURL", "IsPopular", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Klassisk pizza med mozzarella", null, true, "Pizza Margherita", 99m },
                    { 2, "Sallad med kyckling och parmesan", null, false, "Caesarsallad", 120m }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "Capacity", "Number" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 2, 4, 2 },
                    { 3, 6, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
