using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesBoard.Data.Migrations
{
    public partial class changedCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CartId",
                table: "Cart",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "Cart",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
