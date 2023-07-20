using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class MadeSomeModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Talents_TalentId",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AspNetUsers_JobPosterId",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "JobPosterId",
                table: "Jobs",
                newName: "RecruterId");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_JobPosterId",
                table: "Jobs",
                newName: "IX_Jobs_RecruterId");

            migrationBuilder.RenameColumn(
                name: "TalentId",
                table: "Candidates",
                newName: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Jobs_JobId",
                table: "Candidates",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AspNetUsers_RecruterId",
                table: "Jobs",
                column: "RecruterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Jobs_JobId",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AspNetUsers_RecruterId",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "RecruterId",
                table: "Jobs",
                newName: "JobPosterId");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_RecruterId",
                table: "Jobs",
                newName: "IX_Jobs_JobPosterId");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "Candidates",
                newName: "TalentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Talents_TalentId",
                table: "Candidates",
                column: "TalentId",
                principalTable: "Talents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AspNetUsers_JobPosterId",
                table: "Jobs",
                column: "JobPosterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
