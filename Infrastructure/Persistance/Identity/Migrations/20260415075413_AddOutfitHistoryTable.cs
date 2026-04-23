using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddOutfitHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OutfitHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OriginalScore = table.Column<float>(type: "real", nullable: false),
                    ImprovedScore = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    TopImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BottomImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShoeImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessoryImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BagImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplacementsJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutfitHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutfitHistories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutfitHistories_UserId",
                table: "OutfitHistories",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutfitHistories");
        }
    }
}
