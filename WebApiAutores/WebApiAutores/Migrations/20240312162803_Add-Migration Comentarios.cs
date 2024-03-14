using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiAutores.Migrations
{
    public partial class AddMigrationComentarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "idUsuario",
                table: "Comentarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "usuarioId",
                table: "Comentarios",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_usuarioId",
                table: "Comentarios",
                column: "usuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_AspNetUsers_usuarioId",
                table: "Comentarios",
                column: "usuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_AspNetUsers_usuarioId",
                table: "Comentarios");

            migrationBuilder.DropIndex(
                name: "IX_Comentarios_usuarioId",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "idUsuario",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "usuarioId",
                table: "Comentarios");
        }
    }
}
