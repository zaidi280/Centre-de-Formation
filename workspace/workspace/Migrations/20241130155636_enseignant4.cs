using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workspace.Migrations
{
    /// <inheritdoc />
    public partial class enseignant4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnseigenentId",
                table: "enseignants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnseigenentId",
                table: "enseignants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
