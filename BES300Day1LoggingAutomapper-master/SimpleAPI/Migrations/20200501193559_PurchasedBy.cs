using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleAPI.Migrations
{
    public partial class PurchasedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PurchasedBy",
                table: "ShoppingItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchasedBy",
                table: "ShoppingItems");
        }
    }
}
