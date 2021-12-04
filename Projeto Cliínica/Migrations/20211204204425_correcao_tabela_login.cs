using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_Cliínica.Migrations
{
    public partial class correcao_tabela_login : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBUsuarios_TBTipoAcessos_TipoAcessoID",
                table: "TBUsuarios");

            migrationBuilder.DropIndex(
                name: "IX_TBUsuarios_TipoAcessoID",
                table: "TBUsuarios");

            migrationBuilder.DropColumn(
                name: "TipoAcessoID",
                table: "TBUsuarios");

            migrationBuilder.AddColumn<int>(
                name: "TipoAcessoID",
                table: "TBLogins",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBLogins_TipoAcessoID",
                table: "TBLogins",
                column: "TipoAcessoID");

            migrationBuilder.AddForeignKey(
                name: "FK_TBLogins_TBTipoAcessos_TipoAcessoID",
                table: "TBLogins",
                column: "TipoAcessoID",
                principalTable: "TBTipoAcessos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBLogins_TBTipoAcessos_TipoAcessoID",
                table: "TBLogins");

            migrationBuilder.DropIndex(
                name: "IX_TBLogins_TipoAcessoID",
                table: "TBLogins");

            migrationBuilder.DropColumn(
                name: "TipoAcessoID",
                table: "TBLogins");

            migrationBuilder.AddColumn<int>(
                name: "TipoAcessoID",
                table: "TBUsuarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBUsuarios_TipoAcessoID",
                table: "TBUsuarios",
                column: "TipoAcessoID");

            migrationBuilder.AddForeignKey(
                name: "FK_TBUsuarios_TBTipoAcessos_TipoAcessoID",
                table: "TBUsuarios",
                column: "TipoAcessoID",
                principalTable: "TBTipoAcessos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
