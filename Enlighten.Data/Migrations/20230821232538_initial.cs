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
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "TextbookUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextbookId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_TextbookUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextbookUnits_Textbooks_TextbookId",
                        column: x => x.TextbookId,
                        principalTable: "Textbooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TextbookUnits_TextbookId",
                table: "TextbookUnits",
                column: "TextbookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GptDataSettings");

            migrationBuilder.DropTable(
                name: "TextbookUnits");

            migrationBuilder.DropTable(
                name: "Textbooks");
        }
    }
}
