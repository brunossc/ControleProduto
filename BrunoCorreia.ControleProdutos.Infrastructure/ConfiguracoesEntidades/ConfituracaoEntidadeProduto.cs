using BrunoCorreia.ControleProdutos.Core.Entidades;
using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Categoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrunoCorreia.ControleProdutos.Infrastructure.ConfiguracoesEntidades
{
    class ConfituracaoEntidadeProduto : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).UseHiLo("Produtoseq");

            builder.HasOne(c=>c.Marca)
              .WithMany()
              .HasForeignKey(c => c.MarcaId)
              .IsRequired(true);

            builder.HasOne(c=>c.Categoria)
              .WithMany()
              .HasForeignKey(c => c.CategoriaId)
              .IsRequired(true);
              
        }
    }
}
