using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Categoria;
using BrunoCorreia.ControleProdutos.Core.Repository;
using BrunoCorreia.ControleProdutos.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BrunoCorreia.ControleProdutos.Infrastructure.Repository
{
    public class CategoriaRepository : DataRepository<Categoria>,  ICategoriaRepository
    {
        public CategoriaRepository(ControleProdutosContext context) : base(context)
        {
        }

        public override async Task<Categoria> First(Expression<Func<Categoria, bool>> predicate)
        {
            return await _objectSet.Include(c=>c.Reajuste).FirstOrDefaultAsync(predicate, new System.Threading.CancellationToken());
        }
    }
}
