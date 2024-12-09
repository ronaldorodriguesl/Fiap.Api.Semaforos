using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Api.Semaforos.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_semaforos",
                columns: table => new
                {
                    SemaforoId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Luz = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Logradouro = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "SYSTIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_semaforos", x => x.SemaforoId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Role = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_condicoes_climaticas",
                columns: table => new
                {
                    CondicaoClimaticaId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Tempo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Temperatura = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "SYSTIMESTAMP"),
                    SemaforoId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_condicoes_climaticas", x => x.CondicaoClimaticaId);
                    table.ForeignKey(
                        name: "FK_tbl_condicoes_climaticas_tbl_semaforos_SemaforoId",
                        column: x => x.SemaforoId,
                        principalTable: "tbl_semaforos",
                        principalColumn: "SemaforoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_fluxo_veiculos",
                columns: table => new
                {
                    FluxoVeiculoId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Quantidade = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "SYSTIMESTAMP"),
                    SemaforoId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_fluxo_veiculos", x => x.FluxoVeiculoId);
                    table.ForeignKey(
                        name: "FK_tbl_fluxo_veiculos_tbl_semaforos_SemaforoId",
                        column: x => x.SemaforoId,
                        principalTable: "tbl_semaforos",
                        principalColumn: "SemaforoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_condicoes_climaticas_SemaforoId",
                table: "tbl_condicoes_climaticas",
                column: "SemaforoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_fluxo_veiculos_SemaforoId",
                table: "tbl_fluxo_veiculos",
                column: "SemaforoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_condicoes_climaticas");

            migrationBuilder.DropTable(
                name: "tbl_fluxo_veiculos");

            migrationBuilder.DropTable(
                name: "tbl_usuarios");

            migrationBuilder.DropTable(
                name: "tbl_semaforos");
        }
    }
}
