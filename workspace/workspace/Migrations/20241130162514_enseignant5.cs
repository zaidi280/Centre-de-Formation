using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workspace.Migrations
{
    /// <inheritdoc />
    public partial class enseignant5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_enseignants_AspNetUsers_UserId",
                table: "enseignants");

            migrationBuilder.DropIndex(
                name: "IX_enseignants_UserId",
                table: "enseignants");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EnseigenentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "enseignants");

            migrationBuilder.AddColumn<string>(
                name: "EnseigenentId",
                table: "enseignants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EnseigenentId",
                table: "AspNetUsers",
                column: "EnseigenentId",
                unique: true,
                filter: "[EnseigenentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_enseignants_EnseigenentId",
                table: "AspNetUsers",
                column: "EnseigenentId",
                principalTable: "enseignants",
                principalColumn: "IdEnseignant");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_enseignants_EnseigenentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EnseigenentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EnseigenentId",
                table: "enseignants");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "enseignants",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_enseignants_UserId",
                table: "enseignants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EnseigenentId",
                table: "AspNetUsers",
                column: "EnseigenentId");

            migrationBuilder.AddForeignKey(
                name: "FK_enseignants_AspNetUsers_UserId",
                table: "enseignants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
