
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTheTabels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkedForYear",
                table: "TalentExperiences",
                newName: "NumberOfYears");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Skills",
                newName: "SkillName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "EducationLevels",
                newName: "EducationLevelName");

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Talents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhoneNo",
                table: "Talents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyEmailAddress",
                table: "TalentExperiences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "Talents");

            migrationBuilder.DropColumn(
                name: "PhoneNo",
                table: "Talents");

            migrationBuilder.DropColumn(
                name: "CompanyEmailAddress",
                table: "TalentExperiences");

            migrationBuilder.RenameColumn(
                name: "NumberOfYears",
                table: "TalentExperiences",
                newName: "WorkedForYear");

            migrationBuilder.RenameColumn(
                name: "SkillName",
                table: "Skills",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "EducationLevelName",
                table: "EducationLevels",
                newName: "Name");
        }
    }
}
