using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talaqah.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpadteCEFRASVarcharExamAttempsExamQuestionRulsQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CefrLevel",
                table: "Questions",
                type: "varchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<string>(
                name: "CefrLevel",
                table: "ExamQuestionRules",
                type: "varchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CefrLevel",
                table: "Questions",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<string>(
                name: "CefrLevel",
                table: "ExamQuestionRules",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldMaxLength: 2);
        }
    }
}
