using ExchangeRate.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ExchangeRate.DataContext
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        { }
        public DbSet<Moneda> Monedas { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Exchange>()
            //    .HasOne(p => p.Origen)
            //    .WithMany(b => b.Origenes)
            //    .HasForeignKey(p => p.IdOrigen);
            //modelBuilder.Entity<Exchange>()
            //    .HasOne(p => p.Destino)
            //    .WithMany(b => b.Destinos)
            //    .HasForeignKey(p => p.IdDestino);
            modelBuilder.Entity<Exchange>()
            .Property(f => f.IdExchange)
            .ValueGeneratedOnAdd();
            modelBuilder.Entity<Moneda>()
            .Property(f => f.IdMoneda)
            .ValueGeneratedOnAdd();
        }
        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }
}
