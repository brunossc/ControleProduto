using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Categoria
{
    public class Categoria : IAggregateRoot
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ReajustePrecoCategoria Reajuste { get; set; }
        public int? ReajusteId { get;  set; }
    }
}
