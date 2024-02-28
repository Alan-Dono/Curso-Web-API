using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiAutores.Migrations
{
    public partial class FechaPublicacionLibro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "Libros",
                newName: "titulo");

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaPublicacion",
                table: "Libros",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fechaPublicacion",
                table: "Libros");

            migrationBuilder.RenameColumn(
                name: "titulo",
                table: "Libros",
                newName: "Titulo");
        }
    }
}
