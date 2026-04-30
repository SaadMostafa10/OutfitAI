using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace Persistance.Identity.Migrations
{
    /// <inheritdoc />
    public partial class OutfitsRecommendationsModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. حذف الـ Primary Key الأول
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSavedOutfits",
                table: "UserSavedOutfits");

            // 2. حذف الـ Index
            migrationBuilder.DropIndex(
                name: "IX_UserSavedOutfits_UserId",
                table: "UserSavedOutfits");

            // 3. تغيير نوع الـ Column
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserSavedOutfits",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            // 4. إرجاع الـ Primary Key
            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSavedOutfits",
                table: "UserSavedOutfits",
                columns: new[] { "UserId", "OutfitId" });

            // 5. إرجاع الـ Index
            migrationBuilder.CreateIndex(
                name: "IX_UserSavedOutfits_UserId",
                table: "UserSavedOutfits",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 1. حذف الـ Primary Key
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSavedOutfits",
                table: "UserSavedOutfits");

            // 2. حذف الـ Index
            migrationBuilder.DropIndex(
                name: "IX_UserSavedOutfits_UserId",
                table: "UserSavedOutfits");

            // 3. رجوع نوع الـ Column لـ int
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserSavedOutfits",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            // 4. إرجاع الـ Primary Key
            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSavedOutfits",
                table: "UserSavedOutfits",
                columns: new[] { "UserId", "OutfitId" });

            // 5. إرجاع الـ Index
            migrationBuilder.CreateIndex(
                name: "IX_UserSavedOutfits_UserId",
                table: "UserSavedOutfits",
                column: "UserId");
        }
    }
}