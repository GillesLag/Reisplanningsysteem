using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reisplanningssysteem_DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "RPS");

            migrationBuilder.CreateTable(
                name: "Bestemmingen",
                schema: "RPS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestemmingen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cursussen",
                schema: "RPS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursussen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gemeenten",
                schema: "RPS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gemeenten", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeeftijdsCategorieën",
                schema: "RPS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LeeftijdMinimum = table.Column<int>(type: "int", nullable: false),
                    LeeftijdMaximum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeeftijdsCategorieën", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Themas",
                schema: "RPS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gebruikers",
                schema: "RPS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Voornaam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Achternaam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TelefoonNr = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Straat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Huisnummer = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BasisCursus = table.Column<bool>(type: "bit", nullable: false),
                    HoofmonitorCursus = table.Column<bool>(type: "bit", nullable: false),
                    IsLid = table.Column<bool>(type: "bit", nullable: false),
                    GemeenteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gebruikers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gebruikers_Gemeenten_GemeenteId",
                        column: x => x.GemeenteId,
                        principalSchema: "RPS",
                        principalTable: "Gemeenten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GebruikersCursusen",
                schema: "RPS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GebruikerId = table.Column<int>(type: "int", nullable: false),
                    CursusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GebruikersCursusen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GebruikersCursusen_Cursussen_CursusId",
                        column: x => x.CursusId,
                        principalSchema: "RPS",
                        principalTable: "Cursussen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GebruikersCursusen_Gebruikers_GebruikerId",
                        column: x => x.GebruikerId,
                        principalSchema: "RPS",
                        principalTable: "Gebruikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reizen",
                schema: "RPS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BeginDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EindDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Prijs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThemaId = table.Column<int>(type: "int", nullable: true),
                    BestemmingsId = table.Column<int>(type: "int", nullable: false),
                    HoofdmonitorId = table.Column<int>(type: "int", nullable: false),
                    LeeftijdsCategorieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reizen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reizen_Bestemmingen_BestemmingsId",
                        column: x => x.BestemmingsId,
                        principalSchema: "RPS",
                        principalTable: "Bestemmingen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reizen_Gebruikers_HoofdmonitorId",
                        column: x => x.HoofdmonitorId,
                        principalSchema: "RPS",
                        principalTable: "Gebruikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reizen_LeeftijdsCategorieën_LeeftijdsCategorieId",
                        column: x => x.LeeftijdsCategorieId,
                        principalSchema: "RPS",
                        principalTable: "LeeftijdsCategorieën",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reizen_Themas_ThemaId",
                        column: x => x.ThemaId,
                        principalSchema: "RPS",
                        principalTable: "Themas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Boekingen",
                schema: "RPS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsMonitor = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InschrijvingsDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GebruikerId = table.Column<int>(type: "int", nullable: true),
                    ReisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boekingen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boekingen_Gebruikers_GebruikerId",
                        column: x => x.GebruikerId,
                        principalSchema: "RPS",
                        principalTable: "Gebruikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Boekingen_Reizen_ReisId",
                        column: x => x.ReisId,
                        principalSchema: "RPS",
                        principalTable: "Reizen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Onkosten",
                schema: "RPS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bedrag = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Omschrijving = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ReisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onkosten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Onkosten_Reizen_ReisId",
                        column: x => x.ReisId,
                        principalSchema: "RPS",
                        principalTable: "Reizen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boekingen_GebruikerId",
                schema: "RPS",
                table: "Boekingen",
                column: "GebruikerId");

            migrationBuilder.CreateIndex(
                name: "IX_Boekingen_ReisId",
                schema: "RPS",
                table: "Boekingen",
                column: "ReisId");

            migrationBuilder.CreateIndex(
                name: "IX_Gebruikers_GemeenteId",
                schema: "RPS",
                table: "Gebruikers",
                column: "GemeenteId");

            migrationBuilder.CreateIndex(
                name: "IX_GebruikersCursusen_CursusId",
                schema: "RPS",
                table: "GebruikersCursusen",
                column: "CursusId");

            migrationBuilder.CreateIndex(
                name: "IX_GebruikersCursusen_GebruikerId",
                schema: "RPS",
                table: "GebruikersCursusen",
                column: "GebruikerId");

            migrationBuilder.CreateIndex(
                name: "IX_Onkosten_ReisId",
                schema: "RPS",
                table: "Onkosten",
                column: "ReisId");

            migrationBuilder.CreateIndex(
                name: "IX_Reizen_BestemmingsId",
                schema: "RPS",
                table: "Reizen",
                column: "BestemmingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Reizen_HoofdmonitorId",
                schema: "RPS",
                table: "Reizen",
                column: "HoofdmonitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reizen_LeeftijdsCategorieId",
                schema: "RPS",
                table: "Reizen",
                column: "LeeftijdsCategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Reizen_ThemaId",
                schema: "RPS",
                table: "Reizen",
                column: "ThemaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boekingen",
                schema: "RPS");

            migrationBuilder.DropTable(
                name: "GebruikersCursusen",
                schema: "RPS");

            migrationBuilder.DropTable(
                name: "Onkosten",
                schema: "RPS");

            migrationBuilder.DropTable(
                name: "Cursussen",
                schema: "RPS");

            migrationBuilder.DropTable(
                name: "Reizen",
                schema: "RPS");

            migrationBuilder.DropTable(
                name: "Bestemmingen",
                schema: "RPS");

            migrationBuilder.DropTable(
                name: "Gebruikers",
                schema: "RPS");

            migrationBuilder.DropTable(
                name: "LeeftijdsCategorieën",
                schema: "RPS");

            migrationBuilder.DropTable(
                name: "Themas",
                schema: "RPS");

            migrationBuilder.DropTable(
                name: "Gemeenten",
                schema: "RPS");
        }
    }
}
