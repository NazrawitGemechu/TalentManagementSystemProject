using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UndidThePreviousModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResumePosts_AspNetUsers_UserId",
                table: "ResumePosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ResumePosts_Talents_TalentId",
                table: "ResumePosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Talents_Jobs_JobId",
                table: "Talents");

            migrationBuilder.DropTable(
                name: "JobApplicants");

            migrationBuilder.DropIndex(
                name: "IX_Talents_JobId",
                table: "Talents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResumePosts",
                table: "ResumePosts");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Talents");

            migrationBuilder.RenameTable(
                name: "ResumePosts",
                newName: "Candidates");

            migrationBuilder.RenameIndex(
                name: "IX_ResumePosts_UserId",
                table: "Candidates",
                newName: "IX_Candidates_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Candidates",
                table: "Candidates",
                columns: new[] { "TalentId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_AspNetUsers_UserId",
                table: "Candidates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Talents_TalentId",
                table: "Candidates",
                column: "TalentId",
                principalTable: "Talents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_AspNetUsers_UserId",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Talents_TalentId",
                table: "Candidates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Candidates",
                table: "Candidates");

            migrationBuilder.RenameTable(
                name: "Candidates",
                newName: "ResumePosts");

            migrationBuilder.RenameIndex(
                name: "IX_Candidates_UserId",
                table: "ResumePosts",
                newName: "IX_ResumePosts_UserId");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Talents",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResumePosts",
                table: "ResumePosts",
                columns: new[] { "TalentId", "UserId" });

            migrationBuilder.CreateTable(
                name: "JobApplicants",
                columns: table => new
                {
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplicants", x => new { x.ApplicantId, x.JobId });
                    table.ForeignKey(
                        name: "FK_JobApplicants_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobApplicants_Talents_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Talents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Talents_JobId",
                table: "Talents",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicants_JobId",
                table: "JobApplicants",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResumePosts_AspNetUsers_UserId",
                table: "ResumePosts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResumePosts_Talents_TalentId",
                table: "ResumePosts",
                column: "TalentId",
                principalTable: "Talents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Talents_Jobs_JobId",
                table: "Talents",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }
    }
}
