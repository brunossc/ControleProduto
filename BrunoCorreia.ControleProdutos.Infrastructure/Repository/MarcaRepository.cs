using BrunoCorreia.ControleProdutos.Core.Entidades;
using BrunoCorreia.ControleProdutos.Core.Repository;
using BrunoCorreia.ControleProdutos.Infrastructure.Repository.Base;

namespace BrunoCorreia.ControleProdutos.Infrastructure.Repository
{
    public class MarcaRepository : DataRepository<Marca>, IMarcaRepository
    {
        public MarcaRepository(ControleProdutosContext context) : base(context)
        {
        }
    }
}
