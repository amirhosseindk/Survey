using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class refgpt3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerMaxDegree",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "AnswerMaxValue",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "FillDatTime",
                table: "Answers",
                newName: "FillDateTime");

            migrationBuilder.RenameColumn(
                name: "AnswerMinValue",
                table: "Answers",
                newName: "AnswerValue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FillDateTime",
                table: "Answers",
                newName: "FillDatTime");

            migrationBuilder.RenameColumn(
                name: "AnswerValue",
                table: "Answers",
                newName: "AnswerMinValue");

            migrationBuilder.AddColumn<short>(
                name: "AnswerMaxDegree",
                table: "Answers",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "AnswerMaxValue",
                table: "Answers",
                type: "smallint",
                nullable: true);
        }
    }
}
