using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResturantBooking.Migrations
{
    /// <inheritdoc />
    public partial class EmailToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNbr",
                table: "Customers",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "test@gmail.com");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Email",
                value: "nytest@gmail.com");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Customers",
                newName: "PhoneNbr");
        }
    }
}
