using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestArtur.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Naimenovanie = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tegs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazvanie = table.Column<string>(type: "TEXT", nullable: true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tegs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tegs_Categorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Novosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Zagolovok = table.Column<string>(type: "TEXT", nullable: true),
                    Datadobavleniya = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Vidimost = table.Column<bool>(type: "INTEGER", nullable: false),
                    Kartinka = table.Column<string>(type: "TEXT", nullable: true),
                    TegId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Novosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Novosts_Tegs_TegId",
                        column: x => x.TegId,
                        principalTable: "Tegs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Novosts_TegId",
                table: "Novosts",
                column: "TegId");

            migrationBuilder.CreateIndex(
                name: "IX_Tegs_CategoryId",
                table: "Tegs",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Novosts");

            migrationBuilder.DropTable(
                name: "Tegs");

            migrationBuilder.DropTable(
                name: "Categorys");
        }
    }
}
