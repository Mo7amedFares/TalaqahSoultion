using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talaqah.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveComputedFromExamSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "ExamSchedules",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComputedColumnSql: "\r\n            CASE\r\n                WHEN\r\n                    CAST(GETDATE() AS DATE) BETWEEN [StartDate] AND [EndDate]\r\n                    AND CAST(GETDATE() AS TIME) BETWEEN [StartTime] AND [EndTime]\r\n                THEN CAST(1 AS BIT)\r\n                ELSE CAST(0 AS BIT)\r\n            END");


            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ExamSchedules",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ExamSchedules",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ExamSchedules",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ExamSchedules",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "ExamSchedules",
                type: "bit",
                nullable: false,
                computedColumnSql: "\r\n            CASE\r\n                WHEN\r\n                    CAST(GETDATE() AS DATE) BETWEEN [StartDate] AND [EndDate]\r\n                    AND CAST(GETDATE() AS TIME) BETWEEN [StartTime] AND [EndTime]\r\n                THEN CAST(1 AS BIT)\r\n                ELSE CAST(0 AS BIT)\r\n            END",
                stored: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);
        }
    }
}
