using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talaqah.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixExamSoftDeleteFilters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_User_Institution_Type",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ExamSections",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_User_Institution_Type",
                table: "Users",
                sql: "([UserType] = 'SchoolStudent' AND [SchoolId] IS NOT NULL AND [CollegeId] IS NULL) OR\r\n              ([UserType] = 'CollegeStudent' AND [CollegeId] IS NOT NULL AND [SchoolId] IS NULL) OR\r\n              ([UserType] = 'Public' AND [SchoolId] IS NULL AND [CollegeId] IS NULL) OR\r\n              ([UserType] IS NULL AND [SchoolId] IS NULL AND [CollegeId] IS NULL)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_User_Institution_Type",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ExamSections",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddCheckConstraint(
                name: "CK_User_Institution_Type",
                table: "Users",
                sql: "([UserType] = 'SchoolStudent' AND [SchoolId] IS NOT NULL AND [CollegeId] IS NULL) OR ([UserType] = 'CollegeStudent' AND [CollegeId] IS NOT NULL AND [SchoolId] IS NULL) OR ([UserType] IS NULL AND [SchoolId] IS NULL AND [CollegeId] IS NULL)");
        }
    }
}
