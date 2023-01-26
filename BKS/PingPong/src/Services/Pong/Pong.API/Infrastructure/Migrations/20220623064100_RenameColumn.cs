using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pong.API.Infrastructure.Migrations
{
    public partial class RenameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UseId",
                table: "PongMessages",
                newName: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PongMessages",
                newName: "UseId");
        }
    }
}
