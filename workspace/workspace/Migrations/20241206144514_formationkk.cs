using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workspace.Migrations
{
    /// <inheritdoc />
    public partial class formationkk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormationId",
                table: "Matieres",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormationId",
                table: "Etudiants",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormationId",
                table: "Enseignants",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Formations",
                columns: table => new
                {
                    IdFormation = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duree = table.Column<int>(type: "int", nullable: true),
                    Prix = table.Column<float>(type: "real", nullable: true),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SalleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formations", x => x.IdFormation);
                    table.ForeignKey(
                        name: "FK_Formations_Salles_SalleId",
                        column: x => x.SalleId,
                        principalTable: "Salles",
                        principalColumn: "IdSalle",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matieres_FormationId",
                table: "Matieres",
                column: "FormationId");

            migrationBuilder.CreateIndex(
                name: "IX_Etudiants_FormationId",
                table: "Etudiants",
                column: "FormationId");

            migrationBuilder.CreateIndex(
                name: "IX_Enseignants_FormationId",
                table: "Enseignants",
                column: "FormationId");

            migrationBuilder.CreateIndex(
                name: "IX_Formations_SalleId",
                table: "Formations",
                column: "SalleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enseignants_Formations_FormationId",
                table: "Enseignants",
                column: "FormationId",
                principalTable: "Formations",
                principalColumn: "IdFormation",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Etudiants_Formations_FormationId",
                table: "Etudiants",
                column: "FormationId",
                principalTable: "Formations",
                principalColumn: "IdFormation");

            migrationBuilder.AddForeignKey(
                name: "FK_Matieres_Formations_FormationId",
                table: "Matieres",
                column: "FormationId",
                principalTable: "Formations",
                principalColumn: "IdFormation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enseignants_Formations_FormationId",
                table: "Enseignants");

            migrationBuilder.DropForeignKey(
                name: "FK_Etudiants_Formations_FormationId",
                table: "Etudiants");

            migrationBuilder.DropForeignKey(
                name: "FK_Matieres_Formations_FormationId",
                table: "Matieres");

            migrationBuilder.DropTable(
                name: "Formations");

            migrationBuilder.DropIndex(
                name: "IX_Matieres_FormationId",
                table: "Matieres");

            migrationBuilder.DropIndex(
                name: "IX_Etudiants_FormationId",
                table: "Etudiants");

            migrationBuilder.DropIndex(
                name: "IX_Enseignants_FormationId",
                table: "Enseignants");

            migrationBuilder.DropColumn(
                name: "FormationId",
                table: "Matieres");

            migrationBuilder.DropColumn(
                name: "FormationId",
                table: "Etudiants");

            migrationBuilder.DropColumn(
                name: "FormationId",
                table: "Enseignants");
        }
    }
}
