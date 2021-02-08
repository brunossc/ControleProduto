using BrunoCorreia.ControleProdutos.Core.Entidades;
using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Categoria;
using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Operacao;
using BrunoCorreia.ControleProdutos.Core.Repository.Base;
using BrunoCorreia.ControleProdutos.Infrastructure.ConfiguracoesEntidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Threading;
using System.Threading.Tasks;

namespace BrunoCorreia.ControleProdutos.Infrastructure
{
    public class ControleProdutosContext : DbContext, IUnitOfWork
    {
        public DbSet<Operacao> Operacao { get; set; }
        public DbSet<ItemOperacao> ItemOperacao { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<ReajustePrecoCategoria> ReajustePrecoCategoria { get; set; }

        //private IDbContextTransaction _currentTransaction;
        public ControleProdutosContext(DbContextOptions<ControleProdutosContext> options) : base(options) { }
        //public bool HasActiveTransaction => _currentTransaction != null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConfituracaoEntidadeCategoria());
            modelBuilder.ApplyConfiguration(new ConfituracaoEntidadeEstoque());
            modelBuilder.ApplyConfiguration(new ConfituracaoEntidadeItemOperacao());
            modelBuilder.ApplyConfiguration(new ConfituracaoEntidadeMarca());
            modelBuilder.ApplyConfiguration(new ConfituracaoEntidadeOperacao());
            modelBuilder.ApplyConfiguration(new ConfituracaoEntidadeProduto());
            modelBuilder.ApplyConfiguration(new ConfituracaoEntidadeReajusteCategoria());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            //await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

    public class ControleProdutosContextDesignFactory : IDesignTimeDbContextFactory<ControleProdutosContext>
    {
        public ControleProdutosContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ControleProdutosContext>()
                .UseSqlServer("Server=DESKTOP-9DKI4RP\\BMBNARFSQL;Database=ControleProdutos; Trusted_Connection=True;");

            return new ControleProdutosContext(optionsBuilder.Options);
        }
    }
}
