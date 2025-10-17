using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workspace.Migrations
{
    /// <inheritdoc />
    public partial class AddEnseignantsAndEtudiants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_enseignants_AspNetUsers_UserId",
                table: "enseignants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_enseignants",
                table: "enseignants");

            migrationBuilder.DropIndex(
                name: "IX_enseignants_UserId",
                table: "enseignants");

            migrationBuilder.DropColumn(
                name: "EnseigenentId",
                table: "enseignants");

            migrationBuilder.RenameTable(
                name: "enseignants",
                newName: "Enseignants");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Enseignants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enseignants",
                table: "Enseignants",
                column: "IdEnseignant");

            migrationBuilder.CreateTable(
                name: "Etudiants",
                columns: table => new
                {
                    IdEtudiant = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Classe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Niveau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInscription = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etudiants", x => x.IdEtudiant);
                    table.ForeignKey(
                        name: "FK_Etudiants_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enseignants_UserId",
                table: "Enseignants",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Etudiants_UserId",
                table: "Etudiants",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enseignants_AspNetUsers_UserId",
                table: "Enseignants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enseignants_AspNetUsers_UserId",
                table: "Enseignants");

            migrationBuilder.DropTable(
                name: "Etudiants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enseignants",
                table: "Enseignants");

            migrationBuilder.DropIndex(
                name: "IX_Enseignants_UserId",
                table: "Enseignants");

            migrationBuilder.RenameTable(
                name: "Enseignants",
                newName: "enseignants");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "enseignants",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "EnseigenentId",
                table: "enseignants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_enseignants",
                table: "enseignants",
                column: "IdEnseignant");

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
    }
}
