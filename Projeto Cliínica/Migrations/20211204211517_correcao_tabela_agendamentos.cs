using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_Cliínica.Migrations
{
    public partial class correcao_tabela_agendamentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBAgendamentos_TBSecretaria_QuemCancelouID",
                table: "TBAgendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_TBAgendamentos_TBSecretaria_SecretariaID",
                table: "TBAgendamentos");

            migrationBuilder.RenameColumn(
                name: "SecretariaID",
                table: "TBAgendamentos",
                newName: "LoginCriadorID");

            migrationBuilder.RenameIndex(
                name: "IX_TBAgendamentos_SecretariaID",
                table: "TBAgendamentos",
                newName: "IX_TBAgendamentos_LoginCriadorID");

            migrationBuilder.AddForeignKey(
                name: "FK_TBAgendamentos_TBLogins_LoginCriadorID",
                table: "TBAgendamentos",
                column: "LoginCriadorID",
                principalTable: "TBLogins",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TBAgendamentos_TBLogins_QuemCancelouID",
                table: "TBAgendamentos",
                column: "QuemCancelouID",
                principalTable: "TBLogins",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBAgendamentos_TBLogins_LoginCriadorID",
                table: "TBAgendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_TBAgendamentos_TBLogins_QuemCancelouID",
                table: "TBAgendamentos");

            migrationBuilder.RenameColumn(
                name: "LoginCriadorID",
                table: "TBAgendamentos",
                newName: "SecretariaID");

            migrationBuilder.RenameIndex(
                name: "IX_TBAgendamentos_LoginCriadorID",
                table: "TBAgendamentos",
                newName: "IX_TBAgendamentos_SecretariaID");

            migrationBuilder.AddForeignKey(
                name: "FK_TBAgendamentos_TBSecretaria_QuemCancelouID",
                table: "TBAgendamentos",
                column: "QuemCancelouID",
                principalTable: "TBSecretaria",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TBAgendamentos_TBSecretaria_SecretariaID",
                table: "TBAgendamentos",
                column: "SecretariaID",
                principalTable: "TBSecretaria",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
