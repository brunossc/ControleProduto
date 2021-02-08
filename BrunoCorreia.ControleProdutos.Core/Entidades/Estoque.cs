using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BrunoCorreia.ControleProdutos.Core.Entidades
{
    public class Estoque : IAggregateRoot
    {
        public int Id { get; set; }
        public Produto Produto { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
