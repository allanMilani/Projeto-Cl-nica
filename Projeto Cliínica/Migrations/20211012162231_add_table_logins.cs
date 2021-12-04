using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_Cliínica.Migrations
{
    public partial class add_table_logins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBLogins",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicoID = table.Column<int>(type: "int", nullable: true),
                    PacienteID = table.Column<int>(type: "int", nullable: true),
                    SecretariaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBLogins", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TBLogins_TBMedicos_MedicoID",
                        column: x => x.MedicoID,
                        principalTable: "TBMedicos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBLogins_TBPaciente_PacienteID",
                        column: x => x.PacienteID,
                        principalTable: "TBPaciente",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBLogins_TBSecretaria_SecretariaID",
                        column: x => x.SecretariaID,
                        principalTable: "TBSecretaria",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBLogins");
        }
    }
}
