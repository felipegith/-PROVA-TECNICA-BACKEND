using Microsoft.EntityFrameworkCore.Migrations;

namespace LOCACOES.API.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacaos_Clientes_ClienteId",
                table: "Locacaos");

            migrationBuilder.DropForeignKey(
                name: "FK_Locacaos_Filmes_FilmeId",
                table: "Locacaos");

            migrationBuilder.DropIndex(
                name: "IX_Locacaos_ClienteId",
                table: "Locacaos");

            migrationBuilder.DropIndex(
                name: "IX_Locacaos_FilmeId",
                table: "Locacaos");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Locacaos");

            migrationBuilder.DropColumn(
                name: "FilmeId",
                table: "Locacaos");

            migrationBuilder.CreateIndex(
                name: "IX_Locacaos_Id_Cliente",
                table: "Locacaos",
                column: "Id_Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Locacaos_Id_Filme",
                table: "Locacaos",
                column: "Id_Filme");

            migrationBuilder.AddForeignKey(
                name: "FK_Locacaos_Clientes_Id_Cliente",
                table: "Locacaos",
                column: "Id_Cliente",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locacaos_Filmes_Id_Filme",
                table: "Locacaos",
                column: "Id_Filme",
                principalTable: "Filmes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacaos_Clientes_Id_Cliente",
                table: "Locacaos");

            migrationBuilder.DropForeignKey(
                name: "FK_Locacaos_Filmes_Id_Filme",
                table: "Locacaos");

            migrationBuilder.DropIndex(
                name: "IX_Locacaos_Id_Cliente",
                table: "Locacaos");

            migrationBuilder.DropIndex(
                name: "IX_Locacaos_Id_Filme",
                table: "Locacaos");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Locacaos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FilmeId",
                table: "Locacaos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locacaos_ClienteId",
                table: "Locacaos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Locacaos_FilmeId",
                table: "Locacaos",
                column: "FilmeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locacaos_Clientes_ClienteId",
                table: "Locacaos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locacaos_Filmes_FilmeId",
                table: "Locacaos",
                column: "FilmeId",
                principalTable: "Filmes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
