using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Categoria;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunoCorreia.ControleProdutos.Core.DTO
{
    public class CategoriaResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public EnumTipoReajuste TipoReajuste { get; set; }
        public decimal Porcentagem { get; set; }
        public bool ReajusteVigente { get; set; }
    }
}
