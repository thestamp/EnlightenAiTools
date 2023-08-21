using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enlighten.Data.Migrations
{
    /// <inheritdoc />
    public partial class gptSettings : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GptDataSettings");
        }
    }
}
