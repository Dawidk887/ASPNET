using WebApp.Models;
using System.Collections.Generic;
using Data.Entities;

namespace WebApp.Services;

public interface IProduktService
{
    IEnumerable<ProduktEntity> GetAll();
    ProduktEntity GetById(int id);
    void Add(ProduktEntity produkt);
    void Update(ProduktEntity produkt);
    void Delete(int id);
}

