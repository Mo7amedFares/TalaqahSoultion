using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talaqah.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactorExamQuestionRule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamQuestionRules_Exams_ExamId",
                table: "ExamQuestionRules");

            migrationBuilder.DropIndex(
                name: "IX_ExamQuestionRule_ExamSection",
                table: "ExamQuestionRules");

            migrationBuilder.DropIndex(
                name: "IX_ExamQuestionRules_ExamId",
                table: "ExamQuestionRules");

            migrationBuilder.DropColumn(
                name: "ExamId",
                table: "ExamQuestionRules");

            migrationBuilder.RenameIndex(
                name: "UX_ExamQuestionRule_Exam_Skill_Level",
                table: "ExamQuestionRules",
                newName: "UX_ExamQuestionRule_ExamSection_CefrLevel");

            migrationBuilder.AlterColumn<string>(
                name: "CefrLevel",
                table: "ExamQuestionRules",
                type: "char(2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldMaxLength: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UX_ExamQuestionRule_ExamSection_CefrLevel",
                table: "ExamQuestionRules",
                newName: "UX_ExamQuestionRule_Exam_Skill_Level");

            migrationBuilder.AlterColumn<string>(
                name: "CefrLevel",
                table: "ExamQuestionRules",
                type: "varchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(2)");

            migrationBuilder.AddColumn<int>(
                name: "ExamId",
                table: "ExamQuestionRules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestionRule_ExamSection",
                table: "ExamQuestionRules",
                column: "ExamSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestionRules_ExamId",
                table: "ExamQuestionRules",
                column: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamQuestionRules_Exams_ExamId",
                table: "ExamQuestionRules",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id");
        }
    }
}
