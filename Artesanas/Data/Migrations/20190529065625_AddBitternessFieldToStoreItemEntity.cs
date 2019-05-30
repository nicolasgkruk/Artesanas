using Microsoft.EntityFrameworkCore.Migrations;

namespace Artesanas.Data.Migrations
{
    public partial class AddBitternessFieldToStoreItemEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bitterness",
                table: "StoreItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bitterness",
                table: "StoreItem");
        }
    }
}
