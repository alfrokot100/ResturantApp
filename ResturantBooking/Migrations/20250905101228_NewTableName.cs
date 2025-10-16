using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResturantBooking.Migrations
{
    /// <inheritdoc />
    public partial class NewTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Tables_TableId_FK",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.CreateTable(
                name: "ResturantTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResturantTables", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ResturantTables",
                columns: new[] { "Id", "Capacity", "Number" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 2, 4, 2 },
                    { 3, 6, 3 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_ResturantTables_TableId_FK",
                table: "Bookings",
                column: "TableId_FK",
                principalTable: "ResturantTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_ResturantTables_TableId_FK",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "ResturantTables");

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
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

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Tables_TableId_FK",
                table: "Bookings",
                column: "TableId_FK",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
