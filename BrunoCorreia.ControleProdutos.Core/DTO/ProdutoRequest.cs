using BrunoCorreia.ControleProdutos.Core.Entidades;
using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Categoria;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunoCorreia.ControleProdutos.Core.DTO
{
    public class ProdutoRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int MarcaId { get; set; }
        public int CategoriaId { get; set; }
    }
}
