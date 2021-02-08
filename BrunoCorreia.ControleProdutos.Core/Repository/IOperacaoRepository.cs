using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Operacao;
using BrunoCorreia.ControleProdutos.Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrunoCorreia.ControleProdutos.Core.Repository
{
    public interface IOperacaoRepository : IRepository<Operacao>
    {
        Task<IEnumerable<Operacao>> GetAsync(DateTime dataIni, DateTime dataFin);
        Task<IEnumerable<Operacao>> GetAsync(int produtoId, DateTime dataIni, DateTime dataFin);
    }
}
