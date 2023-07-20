using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addedModelproperitesToJobAndTalent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CV",
                table: "Talents");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Talents",
                type: "int",
                nullable: true);

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
                name: "FK_Talents_Jobs_JobId",
                table: "Talents",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Talents_Jobs_JobId",
                table: "Talents");

            migrationBuilder.DropTable(
                name: "JobApplicants");

            migrationBuilder.DropIndex(
                name: "IX_Talents_JobId",
                table: "Talents");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Talents");

            migrationBuilder.AddColumn<byte[]>(
                name: "CV",
                table: "Talents",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
