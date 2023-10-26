using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Sistema_de_registro_y_validacion_de_personas.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    apellido = table.Column<string>(type: "text", nullable: false),
                    cuil = table.Column<int>(type: "integer", nullable: false),
                    dni = table.Column<int>(type: "integer", nullable: false),
                    genero = table.Column<string>(type: "text", nullable: false),
                    telefono = table.Column<int>(type: "integer", nullable: false),
                    habilitado = table.Column<bool>(type: "boolean", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false),
                    estadoCrediticio = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SistemasExternos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SistemasExternos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    clave = table.Column<string>(type: "text", nullable: false),
                    rol = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "SistemasExternos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
