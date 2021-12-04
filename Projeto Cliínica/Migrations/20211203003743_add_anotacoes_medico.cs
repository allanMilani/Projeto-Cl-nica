using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_Cliínica.Migrations
{
    public partial class add_anotacoes_medico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnotacoesMedico",
                table: "TBAgendamentos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuemCancelouID",
                table: "TBAgendamentos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBAgendamentos_QuemCancelouID",
                table: "TBAgendamentos",
                column: "QuemCancelouID");

            migrationBuilder.AddForeignKey(
                name: "FK_TBAgendamentos_TBSecretaria_QuemCancelouID",
                table: "TBAgendamentos",
                column: "QuemCancelouID",
                principalTable: "TBSecretaria",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBAgendamentos_TBSecretaria_QuemCancelouID",
                table: "TBAgendamentos");

            migrationBuilder.DropIndex(
                name: "IX_TBAgendamentos_QuemCancelouID",
                table: "TBAgendamentos");

            migrationBuilder.DropColumn(
                name: "AnotacoesMedico",
                table: "TBAgendamentos");

            migrationBuilder.DropColumn(
                name: "QuemCancelouID",
                table: "TBAgendamentos");
        }
    }
}
