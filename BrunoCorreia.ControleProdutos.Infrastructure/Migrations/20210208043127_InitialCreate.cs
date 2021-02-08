using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BrunoCorreia.ControleProdutos.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "Categoriaseq",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Estoqueseq",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "ItemOperacaoseq",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Marcaseq",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Operacaoseq",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Produtoseq",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "ReajustePrecoCategoriaseq",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataOcorrencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusOperacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReajustePrecoCategoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TipoReajuste = table.Column<int>(type: "int", nullable: false),
                    Porcentagem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InicioVigencia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinalVigencia = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReajustePrecoCategoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReajusteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categoria_ReajustePrecoCategoria_ReajusteId",
                        column: x => x.ReajusteId,
                        principalTable: "ReajustePrecoCategoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MarcaId = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produto_Marca_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estoque",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estoque_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemOperacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    OperacaoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOperacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemOperacao_Operacao_OperacaoId",
                        column: x => x.OperacaoId,
                        principalTable: "Operacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemOperacao_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_ReajusteId",
                table: "Categoria",
                column: "ReajusteId");

            migrationBuilder.CreateIndex(
                name: "IX_Estoque_ProdutoId",
                table: "Estoque",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOperacao_OperacaoId",
                table: "ItemOperacao",
                column: "OperacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOperacao_ProdutoId",
                table: "ItemOperacao",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CategoriaId",
                table: "Produto",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_MarcaId",
                table: "Produto",
                column: "MarcaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estoque");

            migrationBuilder.DropTable(
                name: "ItemOperacao");

            migrationBuilder.DropTable(
                name: "Operacao");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Marca");

            migrationBuilder.DropTable(
                name: "ReajustePrecoCategoria");

            migrationBuilder.DropSequence(
                name: "Categoriaseq");

            migrationBuilder.DropSequence(
                name: "Estoqueseq");

            migrationBuilder.DropSequence(
                name: "ItemOperacaoseq");

            migrationBuilder.DropSequence(
                name: "Marcaseq");

            migrationBuilder.DropSequence(
                name: "Operacaoseq");

            migrationBuilder.DropSequence(
                name: "Produtoseq");

            migrationBuilder.DropSequence(
                name: "ReajustePrecoCategoriaseq");
        }
    }
}
