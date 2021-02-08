using BrunoCorreia.ControleProdutos.Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrunoCorreia.ControleProdutos.Infrastructure.ConfiguracoesEntidades
{
    class ConfituracaoEntidadeEstoque : IEntityTypeConfiguration<Estoque>
    {
        public void Configure(EntityTypeBuilder<Estoque> builder)
        {
            builder.ToTable("Estoque");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).UseHiLo("Estoqueseq");

            builder.HasOne(c=>c.Produto)
              .WithMany()
              .HasForeignKey(c => c.ProdutoId)
              .IsRequired(true);
        }
    }
}
