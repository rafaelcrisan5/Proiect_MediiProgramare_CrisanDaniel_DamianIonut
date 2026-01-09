using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace auto.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAngajatMasina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Functie",
                table: "Angajat");

            migrationBuilder.AddColumn<string>(
                name: "NrInmatriculare",
                table: "Masina",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NrInmatriculare",
                table: "Masina");

            migrationBuilder.AddColumn<string>(
                name: "Functie",
                table: "Angajat",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
