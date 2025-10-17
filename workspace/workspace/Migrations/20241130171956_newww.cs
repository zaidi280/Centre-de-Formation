using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workspace.Migrations
{
    /// <inheritdoc />
    public partial class newww : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ParentUserId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_enseignants_EnseigenentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EnseigenentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ParentUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EnseigenentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ParentUserId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "enseignants",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_enseignants_UserId",
                table: "enseignants",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_enseignants_AspNetUsers_UserId",
                table: "enseignants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_enseignants_AspNetUsers_UserId",
                table: "enseignants");

            migrationBuilder.DropIndex(
                name: "IX_enseignants_UserId",
                table: "enseignants");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "enseignants");

            migrationBuilder.AddColumn<string>(
                name: "EnseigenentId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentUserId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EnseigenentId",
                table: "AspNetUsers",
                column: "EnseigenentId",
                unique: true,
                filter: "[EnseigenentId] IS NOT NULL");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_enseignants_EnseigenentId",
                table: "AspNetUsers",
                column: "EnseigenentId",
                principalTable: "enseignants",
                principalColumn: "IdEnseignant");
        }
    }
}
