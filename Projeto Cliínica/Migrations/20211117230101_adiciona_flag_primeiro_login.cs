using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_Cliínica.Migrations
{
    public partial class adiciona_flag_primeiro_login : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PrimeiroLogin",
                table: "TBLogins",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrimeiroLogin",
                table: "TBLogins");
        }
    }
}
