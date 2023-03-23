using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reisplanningssysteem_DAL.Migrations
{
    /// <inheritdoc />
    public partial class Mgr2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MedischeGegevens",
                schema: "RPS",
                table: "Gebruikers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GemeenteId",
                schema: "RPS",
                table: "Bestemmingen",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Huisnummer",
                schema: "RPS",
                table: "Bestemmingen",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Land",
                schema: "RPS",
                table: "Bestemmingen",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Straat",
                schema: "RPS",
                table: "Bestemmingen",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Bestemmingen_GemeenteId",
                schema: "RPS",
                table: "Bestemmingen",
                column: "GemeenteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bestemmingen_Gemeenten_GemeenteId",
                schema: "RPS",
                table: "Bestemmingen",
                column: "GemeenteId",
                principalSchema: "RPS",
                principalTable: "Gemeenten",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestemmingen_Gemeenten_GemeenteId",
                schema: "RPS",
                table: "Bestemmingen");

            migrationBuilder.DropIndex(
                name: "IX_Bestemmingen_GemeenteId",
                schema: "RPS",
                table: "Bestemmingen");

            migrationBuilder.DropColumn(
                name: "MedischeGegevens",
                schema: "RPS",
                table: "Gebruikers");

            migrationBuilder.DropColumn(
                name: "GemeenteId",
                schema: "RPS",
                table: "Bestemmingen");

            migrationBuilder.DropColumn(
                name: "Huisnummer",
                schema: "RPS",
                table: "Bestemmingen");

            migrationBuilder.DropColumn(
                name: "Land",
                schema: "RPS",
                table: "Bestemmingen");

            migrationBuilder.DropColumn(
                name: "Straat",
                schema: "RPS",
                table: "Bestemmingen");
        }
    }
}
