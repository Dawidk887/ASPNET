using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;

public class ProduktController : Controller
{
    private readonly IProduktService _service;
    private readonly AppDbContext _context;

    public ProduktController(IProduktService service, AppDbContext context)
    {
        _context = context;
        _service = service;
    }

    public IActionResult Index()
    {
        var produkty = _context.Produkty
            .Include(p => p.Organization) 
            .ToList();

        return View(produkty);
    }

    public IActionResult Details(int id)
    {
        var produkt = _context.Produkty
            .Include(p => p.Organization) // Do³¹czenie organizacji do produktu
            .FirstOrDefault(p => p.Id == id);

        if (produkt == null)
        {
            return NotFound();
        }

        return View(produkt);
    }

    public IActionResult Create()
    {
        // Pobieranie organizacji z bazy danych
        ViewBag.Organizations = _context.Organizations
            .Select(o => new SelectListItem
            {
                Value = o.Id.ToString(),
                Text = o.Title
            }).ToList();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ProduktEntity produkt)
    {
        if (true)
        {
            _context.Produkty.Add(produkt);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Jeœli model jest niepoprawny, ponownie przekazujemy organizacje
        ViewBag.Organizations = _context.Organizations
            .Select(o => new SelectListItem
            {
                Value = o.Id.ToString(),
                Text = o.Title
            }).ToList();

        return View(produkt);
    }

    public IActionResult Edit(int id)
    {
        var produkt = _context.Produkty
            .Include(p => p.Organization)
            .FirstOrDefault(p => p.Id == id);

        if (produkt == null)
        {
            return NotFound();
        }

        ViewBag.Organizations = _context.Organizations
            .Select(o => new SelectListItem
            {
                Value = o.Id.ToString(),
                Text = o.Title
            }).ToList();

        return View(produkt);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, ProduktEntity produkt)
    {
        if (id != produkt.Id || !ModelState.IsValid)
        {
            return View(produkt);
        }
        _service.Update(produkt);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var produkt = _service.GetById(id);
        if (produkt == null)
        {
            return NotFound();
        }

        _service.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
