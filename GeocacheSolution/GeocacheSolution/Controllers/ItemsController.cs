using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GeocacheSolution.Data;
using GeocacheSolution.Models;
using System.Text.RegularExpressions;

namespace GeocacheSolution.Controllers
{
    public class ItemsController : Controller
    {
        private readonly GeocacheContext _context;

        public ItemsController(GeocacheContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            return _context.Items != null ?
                          View(await _context.Items.ToListAsync()) :
                          Problem("Entity set 'GeocacheContext.Geocaches'  is null.");
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Active,GeocacheId,FirstActive,LastActive")] Item item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    foreach (var i in _context.Items)
                    {
                        if(item.Name == i.Name)
                        {
                            throw new DbUpdateException();
                        }
                    }
                    var geocache = await _context.Geocaches
                        .Include(g => g.Items)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.ID == item.GeocacheId);
                    if (geocache.Items.Count >= 3)
                    {
                        throw new DbUpdateException();
                    }
                    if ((item.LastActive.AddDays(30) < DateTime.Today))
                    {
                        item.Active = false;
                    }
                    _context.Add(item);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException e)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "One or more input fields received incorrect data.");
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Active,GeocacheId,FirstActive,LastActive")] Item item)
        {
            if (id != item.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if ((item.LastActive.AddDays(30) < DateTime.Today))
                    {
                        item.Active = false;
                    }
                    await _context.SaveChangesAsync();
                    var geocacheMovedTo = await _context.Geocaches
                        .Include(g => g.Items)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.ID == item.GeocacheId);
                    var oldItemValues = await _context.Items.AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);
                    var geocacheRemovedFrom = await _context.Geocaches
                        .Include(g => g.Items)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.ID == oldItemValues.GeocacheId);
                    if (item.Active == false)
                    {
                        if(item.GeocacheId != geocacheRemovedFrom.ID)
                        {
                            throw new DbUpdateException();
                        }
                    }
                    if(oldItemValues.GeocacheId != item.GeocacheId)
                        if (geocacheMovedTo.Items.Count >= 3)
                        {
                            throw new DbUpdateException();
                        }
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "One or more of your input fields received incorrect data.");
                }
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'GeocacheContext.Items'  is null.");
            }
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
          return (_context.Items?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
