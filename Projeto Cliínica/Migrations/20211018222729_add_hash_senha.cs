using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_Cliínica.Migrations
{
    public partial class add_hash_senha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBUsuarios_TBLogins_LoginID",
                table: "TBUsuarios");

            migrationBuilder.AlterColumn<int>(
                name: "LoginID",
                table: "TBUsuarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "TBLogins",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddForeignKey(
                name: "FK_TBUsuarios_TBLogins_LoginID",
                table: "TBUsuarios",
                column: "LoginID",
                principalTable: "TBLogins",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBUsuarios_TBLogins_LoginID",
                table: "TBUsuarios");

            migrationBuilder.AlterColumn<int>(
                name: "LoginID",
                table: "TBUsuarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "TBLogins",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddForeignKey(
                name: "FK_TBUsuarios_TBLogins_LoginID",
                table: "TBUsuarios",
                column: "LoginID",
                principalTable: "TBLogins",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
