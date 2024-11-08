using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SysCoco._0.Migrations
{
    /// <inheritdoc />
    public partial class SySCoco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    apellidos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tipoDocumento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    numeroDocumento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    contraseña = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    numeroTelefono = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rolesid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_rolesid",
                        column: x => x.rolesid,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Buzon",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    usuariosId = table.Column<int>(type: "int", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buzon", x => x.id);
                    table.ForeignKey(
                        name: "FK_Buzon_Usuarios_usuariosId",
                        column: x => x.usuariosId,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comunicado",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    remitente = table.Column<int>(type: "int", nullable: false),
                    destinatario = table.Column<int>(type: "int", nullable: false),
                    Asunto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArchivoAdjunto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comunicado", x => x.id);
                    table.ForeignKey(
                        name: "FK_Comunicado_Usuarios_destinatario",
                        column: x => x.destinatario,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comunicado_Usuarios_remitente",
                        column: x => x.remitente,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Encuesta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    tipoPregunta = table.Column<int>(type: "int", nullable: false),
                    pregunta = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Respuesta = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    usuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encuesta", x => x.id);
                    table.ForeignKey(
                        name: "FK_Encuesta_Usuarios_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Materia",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    usuariosid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materia", x => x.id);
                    table.ForeignKey(
                        name: "FK_Materia_Usuarios_usuariosid",
                        column: x => x.usuariosid,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mensaje",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    remitente = table.Column<int>(type: "int", nullable: false),
                    destinatario = table.Column<int>(type: "int", nullable: false),
                    asunto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    contenido = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    archivoAdjunto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensaje", x => x.id);
                    table.ForeignKey(
                        name: "FK_Mensaje_Usuarios_destinatario",
                        column: x => x.destinatario,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mensaje_Usuarios_remitente",
                        column: x => x.remitente,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Votacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuarioId = table.Column<int>(type: "int", nullable: false),
                    encuestaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_Votacion_Encuesta_encuestaId",
                        column: x => x.encuestaId,
                        principalTable: "Encuesta",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votacion_Usuarios_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    materiaid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Cursos_Materia_materiaid",
                        column: x => x.materiaid,
                        principalTable: "Materia",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Actividad",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    creador = table.Column<int>(type: "int", nullable: false),
                    cursoid = table.Column<int>(type: "int", nullable: false),
                    asunto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    contenido = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaenvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    documentoAdjunto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividad", x => x.id);
                    table.ForeignKey(
                        name: "FK_Actividad_Cursos_cursoid",
                        column: x => x.cursoid,
                        principalTable: "Cursos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Actividad_Usuarios_creador",
                        column: x => x.creador,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Asistencia",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursoId = table.Column<int>(type: "int", nullable: false),
                    usuariosId = table.Column<int>(type: "int", nullable: false),
                    fechaAsistencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencia", x => x.id);
                    table.ForeignKey(
                        name: "FK_Asistencia_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asistencia_Usuarios_usuariosId",
                        column: x => x.usuariosId,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actividad_creador",
                table: "Actividad",
                column: "creador");

            migrationBuilder.CreateIndex(
                name: "IX_Actividad_cursoid",
                table: "Actividad",
                column: "cursoid");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_CursoId",
                table: "Asistencia",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_usuariosId",
                table: "Asistencia",
                column: "usuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_Buzon_usuariosId",
                table: "Buzon",
                column: "usuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_Comunicado_destinatario",
                table: "Comunicado",
                column: "destinatario");

            migrationBuilder.CreateIndex(
                name: "IX_Comunicado_remitente",
                table: "Comunicado",
                column: "remitente");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_materiaid",
                table: "Cursos",
                column: "materiaid");

            migrationBuilder.CreateIndex(
                name: "IX_Encuesta_usuarioId",
                table: "Encuesta",
                column: "usuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Materia_usuariosid",
                table: "Materia",
                column: "usuariosid");

            migrationBuilder.CreateIndex(
                name: "IX_Mensaje_destinatario",
                table: "Mensaje",
                column: "destinatario");

            migrationBuilder.CreateIndex(
                name: "IX_Mensaje_remitente",
                table: "Mensaje",
                column: "remitente");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_rolesid",
                table: "Usuarios",
                column: "rolesid");

            migrationBuilder.CreateIndex(
                name: "IX_Votacion_encuestaId",
                table: "Votacion",
                column: "encuestaId");

            migrationBuilder.CreateIndex(
                name: "IX_Votacion_usuarioId",
                table: "Votacion",
                column: "usuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actividad");

            migrationBuilder.DropTable(
                name: "Asistencia");

            migrationBuilder.DropTable(
                name: "Buzon");

            migrationBuilder.DropTable(
                name: "Comunicado");

            migrationBuilder.DropTable(
                name: "Mensaje");

            migrationBuilder.DropTable(
                name: "Votacion");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Encuesta");

            migrationBuilder.DropTable(
                name: "Materia");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
