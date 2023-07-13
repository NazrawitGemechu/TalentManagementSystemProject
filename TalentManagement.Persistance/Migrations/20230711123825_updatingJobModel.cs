using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class updatingJobModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobDeadline",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JobStatus",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OpeningNo",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PostedDate",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobDeadline",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobStatus",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "OpeningNo",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "PostedDate",
                table: "Jobs");
        }
    }
}
