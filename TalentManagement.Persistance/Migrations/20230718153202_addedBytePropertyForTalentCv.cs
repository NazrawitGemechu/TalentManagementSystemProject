using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addedBytePropertyForTalentCv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "CV",
                table: "Talents",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CV",
                table: "Talents");
        }
    }
}
