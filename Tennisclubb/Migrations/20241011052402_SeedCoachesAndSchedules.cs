using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tennisclubb.Migrations
{
    /// <inheritdoc />
    public partial class SeedCoachesAndSchedules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Coaches");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
