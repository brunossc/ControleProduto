using BrunoCorreia.ControleProdutos.Core.Entidades;
using BrunoCorreia.ControleProdutos.Core.Repository;
using BrunoCorreia.ControleProdutos.Infrastructure.Repository.Base;

namespace BrunoCorreia.ControleProdutos.Infrastructure.Repository
{
    public class ProdutoRepository : DataRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ControleProdutosContext context) : base(context)
        {
        }
    }
}