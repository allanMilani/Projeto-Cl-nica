using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_Cliínica.Migrations
{
    public partial class add_loginid_usuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoginID",
                table: "TBUsuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TBUsuarios_LoginID",
                table: "TBUsuarios",
                column: "LoginID");

            migrationBuilder.AddForeignKey(
                name: "FK_TBUsuarios_TBLogins_LoginID",
                table: "TBUsuarios",
                column: "LoginID",
                principalTable: "TBLogins",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBUsuarios_TBLogins_LoginID",
                table: "TBUsuarios");

            migrationBuilder.DropIndex(
                name: "IX_TBUsuarios_LoginID",
                table: "TBUsuarios");

            migrationBuilder.DropColumn(
                name: "LoginID",
                table: "TBUsuarios");
        }
    }
}
