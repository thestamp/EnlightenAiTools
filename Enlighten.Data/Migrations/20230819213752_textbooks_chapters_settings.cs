using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enlighten.Data.Migrations
{
    /// <inheritdoc />
    public partial class textbooks_chapters_settings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TextbookId",
                table: "TextbookChapters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Textbooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextbookSummary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuizSystemMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuizQuestionPrompt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuizAnswerPrompt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InquireSystemMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InquirePrompt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentStart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentEnd = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Textbooks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TextbookChapters_TextbookId",
                table: "TextbookChapters",
                column: "TextbookId");

            migrationBuilder.AddForeignKey(
                name: "FK_TextbookChapters_Textbooks_TextbookId",
                table: "TextbookChapters",
                column: "TextbookId",
                principalTable: "Textbooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextbookChapters_Textbooks_TextbookId",
                table: "TextbookChapters");

            migrationBuilder.DropTable(
                name: "Textbooks");

            migrationBuilder.DropIndex(
                name: "IX_TextbookChapters_TextbookId",
                table: "TextbookChapters");

            migrationBuilder.DropColumn(
                name: "TextbookId",
                table: "TextbookChapters");
        }
    }
}
