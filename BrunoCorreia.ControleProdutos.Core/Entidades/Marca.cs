using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BrunoCorreia.ControleProdutos.Core.Entidades
{
    public class Marca : IAggregateRoot
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
