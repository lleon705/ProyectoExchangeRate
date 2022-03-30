using ExchangeRate.DataContext;
using ExchangeRate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExchangeRate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExchangeController : ControllerBase
    {
        private readonly ILogger<ExchangeController> _logger;

        private IDataContext _context;
        public ExchangeController(IDataContext context, ILogger<ExchangeController> logger)
        {
            _context = context;
            _logger = logger;
            if (_context.Monedas.Count() == 0)
                DatosTest.AddTestData(_context);
        }
        // GET: api/<ExchangeController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var exchanges = await _context.Exchanges.ToListAsync();

            if (exchanges == null) return NotFound();
            var monedas = await _context.Monedas.ToListAsync();
            var response = exchanges.Select(x =>
                          new ExchangeResponse()
                          {
                              IdExchange=x.IdExchange,
                              ExchangeRate = x.ExchangeRate,
                              Origen = monedas.Find(y => y.IdMoneda == x.IdOrigen),
                              Destino = monedas.Find(z => z.IdMoneda == x.IdDestino)
                          }).ToList();
            return Ok(new ExchangeResponseList() { Exchange=response });
        }

        [HttpPost("ExchangeRate")]
        public async Task<IActionResult> Calcular(ExchangeRequest exchangeRequest)
        {
            if (exchangeRequest.Cantidad==0)
                return NotFound();
            var exchanges = new Exchange();
                if(exchangeRequest.IdExchange!=null)
                     exchanges = await _context.Exchanges.Where(c=>c.IdExchange==(int)exchangeRequest.IdExchange).FirstOrDefaultAsync();
                else if (exchangeRequest.IdOrigen != 0 && exchangeRequest.IdDestino != 0)
                {
                    exchanges = await _context.Exchanges.Where(c => c.IdOrigen== exchangeRequest.IdOrigen && c.IdDestino==exchangeRequest.IdDestino).FirstOrDefaultAsync();
                }
                else
                {
                    return NotFound();
                }
                
            if (exchanges == null) return NotFound();
            var monedas = await _context.Monedas.ToListAsync();
            var response =new ExchangeResponse()
                          {
                              IdExchange = exchanges.IdExchange,
                              ExchangeRate = exchanges.ExchangeRate,
                              Origen = monedas.Find(y => y.IdMoneda == exchanges.IdOrigen),
                              Destino = monedas.Find(z => z.IdMoneda == exchanges.IdDestino),
                              Calculo= Math.Round(exchangeRequest.Cantidad/exchanges.ExchangeRate,2)
            };
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Exchange exchange)
        {
            _context.Exchanges.Add(exchange);
            await _context.SaveChanges();
            return Ok(exchange.IdExchange);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var exchange = await _context.Exchanges.Where(a => a.IdExchange == id).FirstOrDefaultAsync();
            if (exchange == null) return NotFound();
            var monedas = await _context.Monedas.ToListAsync();
            var response =  new ExchangeResponse()
              {
                  IdExchange = exchange.IdExchange,   
                  ExchangeRate = exchange.ExchangeRate,
                  Origen = monedas.Find(y => y.IdMoneda == exchange.IdOrigen),
                  Destino = monedas.Find(z => z.IdMoneda == exchange.IdDestino)
              };
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exchange = await _context.Exchanges.Where(a => a.IdExchange == id).FirstOrDefaultAsync();
            if (exchange == null) return NotFound();
            _context.Exchanges.Remove(exchange);
            await _context.SaveChanges();
            return Ok(exchange.IdExchange);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Exchange exchange)
        {
            var exchangeresult = _context.Exchanges.Where(a => a.IdExchange == id).FirstOrDefault();
            if (exchangeresult == null) return NotFound();
            else
            {
                exchangeresult.IdDestino =exchange.IdDestino;
                exchangeresult.IdOrigen = exchange.IdOrigen;
                exchangeresult.ExchangeRate = exchange.ExchangeRate;
                await _context.SaveChanges();
                return Ok(exchangeresult.IdExchange);
            }
        }
    }
}
