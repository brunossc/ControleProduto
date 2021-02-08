using BrunoCorreia.ControleProdutos.Core.DTO;
using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Operacao;
using BrunoCorreia.ControleProdutos.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BrunoCorreia.ControleProdutos.Core.Service
{
    public class OperacaoService
    {
        IEstoqueRepository _estoqueRepo;
        IOperacaoRepository _operacaoRepo;

        public OperacaoService(IEstoqueRepository estoqueRepo, IOperacaoRepository operacaoRepository)
        {         
            _operacaoRepo = operacaoRepository;
            _estoqueRepo = estoqueRepo;
        }

        public async Task<int> EfetuarCompra(OperacaoRequest operacao)
        {
            var novaOperacao = new Operacao(new OperacaoCompra(_estoqueRepo));
            return await this.Processar(novaOperacao, operacao);
        }

        public async Task<int> EfetuarVenda(OperacaoRequest operacao)
        {
            var novaOperacao = new Operacao(new OperacaoVenda(_estoqueRepo));
            return await this.Processar(novaOperacao, operacao);
        }

        private async Task<int> Processar(Operacao novaOperacao, OperacaoRequest operacao)
        {
            novaOperacao.StatusOperacao = EnumStatusOperacao.Iniciada;

            foreach (var item in operacao.ItensOperacao)
            {
                var itemOperacao = new ItemOperacao();
                itemOperacao.Preco = item.Preco;
                itemOperacao.ProdutoId = item.ProdutoId;
                itemOperacao.Quantidade = item.Quantidade;
                novaOperacao.AdicionarItemOperacao(itemOperacao);
            }

            var operacaoIniciada = await _operacaoRepo.Add(novaOperacao);
            

            try
            {
                await operacaoIniciada.ProcessarOperacao();
                operacaoIniciada.StatusOperacao = EnumStatusOperacao.Finalizada;
                await _operacaoRepo.SaveChanges();

                return operacaoIniciada.Id;
            }
            catch(Exception ex)
            {
                operacaoIniciada.StatusOperacao = EnumStatusOperacao.Cancelada;
                await _operacaoRepo.SaveChanges();
                return -1;
            }            
        }
    }
}
