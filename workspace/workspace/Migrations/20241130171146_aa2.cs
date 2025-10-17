using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workspace.Migrations
{
    /// <inheritdoc />
    public partial class aa2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_EnseigenentId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ParentUserId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ParentUserId",
                table: "AspNetUsers",
                column: "ParentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ParentUserId",
                table: "AspNetUsers",
                column: "ParentUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ParentUserId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ParentUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ParentUserId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_EnseigenentId",
                table: "AspNetUsers",
                column: "EnseigenentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
