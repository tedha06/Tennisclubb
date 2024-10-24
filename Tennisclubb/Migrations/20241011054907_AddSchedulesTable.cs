using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tennisclubb.Migrations
{
    /// <inheritdoc />
    public partial class AddSchedulesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(nullable: false),
                    EventDate = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    CoachId = table.Column<int>(nullable: true)  // Assuming CoachId is nullable
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }
    }
}
