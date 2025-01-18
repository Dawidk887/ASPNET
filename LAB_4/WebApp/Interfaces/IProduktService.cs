using WebApp.Models;
using System.Collections.Generic;

namespace WebApp.Services;

public interface IProduktService
{
    IEnumerable<Produkt> GetAll();
    Produkt GetById(int id);
    void Add(Produkt produkt);
    void Update(Produkt produkt);
    void Delete(int id);
}

