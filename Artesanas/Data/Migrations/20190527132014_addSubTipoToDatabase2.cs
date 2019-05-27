using Microsoft.EntityFrameworkCore.Migrations;

namespace Artesanas.Data.Migrations
{
    public partial class addSubTipoToDatabase2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTipo_Tipo_CategoryId",
                table: "SubTipo");

            migrationBuilder.DropIndex(
                name: "IX_SubTipo_CategoryId",
                table: "SubTipo");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "SubTipo");

            migrationBuilder.CreateIndex(
                name: "IX_SubTipo_TipoId",
                table: "SubTipo",
                column: "TipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTipo_Tipo_TipoId",
                table: "SubTipo",
                column: "TipoId",
                principalTable: "Tipo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTipo_Tipo_TipoId",
                table: "SubTipo");

            migrationBuilder.DropIndex(
                name: "IX_SubTipo_TipoId",
                table: "SubTipo");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "SubTipo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubTipo_CategoryId",
                table: "SubTipo",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTipo_Tipo_CategoryId",
                table: "SubTipo",
                column: "CategoryId",
                principalTable: "Tipo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
