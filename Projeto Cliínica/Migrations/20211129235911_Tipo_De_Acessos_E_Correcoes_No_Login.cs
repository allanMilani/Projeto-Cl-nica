using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_Cliínica.Migrations
{
    public partial class Tipo_De_Acessos_E_Correcoes_No_Login : Migration
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

            migrationBuilder.DropColumn(
                name: "Capacidade",
                table: "TBLogins");

            migrationBuilder.DropColumn(
                name: "MedicoID",
                table: "TBLogins");

            migrationBuilder.DropColumn(
                name: "PacienteID",
                table: "TBLogins");

            migrationBuilder.RenameColumn(
                name: "SecretariaID",
                table: "TBLogins",
                newName: "UsuarioID");

            migrationBuilder.RenameIndex(
                name: "IX_TBLogins_SecretariaID",
                table: "TBLogins",
                newName: "IX_TBLogins_UsuarioID");

            migrationBuilder.AddColumn<int>(
                name: "TipoAcessoID",
                table: "TBUsuarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TBTipoAcessos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Grupo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBTipoAcessos", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBUsuarios_TipoAcessoID",
                table: "TBUsuarios",
                column: "TipoAcessoID");

            migrationBuilder.AddForeignKey(
                name: "FK_TBLogins_TBUsuarios_UsuarioID",
                table: "TBLogins",
                column: "UsuarioID",
                principalTable: "TBUsuarios",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TBUsuarios_TBTipoAcessos_TipoAcessoID",
                table: "TBUsuarios",
                column: "TipoAcessoID",
                principalTable: "TBTipoAcessos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBLogins_TBUsuarios_UsuarioID",
                table: "TBLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_TBUsuarios_TBTipoAcessos_TipoAcessoID",
                table: "TBUsuarios");

            migrationBuilder.DropTable(
                name: "TBTipoAcessos");

            migrationBuilder.DropIndex(
                name: "IX_TBUsuarios_TipoAcessoID",
                table: "TBUsuarios");

            migrationBuilder.DropColumn(
                name: "TipoAcessoID",
                table: "TBUsuarios");

            migrationBuilder.RenameColumn(
                name: "UsuarioID",
                table: "TBLogins",
                newName: "SecretariaID");

            migrationBuilder.RenameIndex(
                name: "IX_TBLogins_UsuarioID",
                table: "TBLogins",
                newName: "IX_TBLogins_SecretariaID");

            migrationBuilder.AddColumn<string>(
                name: "Capacidade",
                table: "TBLogins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_TBLogins_MedicoID",
                table: "TBLogins",
                column: "MedicoID");

            migrationBuilder.CreateIndex(
                name: "IX_TBLogins_PacienteID",
                table: "TBLogins",
                column: "PacienteID");

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
