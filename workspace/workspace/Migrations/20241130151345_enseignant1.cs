using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workspace.Migrations
{
    /// <inheritdoc />
    public partial class enseignant1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnseigenentId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EnseigenentId",
                table: "AspNetUsers",
                column: "EnseigenentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_EnseigenentId",
                table: "AspNetUsers",
                column: "EnseigenentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UserId",
                table: "AspNetUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_EnseigenentId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UserId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EnseigenentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EnseigenentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");
        }
    }
}
