using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Operacao;
using BrunoCorreia.ControleProdutos.Core.Repository;
using BrunoCorreia.ControleProdutos.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BrunoCorreia.ControleProdutos.Infrastructure.Repository
{
    public class OperacaoRepository : DataRepository<Operacao>, IOperacaoRepository
    {
        public OperacaoRepository(ControleProdutosContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Operacao>> Find(Expression<Func<Operacao, bool>> predicate)
        {
            return await Task.Run(() =>
            {
                return _objectSet.Include(c => c.ItensOperacao).Where(predicate);
            });
        }

        public Task<IEnumerable<Operacao>> GetAsync(DateTime dataIni, DateTime dataFin) 
        {
            return null;
        }

        public Task<IEnumerable<Operacao>> GetAsync(int produtoId, DateTime dataIni, DateTime dataFin)
        {
            return null;
        }

    }
}
