using BrunoCorreia.ControleProdutos.Core.Entidades;
using BrunoCorreia.ControleProdutos.Core.Repository;
using BrunoCorreia.ControleProdutos.Infrastructure.Repository.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrunoCorreia.ControleProdutos.Infrastructure.Repository
{
    public class EstoqueRepository : DataRepository<Estoque>, IEstoqueRepository
    {
        public EstoqueRepository(ControleProdutosContext context) : base(context)
        {
        }
    }
}
