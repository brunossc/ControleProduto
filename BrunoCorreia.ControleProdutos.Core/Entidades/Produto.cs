using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates;
using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Categoria;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BrunoCorreia.ControleProdutos.Core.Entidades
{
    public class Produto : IAggregateRoot
    {        
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public Marca Marca { get; set; }
        public int MarcaId { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }
    }
}
