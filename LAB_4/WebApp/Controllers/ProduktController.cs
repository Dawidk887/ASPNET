using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;

public class ProduktController : Controller
{
    private readonly IProduktService _service;

    public ProduktController(IProduktService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        var produkty = _service.GetAll().ToDictionary(p => p.Id, p => p);
        return View(produkty);

    }

    public IActionResult Details(int id)
    {
        var produkt = _service.GetById(id);
        if (produkt == null)
        {
            return NotFound();
        }
        return View(produkt);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Produkt produkt)
    {
        if (true)
        {
            _service.Add(produkt);
            return RedirectToAction(nameof(Index));
        }
        return View(produkt);
    }

    public IActionResult Edit(int id)
    {
        var produkt = _service.GetById(id);
        if (produkt == null)
        {
            return NotFound();
        }
        return View(produkt);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Produkt produkt)
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
