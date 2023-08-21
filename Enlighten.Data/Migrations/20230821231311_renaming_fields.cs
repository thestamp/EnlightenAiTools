using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enlighten.Data.Migrations
{
    /// <inheritdoc />
    public partial class renaming_fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TextbookSummary",
                table: "Textbooks",
                newName: "Summary");

            migrationBuilder.RenameColumn(
                name: "ChapterName",
                table: "TextbookChapters",
                newName: "Summary");

            migrationBuilder.RenameColumn(
                name: "ChapterContent",
                table: "TextbookChapters",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "TextbookChapters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "TextbookChapters");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "Textbooks",
                newName: "TextbookSummary");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "TextbookChapters",
                newName: "ChapterName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TextbookChapters",
                newName: "ChapterContent");
        }
    }
}
