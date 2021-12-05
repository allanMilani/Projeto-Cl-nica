using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_Cliínica.Migrations
{
    public partial class add_status_login : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "TBLogins",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "TBLogins");
        }
    }
}
