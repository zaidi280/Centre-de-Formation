using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workspace.Migrations
{
    /// <inheritdoc />
    public partial class salle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cours_Salles_SalleId",
                table: "Cours");

            migrationBuilder.DropForeignKey(
                name: "FK_Formations_Salles_SalleId",
                table: "Formations");

            migrationBuilder.DropIndex(
                name: "IX_Formations_SalleId",
                table: "Formations");

            migrationBuilder.DropIndex(
                name: "IX_Cours_SalleId",
                table: "Cours");

            migrationBuilder.DropColumn(
                name: "SalleId",
                table: "Formations");

            migrationBuilder.DropColumn(
                name: "SalleId",
                table: "Cours");

            migrationBuilder.AddColumn<string>(
                name: "SalleId",
                table: "Matieres",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Matieres_SalleId",
                table: "Matieres",
                column: "SalleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matieres_Salles_SalleId",
                table: "Matieres",
                column: "SalleId",
                principalTable: "Salles",
                principalColumn: "IdSalle",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matieres_Salles_SalleId",
                table: "Matieres");

            migrationBuilder.DropIndex(
                name: "IX_Matieres_SalleId",
                table: "Matieres");

            migrationBuilder.DropColumn(
                name: "SalleId",
                table: "Matieres");

            migrationBuilder.AddColumn<string>(
                name: "SalleId",
                table: "Formations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SalleId",
                table: "Cours",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Formations_SalleId",
                table: "Formations",
                column: "SalleId");

            migrationBuilder.CreateIndex(
                name: "IX_Cours_SalleId",
                table: "Cours",
                column: "SalleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cours_Salles_SalleId",
                table: "Cours",
                column: "SalleId",
                principalTable: "Salles",
                principalColumn: "IdSalle",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Formations_Salles_SalleId",
                table: "Formations",
                column: "SalleId",
                principalTable: "Salles",
                principalColumn: "IdSalle",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
