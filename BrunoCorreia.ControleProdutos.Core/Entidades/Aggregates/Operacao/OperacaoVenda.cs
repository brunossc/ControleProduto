using BrunoCorreia.ControleProdutos.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Operacao
{
    public class OperacaoVenda : ITipoOperacao
    {
        IEstoqueRepository _estoqueRepo;
        public OperacaoVenda(IEstoqueRepository estoqueRepo)
        {
            _estoqueRepo = estoqueRepo;
        }

        private readonly int _tipoOperacao = (int)EnumTipoOperacao.Venda;
        private readonly string _nomeOperacao = EnumTipoOperacao.Venda.ToString();

        public int TipoOperacao => _tipoOperacao;
        public string NomeOperacao => _nomeOperacao;

        public async Task EfetuarOperacao(ItemOperacao itemOperacao)
        {
            var estoque = await _estoqueRepo.First(e => e.ProdutoId == itemOperacao.ProdutoId);

            if (estoque == null)
            {
                estoque = new Estoque();
                estoque.ProdutoId = itemOperacao.ProdutoId;
            }

            estoque.Quantidade -= itemOperacao.Quantidade;        
            await _estoqueRepo.SaveChanges();
        }
    }
}
