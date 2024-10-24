using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tennisclubb.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToCoach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Coaches",
                type: "nvarchar(450)",  // Set kích thước tương ứng với UserId trong Identity
                nullable: true);

            // Nếu bạn muốn tạo khóa ngoại
            migrationBuilder.AddForeignKey(
                name: "FK_Coaches_AspNetUsers_UserId",
                table: "Coaches",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
