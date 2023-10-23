using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enlighten.Data.Migrations
{
    /// <inheritdoc />
    public partial class sharable_textbooks_isPrivateShared : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsShared",
                table: "Textbooks",
                newName: "IsPrivateShared");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPrivateShared",
                table: "Textbooks",
                newName: "IsShared");
        }
    }
}
