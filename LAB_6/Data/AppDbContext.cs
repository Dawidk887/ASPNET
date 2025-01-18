using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDbContext : DbContext
    {
        // DbSet dla encji ProduktEntity
        public DbSet<ProduktEntity> Produkty { get; set; }
        public DbSet<OrganizationEntity> Organizations { get; set; }

        // Ścieżka do bazy danych SQLite
        private string DbPath { get; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<OrganizationEntity>()
                .OwnsOne(e => e.Address);

            modelBuilder.Entity<OrganizationEntity>().ToTable("organizations").HasData(
                new OrganizationEntity()
                {
                    Id = 101,
                    Title = "Biedronka",
                    Nip = "83492384",
                    Regon = "13424234",
                },
                new OrganizationEntity()
                {
                    Id = 102,
                    Title = "Lidl",
                    Nip = "2498534",
                    Regon = "0873439249",
                },
                new OrganizationEntity()
                {
                    Id = 100,
                    Title = "Domyslna",
                    Nip = "83492100",
                    Regon = "13777234",
                }
            );

            modelBuilder.Entity<ProduktEntity>().HasData(
                new ProduktEntity
                {
                    Id = 1,
                    Name = "Przykładowy Produkt 1",
                    Price = 100,
                    Producent = ProducentType.ProducentA,
                    Produktdate = new DateTime(2023, 1, 1),
                    Description = "Przykładowy opis dla produktu 1.",
                    OrganizationId = 101,
                },
                new ProduktEntity
                {
                    Id = 2,
                    Name = "Przykładowy Produkt 2",
                    Price = 200,
                    Producent = ProducentType.ProducentB,
                    Produktdate = new DateTime(2023, 6, 1),
                    Description = "Przykładowy opis dla produktu 2.",
                    OrganizationId = 102,
                }
            );

            modelBuilder.Entity<OrganizationEntity>()
                .OwnsOne(e => e.Address)
                .HasData(
                    new { OrganizationEntityId = 101, City = "Kraków", Street = "Św. Filipa 17", PostalCode = "31-150", Region = "małopolskie" },
                    new { OrganizationEntityId = 102, City = "Kraków", Street = "Krowoderska 45/6", PostalCode = "31-150", Region = "małopolskie" }
                );

            modelBuilder.Entity<ProduktEntity>()
               .HasOne(e => e.Organization)
               .WithMany(o => o.Produkts)
               .HasForeignKey(e => e.OrganizationId);

            modelBuilder.Entity<ProduktEntity>()
               .Property(e => e.OrganizationId)
               .HasDefaultValue(101);

            modelBuilder.Entity<ProduktEntity>()
                .Property(e => e.Produktdate)
                .HasDefaultValue(DateTime.Now);
        }
    }
}