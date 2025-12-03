using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Abyat.Da.Migrations
{
    /// <inheritdoc />
    public partial class TitleArMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "TbServiceCategories",
                newName: "TitleEn");

            migrationBuilder.RenameIndex(
                name: "IX_TbServiceCategories_Title",
                table: "TbServiceCategories",
                newName: "IX_TbServiceCategories_TitleEn");

            migrationBuilder.AddColumn<string>(
                name: "TitleAr",
                table: "TbServiceCategories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbServiceCategories_TitleAr",
                table: "TbServiceCategories",
                column: "TitleAr",
                unique: true,
                filter: "[TitleAr] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TbProcessSteps_TitleAr_Order",
                table: "TbProcessSteps",
                columns: new[] { "TitleAr", "Order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbProcessSteps_TitleEn_Order",
                table: "TbProcessSteps",
                columns: new[] { "TitleEn", "Order" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TbServiceCategories_TitleAr",
                table: "TbServiceCategories");

            migrationBuilder.DropIndex(
                name: "IX_TbProcessSteps_TitleAr_Order",
                table: "TbProcessSteps");

            migrationBuilder.DropIndex(
                name: "IX_TbProcessSteps_TitleEn_Order",
                table: "TbProcessSteps");

            migrationBuilder.DropColumn(
                name: "TitleAr",
                table: "TbServiceCategories");

            migrationBuilder.RenameColumn(
                name: "TitleEn",
                table: "TbServiceCategories",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_TbServiceCategories_TitleEn",
                table: "TbServiceCategories",
                newName: "IX_TbServiceCategories_Title");
        }
    }
}
