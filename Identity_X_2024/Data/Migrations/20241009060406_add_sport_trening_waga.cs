using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity_X_2024.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_sport_trening_waga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sport",
                columns: table => new
                {
                    SportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport", x => x.SportId);
                });

            migrationBuilder.CreateTable(
                name: "Waga",
                columns: table => new
                {
                    WagaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Wartosc = table.Column<float>(type: "real", nullable: false),
                    UzytkownikId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waga", x => x.WagaId);
                    table.ForeignKey(
                        name: "FK_Waga_Uzytkownik_UzytkownikId",
                        column: x => x.UzytkownikId,
                        principalTable: "Uzytkownik",
                        principalColumn: "UzytkownikId");
                });

            migrationBuilder.CreateTable(
                name: "Trening",
                columns: table => new
                {
                    TreningId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UzytkownikId = table.Column<int>(type: "int", nullable: true),
                    SportId = table.Column<int>(type: "int", nullable: true),
                    Dystans = table.Column<float>(type: "real", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Czas = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trening", x => x.TreningId);
                    table.ForeignKey(
                        name: "FK_Trening_Sport_SportId",
                        column: x => x.SportId,
                        principalTable: "Sport",
                        principalColumn: "SportId");
                    table.ForeignKey(
                        name: "FK_Trening_Uzytkownik_UzytkownikId",
                        column: x => x.UzytkownikId,
                        principalTable: "Uzytkownik",
                        principalColumn: "UzytkownikId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trening_SportId",
                table: "Trening",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_Trening_UzytkownikId",
                table: "Trening",
                column: "UzytkownikId");

            migrationBuilder.CreateIndex(
                name: "IX_Waga_UzytkownikId",
                table: "Waga",
                column: "UzytkownikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trening");

            migrationBuilder.DropTable(
                name: "Waga");

            migrationBuilder.DropTable(
                name: "Sport");
        }
    }
}
