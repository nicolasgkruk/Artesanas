using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Artesanas.Data.Migrations
{
    public partial class AddStoreItemAndMakerToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Maker",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    HighlightedWords = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Pairing = table.Column<string>(nullable: true),
                    IBU = table.Column<int>(nullable: false),
                    Gravity = table.Column<int>(nullable: false),
                    Alcohol = table.Column<float>(nullable: false),
                    Amount = table.Column<float>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    TipoId = table.Column<int>(nullable: false),
                    MakerId = table.Column<int>(nullable: false),
                    SubTipoId = table.Column<int>(nullable: false),
                    Price = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreItem_Maker_MakerId",
                        column: x => x.MakerId,
                        principalTable: "Maker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StoreItem_SubTipo_SubTipoId",
                        column: x => x.SubTipoId,
                        principalTable: "SubTipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StoreItem_Tipo_TipoId",
                        column: x => x.TipoId,
                        principalTable: "Tipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreItem_MakerId",
                table: "StoreItem",
                column: "MakerId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItem_SubTipoId",
                table: "StoreItem",
                column: "SubTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItem_TipoId",
                table: "StoreItem",
                column: "TipoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreItem");

            migrationBuilder.DropTable(
                name: "Maker");
        }
    }
}
