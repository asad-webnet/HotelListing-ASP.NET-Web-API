using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelListing.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Hotels");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Countries",
                newName: "CountryId");

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, "Pakistan", "PK" },
                    { 2, "India", "IND" },
                    { 3, "United States", "USA" }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "CountryId", "Name", "Rating" },
                values: new object[,]
                {
                    { 1, "Machi miani road", 1, "Quetta Hotel", 4.5 },
                    { 2, "Bahawalpur road", 2, "Hindu Hotel", 4.0 },
                    { 3, "Mountpalace burmingham", 3, "Burger King", 4.2000000000000002 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Countries",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
