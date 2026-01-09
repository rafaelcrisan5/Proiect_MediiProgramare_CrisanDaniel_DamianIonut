using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace auto.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Angajat",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Functie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataAngajarii = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Angajat", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Serviciu",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denumire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pret = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serviciu", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Masina",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masina", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Masina_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Programare",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MasinaID = table.Column<int>(type: "int", nullable: false),
                    AngajatID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programare", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Programare_Angajat_AngajatID",
                        column: x => x.AngajatID,
                        principalTable: "Angajat",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Programare_Masina_MasinaID",
                        column: x => x.MasinaID,
                        principalTable: "Masina",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramareServiciu",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramareID = table.Column<int>(type: "int", nullable: false),
                    ServiciuID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramareServiciu", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProgramareServiciu_Programare_ProgramareID",
                        column: x => x.ProgramareID,
                        principalTable: "Programare",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramareServiciu_Serviciu_ServiciuID",
                        column: x => x.ServiciuID,
                        principalTable: "Serviciu",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Masina_ClientID",
                table: "Masina",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Programare_AngajatID",
                table: "Programare",
                column: "AngajatID");

            migrationBuilder.CreateIndex(
                name: "IX_Programare_MasinaID",
                table: "Programare",
                column: "MasinaID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramareServiciu_ProgramareID",
                table: "ProgramareServiciu",
                column: "ProgramareID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramareServiciu_ServiciuID",
                table: "ProgramareServiciu",
                column: "ServiciuID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramareServiciu");

            migrationBuilder.DropTable(
                name: "Programare");

            migrationBuilder.DropTable(
                name: "Serviciu");

            migrationBuilder.DropTable(
                name: "Angajat");

            migrationBuilder.DropTable(
                name: "Masina");

            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
