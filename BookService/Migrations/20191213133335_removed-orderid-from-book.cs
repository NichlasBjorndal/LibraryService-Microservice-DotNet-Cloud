using Microsoft.EntityFrameworkCore.Migrations;

namespace BookService.Migrations
{
    public partial class removedorderidfrombook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Book");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Book",
                type: "int",
                nullable: true);
        }
    }
}
