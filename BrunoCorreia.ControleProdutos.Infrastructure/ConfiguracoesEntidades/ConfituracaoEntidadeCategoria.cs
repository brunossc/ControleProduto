using BrunoCorreia.ControleProdutos.Core.Entidades;
using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Categoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrunoCorreia.ControleProdutos.Infrastructure.ConfiguracoesEntidades
{
    class ConfituracaoEntidadeCategoria : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).UseHiLo("Categoriaseq");

            builder.HasOne(c=>c.Reajuste)
              .WithMany()
              .HasForeignKey(c => c.ReajusteId)
              .IsRequired(false)
              .OnDelete(DeleteBehavior.Cascade | DeleteBehavior.Restrict);
        }
    }
}
