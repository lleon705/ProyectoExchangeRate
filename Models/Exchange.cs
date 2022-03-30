using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExchangeRate.Models
{
    public class Exchange
    {
        [Key]
        public int IdExchange { get; set; }
        //public Moneda Origen { get; set; }
        //public Moneda Destino { get; set; }
        public int IdOrigen { get; set; }
        public int IdDestino { get; set; }
        public double ExchangeRate { get; set; }
    }
    public class ExchangeResponse
    {
        public int IdExchange { get; set; }
        public Moneda Origen { get; set; }
        public Moneda Destino { get; set; }
        public double ExchangeRate { get; set; }
        public double? Calculo { get; set; }
    }
    public class ExchangeResponseList
    {
        public List<ExchangeResponse> Exchange { get; set; }
    }
    public class ExchangeRequest
    {
        public int? IdExchange { get; set; }
        public int IdOrigen { get; set; }
        public int IdDestino { get; set; }
        public double Cantidad { get; set; }
    }

}
