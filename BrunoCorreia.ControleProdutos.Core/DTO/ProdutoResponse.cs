using BrunoCorreia.ControleProdutos.Core.Entidades;
using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Categoria;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunoCorreia.ControleProdutos.Core.DTO
{
    public class ProdutoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal PrecoSemReajusteCategoria { get; set; }
        public MarcaResponse Marca { get; set; }
        public CategoriaResponse Categoria { get; set; }
    }
}
