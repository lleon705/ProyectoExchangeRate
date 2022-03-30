using ExchangeRate.DataContext;
using ExchangeRate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MonedaController : ControllerBase
    {
        private readonly ILogger<MonedaController> _logger;

        private IDataContext _context;
        public MonedaController(IDataContext context, ILogger<MonedaController> logger)
        {
            _context = context;
            _logger = logger;
            if (_context.Monedas.Count() == 0)
                DatosTest.AddTestData(_context);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var monedas = await _context.Monedas.ToListAsync();
            if (monedas == null) return NotFound();
            return Ok(monedas);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Moneda moneda)
        {
            _context.Monedas.Add(moneda);
            await _context.SaveChanges();
            return Ok(moneda.IdMoneda);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var moneda = await _context.Monedas.Where(a => a.IdMoneda == id).FirstOrDefaultAsync();
            if (moneda == null) return NotFound();
            return Ok(moneda);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var moneda = await _context.Monedas.Where(a => a.IdMoneda == id).FirstOrDefaultAsync();
            if (moneda == null) return NotFound();
            _context.Monedas.Remove(moneda);
            await _context.SaveChanges();
            return Ok(moneda.IdMoneda);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Moneda moneda)
        {
            var monedaresult = _context.Monedas.Where(a => a.IdMoneda == id).FirstOrDefault();
            if (monedaresult == null) return NotFound();
            else
            {
                monedaresult.Nombre = moneda.Nombre;
                monedaresult.Pais =moneda.Pais;
                await _context.SaveChanges();
                return Ok(monedaresult.IdMoneda);
            }
        }

    }
}
