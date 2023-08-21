using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enlighten.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GptDataSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_GptDataSettings", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "TextbookChapters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextbookId = table.Column<int>(type: "int", nullable: false),
                    ChapterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChapterContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_TextbookChapters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextbookChapters_Textbooks_TextbookId",
                        column: x => x.TextbookId,
                        principalTable: "Textbooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TextbookChapters_TextbookId",
                table: "TextbookChapters",
                column: "TextbookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GptDataSettings");

            migrationBuilder.DropTable(
                name: "TextbookChapters");

            migrationBuilder.DropTable(
                name: "Textbooks");
        }
    }
}
