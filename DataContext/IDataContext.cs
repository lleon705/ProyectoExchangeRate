using ExchangeRate.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ExchangeRate.DataContext
{
    public interface IDataContext
    {
        DbSet<Exchange> Exchanges { get; set; }
        DbSet<Moneda> Monedas { get; set; }

        Task<int> SaveChanges();
    }
}