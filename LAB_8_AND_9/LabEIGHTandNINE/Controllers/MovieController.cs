using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabEIGHTandNINE.Contexts;
using LabEIGHTandNINE.Models;

namespace LabEIGHTandNINE.Controllers
{
    public class MovieController : Controller
    {
        private readonly AppDbContext _context;

        public MovieController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Movie
        [Route("movies")]
        public IActionResult Index(int page = 1, int size = 10)
        {
            return View(PagingListAsync<Movie>.Create(
                (p, s) =>
                    _context.Movies
                    .OrderBy(b => b.Title)
                    .Skip((p - 1) * s)
                    .Take(s)
                    .AsAsyncEnumerable(),
                _context.Movies.Count(),
                page,
                size));
        }

        // GET: Movie/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movie/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieModel model)
        {
            if (ModelState.IsValid)
            {
                var newMovie = new Movie
                {
                    MovieId = _context.Movies.Any() ? _context.Movies.Max(m => m.MovieId) + 1 : 1, // Generowanie ID
                    Title = model.Title,
                    Overview = model.Overview,
                    ReleaseDate = BitConverter.GetBytes(model.ReleaseDate.Ticks), // Konwersja DateTime -> byte[]
                    Budget = model.Budget,
                    Revenue = model.Revenue,
                    Runtime = model.Runtime,
                    MovieStatus = model.MovieStatus,
                    Tagline = model.Tagline,
                    VoteAverage = model.VoteAverage,
                    VoteCount = model.VoteCount
                };

                // Dodanie nowego filmu do kontekstu
                _context.Movies.Add(newMovie);

                // Zapisanie zmian w bazie danych
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Jeśli model jest nieprawidłowy, zwróć widok z błędami
            return View(model);
        }



        // GET: Movie/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MovieId,Title,Budget,Homepage,Overview,Popularity,ReleaseDate,Revenue,Runtime,MovieStatus,Tagline,VoteAverage,VoteCount")] Movie movie)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movie/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'AppDbContext.Movies'  is null.");
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(long id)
        {
          return (_context.Movies?.Any(e => e.MovieId == id)).GetValueOrDefault();
        }

        [Route("api/companies")]
        [HttpGet]
        public IActionResult GetFiltered(string filter)
        {
            return Ok(_context.ProductionCompanies
                .Where(o => o.CompanyName.ToLower().Contains(filter.ToLower()))
                .OrderBy(o => o.CompanyName)
                .AsNoTracking()
                .AsEnumerable()
                );
        }
    }
}
