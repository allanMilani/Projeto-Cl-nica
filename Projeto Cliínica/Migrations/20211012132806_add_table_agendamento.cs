using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_Cliínica.Migrations
{
    public partial class add_table_agendamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBAgendamentos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteID = table.Column<int>(type: "int", nullable: true),
                    MedicoID = table.Column<int>(type: "int", nullable: true),
                    DataHoraConsulta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    SecretariaID = table.Column<int>(type: "int", nullable: true),
                    MotivoCancelamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCancelamento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBAgendamentos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TBAgendamentos_TBMedicos_MedicoID",
                        column: x => x.MedicoID,
                        principalTable: "TBMedicos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBAgendamentos_TBPaciente_PacienteID",
                        column: x => x.PacienteID,
                        principalTable: "TBPaciente",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBAgendamentos_TBSecretaria_SecretariaID",
                        column: x => x.SecretariaID,
                        principalTable: "TBSecretaria",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBAgendamentos_MedicoID",
                table: "TBAgendamentos",
                column: "MedicoID");

            migrationBuilder.CreateIndex(
                name: "IX_TBAgendamentos_PacienteID",
                table: "TBAgendamentos",
                column: "PacienteID");

            migrationBuilder.CreateIndex(
                name: "IX_TBAgendamentos_SecretariaID",
                table: "TBAgendamentos",
                column: "SecretariaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBAgendamentos");
        }
    }
}
