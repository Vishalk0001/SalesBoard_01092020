using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesBoard.Data.Migrations
{
    public partial class Model_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Sales",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sales",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Cart",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cart",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cart");
        }
    }
}
