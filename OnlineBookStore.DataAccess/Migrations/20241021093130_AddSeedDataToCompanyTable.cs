using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineBookStore.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataToCompanyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "City", "Name", "PhoneNumber", "PostalCode", "State", "StreetAddress" },
                values: new object[,]
                {
                    { 1, "Tech City", "Tech Solution", "1239823849", 123, "IL", "123 Tech St" },
                    { 2, "Vid City", "Vivid Books", "1928302983", 666666, "IL", "999 Viv St" },
                    { 3, "Lala Land", "Readers Club", "1123323323", 999999, "NY", "999 Main St" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
