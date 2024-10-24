using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tennisclubb.Migrations
{
    /// <inheritdoc />
    public partial class AddSpecialtyAndFullNameToCoach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "Coaches");
        }
    }
}
