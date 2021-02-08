using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Operacao
{
    public class Operacao : IAggregateRoot
    {
        private readonly List<ItemOperacao> _itensOperacao;
        private readonly ITipoOperacao _tipoOperacao;

        public Operacao()
        { }

        public Operacao(ITipoOperacao tipoOperacao)
        {
            _itensOperacao = new List<ItemOperacao>();
            _tipoOperacao = tipoOperacao;
            this.DataOcorrencia = DateTime.Now;
            this.Tipo = tipoOperacao.NomeOperacao;
        }

        public int Id { get; set; }
        public string Tipo { get; private set; }
        public DateTime DataOcorrencia { get; private set; }

        public EnumStatusOperacao StatusOperacao { get; set; }

        public IReadOnlyCollection<ItemOperacao> ItensOperacao => _itensOperacao;

        public void AdicionarItemOperacao(ItemOperacao itemOperacao)
        {
            _itensOperacao.Add(itemOperacao);
        }
        public async Task ProcessarOperacao()
        {
            foreach(var item in _itensOperacao)
            {
                await _tipoOperacao.EfetuarOperacao(item);
            }
        }
    }
}
