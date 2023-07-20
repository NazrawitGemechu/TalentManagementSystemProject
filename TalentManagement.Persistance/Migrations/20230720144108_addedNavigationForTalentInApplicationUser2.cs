using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addedNavigationForTalentInApplicationUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Talents_TalentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TalentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TalentId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicantId",
                table: "Talents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Talents_ApplicantId",
                table: "Talents",
                column: "ApplicantId",
                unique: true,
                filter: "[ApplicantId] IS NOT NULL");

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

            migrationBuilder.DropIndex(
                name: "IX_Talents_ApplicantId",
                table: "Talents");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "Talents");

            migrationBuilder.AddColumn<int>(
                name: "TalentId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TalentId",
                table: "AspNetUsers",
                column: "TalentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Talents_TalentId",
                table: "AspNetUsers",
                column: "TalentId",
                principalTable: "Talents",
                principalColumn: "Id");
        }
    }
}
