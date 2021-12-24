using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoFinal.Migrations
{
    public partial class TabelasIniciais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(60)", nullable: false),
                    Sobrenome = table.Column<string>(type: "VARCHAR(60)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(60)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoAutor = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(type: "VARCHAR(60)", nullable: false),
                    ISBN = table.Column<int>(nullable: false),
                    AnoLancamento = table.Column<int>(nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Livros_Autores_CodigoAutor",
                        column: x => x.CodigoAutor,
                        principalTable: "Autores",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoRole = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(60)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(60)", nullable: false),
                    Senha = table.Column<string>(type: "VARCHAR(60)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_CodigoRole",
                        column: x => x.CodigoRole,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livros_CodigoAutor",
                table: "Livros",
                column: "CodigoAutor");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_CodigoRole",
                table: "Usuarios",
                column: "CodigoRole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livros");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Autores");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
