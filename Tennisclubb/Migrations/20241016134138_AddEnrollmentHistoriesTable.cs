using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tennisclubb.Migrations
{
    /// <inheritdoc />
    public partial class AddEnrollmentHistoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
    name: "EnrollmentHistories",
    columns: table => new
    {
        Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
        EventId = table.Column<int>(nullable: false),
        EventName = table.Column<string>(nullable: false),
        EventDate = table.Column<DateTime>(nullable: false),
        UserId = table.Column<string>(nullable: false)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_EnrollmentHistories", x => x.Id);
    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoachName",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "CoachName",
                table: "EnrollmentHistories");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
