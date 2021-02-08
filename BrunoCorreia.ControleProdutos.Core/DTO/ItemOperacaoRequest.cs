using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Operacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunoCorreia.ControleProdutos.Core.DTO
{
    public class ItemOperacaoRequest
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
    }
}
