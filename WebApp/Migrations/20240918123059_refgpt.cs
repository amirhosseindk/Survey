using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class refgpt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_UserId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_MultipleChoiceOption_Questions_MultipleChoiceQuestionId",
                table: "MultipleChoiceOption");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Questionnaires_QuestionnaireId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Courses_UserId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MultipleChoiceOption",
                table: "MultipleChoiceOption");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "DegreeQuestion_Answer",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "RangeQuestion_Answer",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TextQuestion_Answer",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "MultipleChoiceOption",
                newName: "MultipleChoiceOptions");

            migrationBuilder.RenameIndex(
                name: "IX_MultipleChoiceOption_MultipleChoiceQuestionId",
                table: "MultipleChoiceOptions",
                newName: "IX_MultipleChoiceOptions_MultipleChoiceQuestionId");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionnaireId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxDegree",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxValue",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinValue",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AnswerText",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MultipleChoiceOptions",
                table: "MultipleChoiceOptions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_AnswerOptionId",
                table: "Answers",
                column: "AnswerOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionnaireId",
                table: "Answers",
                column: "QuestionnaireId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_MultipleChoiceOptions_AnswerOptionId",
                table: "Answers",
                column: "AnswerOptionId",
                principalTable: "MultipleChoiceOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questionnaires_QuestionnaireId",
                table: "Answers",
                column: "QuestionnaireId",
                principalTable: "Questionnaires",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MultipleChoiceOptions_Questions_MultipleChoiceQuestionId",
                table: "MultipleChoiceOptions",
                column: "MultipleChoiceQuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Questionnaires_QuestionnaireId",
                table: "Questions",
                column: "QuestionnaireId",
                principalTable: "Questionnaires",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_MultipleChoiceOptions_AnswerOptionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questionnaires_QuestionnaireId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_MultipleChoiceOptions_Questions_MultipleChoiceQuestionId",
                table: "MultipleChoiceOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Questionnaires_QuestionnaireId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Answers_AnswerOptionId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionnaireId",
                table: "Answers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MultipleChoiceOptions",
                table: "MultipleChoiceOptions");

            migrationBuilder.DropColumn(
                name: "MaxDegree",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "MaxValue",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "MinValue",
                table: "Questions");

            migrationBuilder.RenameTable(
                name: "MultipleChoiceOptions",
                newName: "MultipleChoiceOption");

            migrationBuilder.RenameIndex(
                name: "IX_MultipleChoiceOptions_MultipleChoiceQuestionId",
                table: "MultipleChoiceOption",
                newName: "IX_MultipleChoiceOption_MultipleChoiceQuestionId");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionnaireId",
                table: "Questions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<short>(
                name: "Answer",
                table: "Questions",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "DegreeQuestion_Answer",
                table: "Questions",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Questions",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<short>(
                name: "RangeQuestion_Answer",
                table: "Questions",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextQuestion_Answer",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AnswerText",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MultipleChoiceOption",
                table: "MultipleChoiceOption",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_UserId",
                table: "Courses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_UserId",
                table: "Courses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MultipleChoiceOption_Questions_MultipleChoiceQuestionId",
                table: "MultipleChoiceOption",
                column: "MultipleChoiceQuestionId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Questionnaires_QuestionnaireId",
                table: "Questions",
                column: "QuestionnaireId",
                principalTable: "Questionnaires",
                principalColumn: "Id");
        }
    }
}
