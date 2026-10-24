using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrenaEscala.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Decoders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    ManualPDF = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Comentarios = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decoders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Escalas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Proporcion = table.Column<string>(type: "TEXT", nullable: false),
                    AnchoViaModelo = table.Column<decimal>(type: "TEXT", nullable: false),
                    AnchoViaReal = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escalas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fabricantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", nullable: false),
                    Comentarios = table.Column<string>(type: "TEXT", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fabricantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    LogoOperador = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Comentarios = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tiendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", nullable: false),
                    Fax = table.Column<string>(type: "TEXT", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    SigueAbierta = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiendas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposModelo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Icono = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposModelo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modelos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Referencia = table.Column<string>(type: "TEXT", nullable: false),
                    Decoracion = table.Column<string>(type: "TEXT", nullable: false),
                    Epoca = table.Column<string>(type: "TEXT", nullable: false),
                    Numeral = table.Column<string>(type: "TEXT", nullable: false),
                    TipoUic = table.Column<string>(type: "TEXT", nullable: false),
                    DescripcionTrenReal = table.Column<string>(type: "TEXT", nullable: false),
                    PrecioCatalogo = table.Column<decimal>(type: "TEXT", nullable: false),
                    PrecioCompra = table.Column<decimal>(type: "TEXT", nullable: false),
                    Vendido = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaVenta = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PrecioVenta = table.Column<decimal>(type: "TEXT", nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Unidades = table.Column<int>(type: "INTEGER", nullable: false),
                    ModeloDeseado = table.Column<bool>(type: "INTEGER", nullable: false),
                    LongitudModelo = table.Column<int>(type: "INTEGER", nullable: false),
                    TieneNem = table.Column<bool>(type: "INTEGER", nullable: false),
                    TieneKkk = table.Column<bool>(type: "INTEGER", nullable: false),
                    TomaEnPantografo = table.Column<bool>(type: "INTEGER", nullable: false),
                    TieneSonido = table.Column<bool>(type: "INTEGER", nullable: false),
                    EsEdicionLimitada = table.Column<bool>(type: "INTEGER", nullable: false),
                    AnioEdicionLimitada = table.Column<string>(type: "TEXT", nullable: true),
                    DireccionDigital = table.Column<string>(type: "TEXT", nullable: true),
                    IluminacionFrontal = table.Column<string>(type: "TEXT", nullable: false),
                    IluminacionTrasera = table.Column<string>(type: "TEXT", nullable: false),
                    IluminacionInterior = table.Column<string>(type: "TEXT", nullable: false),
                    OperadorId = table.Column<int>(type: "INTEGER", nullable: true),
                    FabricanteId = table.Column<int>(type: "INTEGER", nullable: true),
                    EscalaId = table.Column<int>(type: "INTEGER", nullable: true),
                    TipoModeloId = table.Column<int>(type: "INTEGER", nullable: true),
                    TiendaId = table.Column<int>(type: "INTEGER", nullable: true),
                    DecoderId = table.Column<int>(type: "INTEGER", nullable: true),
                    Comentarios = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modelos_Decoders_DecoderId",
                        column: x => x.DecoderId,
                        principalTable: "Decoders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Modelos_Escalas_EscalaId",
                        column: x => x.EscalaId,
                        principalTable: "Escalas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Modelos_Fabricantes_FabricanteId",
                        column: x => x.FabricanteId,
                        principalTable: "Fabricantes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Modelos_Operadores_OperadorId",
                        column: x => x.OperadorId,
                        principalTable: "Operadores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Modelos_Tiendas_TiendaId",
                        column: x => x.TiendaId,
                        principalTable: "Tiendas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Modelos_TiposModelo_TipoModeloId",
                        column: x => x.TipoModeloId,
                        principalTable: "TiposModelo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ImagenesModelo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DatosImagen = table.Column<byte[]>(type: "BLOB", nullable: true),
                    NombreArchivo = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Principal = table.Column<bool>(type: "INTEGER", nullable: false),
                    ModeloId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagenesModelo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagenesModelo_Modelos_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Modelos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImagenesModelo_ModeloId",
                table: "ImagenesModelo",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_DecoderId",
                table: "Modelos",
                column: "DecoderId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_EscalaId",
                table: "Modelos",
                column: "EscalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_FabricanteId",
                table: "Modelos",
                column: "FabricanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_OperadorId",
                table: "Modelos",
                column: "OperadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_TiendaId",
                table: "Modelos",
                column: "TiendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_TipoModeloId",
                table: "Modelos",
                column: "TipoModeloId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagenesModelo");

            migrationBuilder.DropTable(
                name: "Modelos");

            migrationBuilder.DropTable(
                name: "Decoders");

            migrationBuilder.DropTable(
                name: "Escalas");

            migrationBuilder.DropTable(
                name: "Fabricantes");

            migrationBuilder.DropTable(
                name: "Operadores");

            migrationBuilder.DropTable(
                name: "Tiendas");

            migrationBuilder.DropTable(
                name: "TiposModelo");
        }
    }
}
