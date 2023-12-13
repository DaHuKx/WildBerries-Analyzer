using Microsoft.EntityFrameworkCore.Migrations;

namespace WildBerriesAnalyzer.Data.Migrations
{
    public partial class db_idinmarket_uniqueAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Products_IdInMarket",
                table: "Products",
                column: "IdInMarket",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_IdInMarket",
                table: "Products");
        }
    }
}
