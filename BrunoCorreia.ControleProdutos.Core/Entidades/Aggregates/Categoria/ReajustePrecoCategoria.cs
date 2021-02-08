using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Categoria
{
    public class ReajustePrecoCategoria
    {
        public int Id { get; set; }
        public EnumTipoReajuste TipoReajuste { get; set; }
        public decimal Porcentagem { get; set; }
        public DateTime? InicioVigencia { get; set; }
        public DateTime? FinalVigencia { get; set; }

        public bool Vigente()
        {
            if ((InicioVigencia == null && FinalVigencia == null)
                || (InicioVigencia == null && FinalVigencia.Value.Date >= DateTime.Now.Date)
                || (FinalVigencia == null && InicioVigencia.Value.Date <= DateTime.Now.Date)
                || (InicioVigencia.Value.Date <= DateTime.Now.Date && FinalVigencia.Value.Date >= DateTime.Now.Date))
            {
                return true;
            }

            return false;
        }
    }
}
