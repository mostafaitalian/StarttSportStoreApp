using Microsoft.EntityFrameworkCore.Migrations;

namespace StartSportStore.Migrations
{
    public partial class removepurchaseee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PurchasePrice",
                table: "Products",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
