using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workspace.Migrations
{
    /// <inheritdoc />
    public partial class enseignant3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UserId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AnneesExperience",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateEmbauche",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Diplome",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdEnseignant",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Specialite",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "enseignants",
                columns: table => new
                {
                    IdEnseignant = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Specialite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnneesExperience = table.Column<int>(type: "int", nullable: true),
                    Diplome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateEmbauche = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnseigenentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enseignants", x => x.IdEnseignant);
                    table.ForeignKey(
                        name: "FK_enseignants_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_enseignants_UserId",
                table: "enseignants",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "enseignants");

            migrationBuilder.AddColumn<int>(
                name: "AnneesExperience",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEmbauche",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Diplome",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdEnseignant",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialite",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UserId",
                table: "AspNetUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
