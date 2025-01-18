using WebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Services;

public class ProduktService : IProduktService
{
    private readonly List<Produkt> _produkty = new();

    public IEnumerable<Produkt> GetAll() => _produkty;

    public Produkt GetById(int id) => _produkty.FirstOrDefault(p => p.Id == id);

    public void Add(Produkt produkt)
    {
        produkt.Id = _produkty.Any() ? _produkty.Max(p => p.Id) + 1 : 1;
        _produkty.Add(produkt);
    }

    public void Update(Produkt produkt)
    {
        var existing = GetById(produkt.Id);
        if (existing != null)
        {
            existing.Name = produkt.Name;
            existing.Price = produkt.Price;
            existing.Producent = produkt.Producent;
            existing.Produktdate = produkt.Produktdate;
            existing.Description = produkt.Description;
        }
    }

    public void Delete(int id)
    {
        var produkt = GetById(id);
        if (produkt != null)
        {
            _produkty.Remove(produkt);
        }
    }
}
