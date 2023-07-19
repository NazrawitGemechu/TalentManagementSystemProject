using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addedrlnshipBtnAppUserAndTalent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicantId",
                table: "Talents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ResumePosts",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TalentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumePosts", x => new { x.TalentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ResumePosts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResumePosts_Talents_TalentId",
                        column: x => x.TalentId,
                        principalTable: "Talents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Talents_ApplicantId",
                table: "Talents",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumePosts_UserId",
                table: "ResumePosts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Talents_AspNetUsers_ApplicantId",
                table: "Talents",
                column: "ApplicantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Talents_AspNetUsers_ApplicantId",
                table: "Talents");

            migrationBuilder.DropTable(
                name: "ResumePosts");

            migrationBuilder.DropIndex(
                name: "IX_Talents_ApplicantId",
                table: "Talents");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "Talents");
        }
    }
}
