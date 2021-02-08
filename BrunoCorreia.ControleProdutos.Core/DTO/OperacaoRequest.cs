using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Operacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunoCorreia.ControleProdutos.Core.DTO
{
    public class OperacaoRequest
    {
        public ICollection<ItemOperacaoRequest> ItensOperacao { get; set; }
    }
}
