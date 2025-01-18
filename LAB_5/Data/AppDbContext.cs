using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDbContext : DbContext
    {
        // DbSet dla encji ProduktEntity
        public DbSet<ProduktEntity> Produkty { get; set; }

        // Ścieżka do bazy danych SQLite
        private string DbPath { get; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seedowanie danych przykładowych
            modelBuilder.Entity<ProduktEntity>().HasData(
                new ProduktEntity
                {
                    Id = 1,
                    Name = "Przykładowy Produkt 1",
                    Price = 100,
                    Producent = ProducentType.ProducentA,
                    Produktdate = new DateTime(2023, 1, 1),
                    Description = "Przykładowy opis dla produktu 1."
                },
                new ProduktEntity
                {
                    Id = 2,
                    Name = "Przykładowy Produkt 2",
                    Price = 200,
                    Producent = ProducentType.ProducentB,
                    Produktdate = new DateTime(2023, 6, 1),
                    Description = "Przykładowy opis dla produktu 2."
                }
            );
        }
    }
}
