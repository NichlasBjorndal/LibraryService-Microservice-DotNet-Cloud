using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderService.Migrations
{
    public partial class simplifiedorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderedBook_OrderedBookId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "OrderedBook");

            migrationBuilder.DropIndex(
                name: "IX_Order_OrderedBookId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderedBookId",
                table: "Order");

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "BookId", "IsCompleted", "OrdTime" },
                values: new object[] { 2, 7, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "BookId", "IsCompleted", "OrdTime" },
                values: new object[] { 3, 8, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<int>(
                name: "OrderedBookId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderedBook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    AuthorLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedBook", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderedBookId",
                table: "Order",
                column: "OrderedBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderedBook_OrderedBookId",
                table: "Order",
                column: "OrderedBookId",
                principalTable: "OrderedBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
