using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestArtur.Migrations
{
    public partial class AddTableBlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Zagolovok = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opisanie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kartinka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datadobavleniya = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TegId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blog_Teg_TegId",
                        column: x => x.TegId,
                        principalTable: "Teg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Zagolovok = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opisanie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kartinka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datadobavleniya = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Teg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TegId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogViewModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_TegId",
                table: "Blog",
                column: "TegId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "BlogViewModel");
        }
    }
}
