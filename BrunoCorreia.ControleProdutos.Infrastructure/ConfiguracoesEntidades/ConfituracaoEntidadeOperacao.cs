using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Operacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrunoCorreia.ControleProdutos.Infrastructure.ConfiguracoesEntidades
{
    class ConfituracaoEntidadeOperacao : IEntityTypeConfiguration<Operacao>
    {
        public void Configure(EntityTypeBuilder<Operacao> builder)
        {
            builder.ToTable("Operacao");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).UseHiLo("Operacaoseq");

            var navigation = builder.Metadata.FindNavigation(nameof(Operacao.ItensOperacao));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
