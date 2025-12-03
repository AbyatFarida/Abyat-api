using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Abyat.Da.Migrations
{
    /// <inheritdoc />
    public partial class TitleArMigrationNullibilty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TbServiceCategories_TitleAr",
                table: "TbServiceCategories");

            migrationBuilder.AlterColumn<string>(
                name: "TitleAr",
                table: "TbServiceCategories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbServiceCategories_TitleAr",
                table: "TbServiceCategories",
                column: "TitleAr",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TbServiceCategories_TitleAr",
                table: "TbServiceCategories");

            migrationBuilder.AlterColumn<string>(
                name: "TitleAr",
                table: "TbServiceCategories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_TbServiceCategories_TitleAr",
                table: "TbServiceCategories",
                column: "TitleAr",
                unique: true,
                filter: "[TitleAr] IS NOT NULL");
        }
    }
}
