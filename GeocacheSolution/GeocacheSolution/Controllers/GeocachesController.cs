using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GeocacheSolution.Data;
using GeocacheSolution.Models;

namespace GeocacheSolution.Controllers
{
    public class GeocachesController : Controller
    {
        private readonly GeocacheContext _context;

        public GeocachesController(GeocacheContext context)
        {
            _context = context;
        }

        // GET: Geocaches
        public async Task<IActionResult> Index()
        {
              return _context.Geocaches != null ? 
                          View(await _context.Geocaches.ToListAsync()) :
                          Problem("Entity set 'GeocacheContext.Geocaches'  is null.");
        }

        // GET: Geocaches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Geocaches == null)
            {
                return NotFound();
            }

            var geocache = await _context.Geocaches
                .FirstOrDefaultAsync(m => m.ID == id);
            if (geocache == null)
            {
                return NotFound();
            }

            return View(geocache);
        }

        // GET: Geocaches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Geocaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Lat,Long")] Geocache geocache)
        {
            if (ModelState.IsValid)
            {
                _context.Add(geocache);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(geocache);
        }

        // GET: Geocaches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Geocaches == null)
            {
                return NotFound();
            }

            var geocache = await _context.Geocaches.FindAsync(id);
            if (geocache == null)
            {
                return NotFound();
            }
            return View(geocache);
        }

        // POST: Geocaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Lat,Long")] Geocache geocache)
        {
            if (id != geocache.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(geocache);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeocacheExists(geocache.ID))
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
            return View(geocache);
        }

        // GET: Geocaches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Geocaches == null)
            {
                return NotFound();
            }

            var geocache = await _context.Geocaches
                .FirstOrDefaultAsync(m => m.ID == id);
            if (geocache == null)
            {
                return NotFound();
            }

            return View(geocache);
        }

        // POST: Geocaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Geocaches == null)
            {
                return Problem("Entity set 'GeocacheContext.Geocaches'  is null.");
            }
            var geocache = await _context.Geocaches.FindAsync(id);
            if (geocache != null)
            {
                _context.Geocaches.Remove(geocache);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeocacheExists(int id)
        {
          return (_context.Geocaches?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
