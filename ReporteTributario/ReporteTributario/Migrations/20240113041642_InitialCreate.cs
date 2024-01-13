using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReporteTributario.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "INFORMACION_BASE",
                columns: table => new
                {
                    IdImpuesto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Impuesto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Departamento = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FechaLimite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Responsable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Periodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Periodicidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vigente = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INFORMACION_BASE", x => x.IdImpuesto);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Restablecer = table.Column<bool>(type: "bit", nullable: false),
                    Confirmado = table.Column<bool>(type: "bit", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.IdUsuario);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "INFORMACION_BASE");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
