using System;

namespace ExchangeRate.DataContext
{
    public static class DatosTest
    {
        public static void AddTestData(IDataContext _context)
        {
            var testMoneda = new Models.Moneda { Pais = "Peru", Nombre = "Sol" };

            _context.Monedas.Add(testMoneda);

            testMoneda = new Models.Moneda { Pais = "Estados Unidos", Nombre = "Dolar" };

            _context.Monedas.Add(testMoneda);

            testMoneda = new Models.Moneda { Pais = "Argentina", Nombre = "Peso Argentino" };

            _context.Monedas.Add(testMoneda);

            var testExchange = new Models.Exchange { IdOrigen = 1, IdDestino = 2, ExchangeRate = 3.75 };
            _context.Exchanges.Add(testExchange);

            testExchange = new Models.Exchange { IdOrigen = 3, IdDestino = 2, ExchangeRate = 110.94 };
            _context.Exchanges.Add(testExchange);

            _context.SaveChanges();
        }
    }
}
