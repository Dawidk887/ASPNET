using System.Collections.Generic;
using System.Linq;
using Data;
using Data.Entities;
using WebApp.Models;

namespace WebApp.Services;

public class EFProduktService : IProduktService
{
    private readonly AppDbContext _context;

    public EFProduktService(AppDbContext context)
    {
        _context = context;
    }

    // Pobierz wszystkie produkty i zmapuj na model domenowy
    public IEnumerable<ProduktEntity> GetAll()
    {
        return _context.Produkty.Select(p => new ProduktEntity
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Producent = (Data.Entities.ProducentType)p.Producent,
            Produktdate = p.Produktdate,
            Description = p.Description
        }).ToList();
    }

    // Pobierz produkt na podstawie ID
    public ProduktEntity GetById(int id)
    {
        var produktEntity = _context.Produkty.FirstOrDefault(p => p.Id == id);
        if (produktEntity == null) return null;

        return new ProduktEntity
        {
            Id = produktEntity.Id,
            Name = produktEntity.Name,
            Price = produktEntity.Price,
            Producent = (Data.Entities.ProducentType)produktEntity.Producent,
            Produktdate = produktEntity.Produktdate,
            Description = produktEntity.Description
        };
    }

    // Dodaj nowy produkt
    public void Add(ProduktEntity produkt)
    {
        var produktEntity = new ProduktEntity
        {
            Name = produkt.Name,
            Price = produkt.Price,
            Producent = (Data.Entities.ProducentType)produkt.Producent,
            Produktdate = produkt.Produktdate,
            Description = produkt.Description
        };

        _context.Produkty.Add(produktEntity);
        _context.SaveChanges();
    }

    // Zaktualizuj istniejący produkt
    public void Update(ProduktEntity produkt)
    {
        var existing = _context.Produkty.FirstOrDefault(p => p.Id == produkt.Id);
        if (existing != null)
        {
            existing.Name = produkt.Name;
            existing.Price = produkt.Price;
            existing.Producent = (Data.Entities.ProducentType)produkt.Producent;
            existing.Produktdate = produkt.Produktdate;
            existing.Description = produkt.Description;

            _context.SaveChanges();
        }
    }

    // Usuń produkt
    public void Delete(int id)
    {
        var produkt = _context.Produkty.FirstOrDefault(p => p.Id == id);
        if (produkt != null)
        {
            _context.Produkty.Remove(produkt);
            _context.SaveChanges();
        }
    }
}

