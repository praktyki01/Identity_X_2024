using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity_X_2024.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uzytkownik",
                columns: table => new
                {
                    UzytkownikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wzrost = table.Column<int>(type: "int", nullable: false),
                    Plec = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UzytkownikUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzytkownik", x => x.UzytkownikId);
                    table.ForeignKey(
                        name: "FK_Uzytkownik_AspNetUsers_UzytkownikUserId",
                        column: x => x.UzytkownikUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Uzytkownik_UzytkownikUserId",
                table: "Uzytkownik",
                column: "UzytkownikUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uzytkownik");
        }
    }
}
