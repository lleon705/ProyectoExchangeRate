using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExchangeRate.Models
{
    public class Moneda
    {
        [Key]
        public int IdMoneda { get; set; }
        public string Nombre { get; set; }
        public string Pais { get; set; }
        //public List<Exchange> Origenes { get; set; }
        //public List<Exchange> Destinos { get; set; }
    }
}
