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
    [Route("api/[controller]")]
    [ApiController]
    public class GeocachesController : Controller
    {
        private readonly GeocacheContext _context;

        public GeocachesController(GeocacheContext context)
        {
            _context = context;
        }

        // GET: Geocaches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Geocache>>> GetItems()
        {
            return await _context.Geocaches.ToListAsync();
        }

        // GET: Geocaches/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Geocache>> GetGeocache(int id)
        {
            var geocache = await _context.Geocaches
                .Include(g => g.Items)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (geocache == null)
            {
                return NotFound();
            }
            foreach(Item item in geocache.Items)
            {
                if(item.Active == false)
                {
                    geocache.Items.Remove(item);
                }
            }

            return geocache;
        }

        // POST: api/Geocaches
        [HttpPost]
        public async Task<ActionResult<Geocache>> PostGeocache(Geocache geocache)
        {
            _context.Add(geocache);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetGeocache", new { id = geocache.ID}, geocache);
        }

        // PUT: api/Geocaches/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Lat,Long")] Geocache geocache)
        {
            if (id != geocache.ID)
            {
                return BadRequest("ID does not match a known cache.");
            }
            
            _context.Update(geocache);
            await _context.SaveChangesAsync();
            return Ok(geocache);
        }

        // DELETE: api/Geocaches/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var geocache = await _context.Geocaches.FindAsync(id);
            if (geocache != null)
            {
                _context.Geocaches.Remove(geocache);
            }
            
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool GeocacheExists(int id)
        {
          return (_context.Geocaches?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
