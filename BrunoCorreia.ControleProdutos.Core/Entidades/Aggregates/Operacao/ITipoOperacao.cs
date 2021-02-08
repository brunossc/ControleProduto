using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Operacao
{
    public interface ITipoOperacao
    {
        public int TipoOperacao { get; }
        public string NomeOperacao { get; }

        Task EfetuarOperacao(ItemOperacao itemOperacao);
    }
}
