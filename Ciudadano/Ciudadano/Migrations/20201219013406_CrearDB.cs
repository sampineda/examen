using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ciudadano.Migrations
{
    public partial class CrearDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "EstadosCiviles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estado = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosCiviles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                schema: "dbo",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(nullable: false),
                    Contrasenia = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    EstaActivo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Ciudadanos",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(nullable: true),
                    Edad = table.Column<int>(nullable: false),
                    EstadoCivilId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudadanos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ciudadanos_EstadosCiviles_EstadoCivilId",
                        column: x => x.EstadoCivilId,
                        principalSchema: "dbo",
                        principalTable: "EstadosCiviles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ciudadanos_EstadoCivilId",
                schema: "dbo",
                table: "Ciudadanos",
                column: "EstadoCivilId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ciudadanos",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Usuarios",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EstadosCiviles",
                schema: "dbo");
        }
    }
}
