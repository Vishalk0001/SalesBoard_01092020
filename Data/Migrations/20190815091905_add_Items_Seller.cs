using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesBoard.Data.Migrations
{
    public partial class add_Items_Seller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Seller",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seller",
                table: "Items");
        }
    }
}
