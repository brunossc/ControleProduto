using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Categoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrunoCorreia.ControleProdutos.Infrastructure.ConfiguracoesEntidades
{
    class ConfituracaoEntidadeReajusteCategoria : IEntityTypeConfiguration<ReajustePrecoCategoria>
    {
        public void Configure(EntityTypeBuilder<ReajustePrecoCategoria> builder)
        {
            builder.ToTable("ReajustePrecoCategoria");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).UseHiLo("ReajustePrecoCategoriaseq");
        }
    }
}
