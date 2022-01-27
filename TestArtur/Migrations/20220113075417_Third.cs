using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestArtur.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Novosts_Tegs_TegId",
                table: "Novosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Tegs_Categorys_CategoryId",
                table: "Tegs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tegs",
                table: "Tegs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Novosts",
                table: "Novosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorys",
                table: "Categorys");

            migrationBuilder.RenameTable(
                name: "Tegs",
                newName: "Teg");

            migrationBuilder.RenameTable(
                name: "Novosts",
                newName: "Novost");

            migrationBuilder.RenameTable(
                name: "Categorys",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_Tegs_CategoryId",
                table: "Teg",
                newName: "IX_Teg_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Novosts_TegId",
                table: "Novost",
                newName: "IX_Novost_TegId");

            migrationBuilder.AlterColumn<string>(
                name: "Nazvanie",
                table: "Teg",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Teg",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Teg",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Zagolovok",
                table: "Novost",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Vidimost",
                table: "Novost",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "TegId",
                table: "Novost",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Kartinka",
                table: "Novost",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Datadobavleniya",
                table: "Novost",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Novost",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Naimenovanie",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Category",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teg",
                table: "Teg",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Novost",
                table: "Novost",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Novost_Teg_TegId",
                table: "Novost",
                column: "TegId",
                principalTable: "Teg",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teg_Category_CategoryId",
                table: "Teg",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Novost_Teg_TegId",
                table: "Novost");

            migrationBuilder.DropForeignKey(
                name: "FK_Teg_Category_CategoryId",
                table: "Teg");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teg",
                table: "Teg");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Novost",
                table: "Novost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Teg",
                newName: "Tegs");

            migrationBuilder.RenameTable(
                name: "Novost",
                newName: "Novosts");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categorys");

            migrationBuilder.RenameIndex(
                name: "IX_Teg_CategoryId",
                table: "Tegs",
                newName: "IX_Tegs_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Novost_TegId",
                table: "Novosts",
                newName: "IX_Novosts_TegId");

            migrationBuilder.AlterColumn<string>(
                name: "Nazvanie",
                table: "Tegs",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Tegs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Tegs",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Zagolovok",
                table: "Novosts",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Vidimost",
                table: "Novosts",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "TegId",
                table: "Novosts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Kartinka",
                table: "Novosts",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Datadobavleniya",
                table: "Novosts",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Novosts",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Naimenovanie",
                table: "Categorys",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Categorys",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tegs",
                table: "Tegs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Novosts",
                table: "Novosts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorys",
                table: "Categorys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Novosts_Tegs_TegId",
                table: "Novosts",
                column: "TegId",
                principalTable: "Tegs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tegs_Categorys_CategoryId",
                table: "Tegs",
                column: "CategoryId",
                principalTable: "Categorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
