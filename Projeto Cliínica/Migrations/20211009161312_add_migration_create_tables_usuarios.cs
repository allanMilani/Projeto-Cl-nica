using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_Cliínica.Migrations
{
    public partial class add_migration_create_tables_usuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBUsuarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBUsuarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TBMedicos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CRM = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    UsuarioID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBMedicos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TBMedicos_TBUsuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "TBUsuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBPaciente",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Endereco = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Profissao = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBPaciente", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TBPaciente_TBUsuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "TBUsuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBSecretaria",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSecretaria", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TBSecretaria_TBUsuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "TBUsuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBMedicos_UsuarioID",
                table: "TBMedicos",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_TBPaciente_UsuarioID",
                table: "TBPaciente",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_TBSecretaria_UsuarioID",
                table: "TBSecretaria",
                column: "UsuarioID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBMedicos");

            migrationBuilder.DropTable(
                name: "TBPaciente");

            migrationBuilder.DropTable(
                name: "TBSecretaria");

            migrationBuilder.DropTable(
                name: "TBUsuarios");
        }
    }
}
