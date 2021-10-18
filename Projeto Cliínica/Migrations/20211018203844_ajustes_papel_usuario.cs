using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_Cliínica.Migrations
{
    public partial class ajustes_papel_usuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBLogins_TBMedicos_MedicoID",
                table: "TBLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_TBLogins_TBPaciente_PacienteID",
                table: "TBLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_TBLogins_TBSecretaria_SecretariaID",
                table: "TBLogins");

            migrationBuilder.DropIndex(
                name: "IX_TBLogins_MedicoID",
                table: "TBLogins");

            migrationBuilder.DropIndex(
                name: "IX_TBLogins_PacienteID",
                table: "TBLogins");

            migrationBuilder.DropIndex(
                name: "IX_TBLogins_SecretariaID",
                table: "TBLogins");

            migrationBuilder.DropColumn(
                name: "Capacidade",
                table: "TBLogins");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "TBLogins");

            migrationBuilder.DropColumn(
                name: "MedicoID",
                table: "TBLogins");

            migrationBuilder.DropColumn(
                name: "PacienteID",
                table: "TBLogins");

            migrationBuilder.DropColumn(
                name: "SecretariaID",
                table: "TBLogins");

            migrationBuilder.AddColumn<int>(
                name: "Papel",
                table: "TBLogins",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Papel",
                table: "TBLogins");

            migrationBuilder.AddColumn<string>(
                name: "Capacidade",
                table: "TBLogins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "TBLogins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MedicoID",
                table: "TBLogins",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PacienteID",
                table: "TBLogins",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecretariaID",
                table: "TBLogins",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBLogins_MedicoID",
                table: "TBLogins",
                column: "MedicoID");

            migrationBuilder.CreateIndex(
                name: "IX_TBLogins_PacienteID",
                table: "TBLogins",
                column: "PacienteID");

            migrationBuilder.CreateIndex(
                name: "IX_TBLogins_SecretariaID",
                table: "TBLogins",
                column: "SecretariaID");

            migrationBuilder.AddForeignKey(
                name: "FK_TBLogins_TBMedicos_MedicoID",
                table: "TBLogins",
                column: "MedicoID",
                principalTable: "TBMedicos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TBLogins_TBPaciente_PacienteID",
                table: "TBLogins",
                column: "PacienteID",
                principalTable: "TBPaciente",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TBLogins_TBSecretaria_SecretariaID",
                table: "TBLogins",
                column: "SecretariaID",
                principalTable: "TBSecretaria",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
