using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClasificacionPeliculas.Models;

namespace ClasificacionPeliculas.Controllers
{
    public class PruebaController : Controller
    {
        private readonly MoviesContext _context;

        public PruebaController(MoviesContext context)
        {
            _context = context;
        }

        // GET: Prueba
        public async Task<IActionResult> Index()
        {
            var moviesContext = _context.Moviescategories.Include(m => m.Category).Include(m => m.Movie);
            return View(await moviesContext.ToListAsync());
        }

        // GET: Prueba/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Moviescategories == null)
            {
                return NotFound();
            }

            var moviescategory = await _context.Moviescategories
                .Include(m => m.Category)
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moviescategory == null)
            {
                return NotFound();
            }

            return View(moviescategory);
        }

        // GET: Prueba/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id");
            return View();
        }

        // POST: Prueba/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,CategoryId")] Moviescategory moviescategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moviescategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", moviescategory.CategoryId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", moviescategory.MovieId);
            return View(moviescategory);
        }

        // GET: Prueba/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Moviescategories == null)
            {
                return NotFound();
            }

            var moviescategory = await _context.Moviescategories.FindAsync(id);
            if (moviescategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", moviescategory.CategoryId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", moviescategory.MovieId);
            return View(moviescategory);
        }

        // POST: Prueba/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,CategoryId")] Moviescategory moviescategory)
        {
            if (id != moviescategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moviescategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoviescategoryExists(moviescategory.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", moviescategory.CategoryId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", moviescategory.MovieId);
            return View(moviescategory);
        }

        // GET: Prueba/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Moviescategories == null)
            {
                return NotFound();
            }

            var moviescategory = await _context.Moviescategories
                .Include(m => m.Category)
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moviescategory == null)
            {
                return NotFound();
            }

            return View(moviescategory);
        }

        // POST: Prueba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Moviescategories == null)
            {
                return Problem("Entity set 'MoviesContext.Moviescategories'  is null.");
            }
            var moviescategory = await _context.Moviescategories.FindAsync(id);
            if (moviescategory != null)
            {
                _context.Moviescategories.Remove(moviescategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoviescategoryExists(int id)
        {
          return (_context.Moviescategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
