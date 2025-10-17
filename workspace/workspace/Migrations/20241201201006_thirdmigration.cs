using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workspace.Migrations
{
    /// <inheritdoc />
    public partial class thirdmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matieres",
                columns: table => new
                {
                    IdMatiere = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomMaiere = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VolumeHoraire = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matieres", x => x.IdMatiere);
                });

            migrationBuilder.CreateTable(
                name: "Salles",
                columns: table => new
                {
                    IdSalle = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomSalle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacite = table.Column<int>(type: "int", nullable: true),
                    TypeSalle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Equipement = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salles", x => x.IdSalle);
                });

            migrationBuilder.CreateTable(
                name: "Cours",
                columns: table => new
                {
                    IdCour = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Chapitre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateHeure = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MatiereId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EnseignantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SalleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cours", x => x.IdCour);
                    table.ForeignKey(
                        name: "FK_Cours_Enseignants_EnseignantId",
                        column: x => x.EnseignantId,
                        principalTable: "Enseignants",
                        principalColumn: "IdEnseignant",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cours_Matieres_MatiereId",
                        column: x => x.MatiereId,
                        principalTable: "Matieres",
                        principalColumn: "IdMatiere",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cours_Salles_SalleId",
                        column: x => x.SalleId,
                        principalTable: "Salles",
                        principalColumn: "IdSalle",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cours_EnseignantId",
                table: "Cours",
                column: "EnseignantId");

            migrationBuilder.CreateIndex(
                name: "IX_Cours_MatiereId",
                table: "Cours",
                column: "MatiereId");

            migrationBuilder.CreateIndex(
                name: "IX_Cours_SalleId",
                table: "Cours",
                column: "SalleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cours");

            migrationBuilder.DropTable(
                name: "Matieres");

            migrationBuilder.DropTable(
                name: "Salles");
        }
    }
}
