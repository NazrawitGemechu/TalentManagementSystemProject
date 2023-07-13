using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AgainUpdatedJobModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobStatus",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "OpeningNo",
                table: "Jobs",
                newName: "Vacancy");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostedDate",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "JobDeadline",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Vacancy",
                table: "Jobs",
                newName: "OpeningNo");

            migrationBuilder.AlterColumn<string>(
                name: "PostedDate",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "JobDeadline",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "JobStatus",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
