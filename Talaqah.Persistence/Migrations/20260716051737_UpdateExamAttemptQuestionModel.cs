using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talaqah.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExamAttemptQuestionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FinalCefrLevel",
                table: "ExamAttempts",
                type: "varchar(2)",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FinalCefrLevel",
                table: "ExamAttempts",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldMaxLength: 2,
                oldNullable: true);
        }
    }
}
