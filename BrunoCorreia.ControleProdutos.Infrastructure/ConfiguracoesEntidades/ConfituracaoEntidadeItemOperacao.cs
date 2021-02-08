using BrunoCorreia.ControleProdutos.Core.Entidades;
using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Operacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrunoCorreia.ControleProdutos.Infrastructure.ConfiguracoesEntidades
{
    class ConfituracaoEntidadeItemOperacao : IEntityTypeConfiguration<ItemOperacao>
    {
        public void Configure(EntityTypeBuilder<ItemOperacao> builder)
        {
            builder.ToTable("ItemOperacao");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).UseHiLo("ItemOperacaoseq");
        }
    }
}
