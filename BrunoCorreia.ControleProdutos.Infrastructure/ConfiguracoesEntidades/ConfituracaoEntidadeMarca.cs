using BrunoCorreia.ControleProdutos.Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrunoCorreia.ControleProdutos.Infrastructure.ConfiguracoesEntidades
{
    class ConfituracaoEntidadeMarca : IEntityTypeConfiguration<Marca>
    {
        public void Configure(EntityTypeBuilder<Marca> builder)
        {
            builder.ToTable("Marca");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).UseHiLo("Marcaseq");
        }
    }
}
