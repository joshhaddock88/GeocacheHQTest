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
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly GeocacheContext _context;

        public ItemsController(GeocacheContext context)
        {
            _context = context;
        }

        // GET: Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }

        // GET: Items/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST: api/Items
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {

            var itemNames = await _context.Items.Select(i => i.Name).AsNoTracking().ToListAsync();
            if (itemNames.Contains(item.Name))
            {
                return BadRequest("Name already exists. Item names must be unique.");
            }
            item.Active = CheckIfActive(item.LastActive, item.Active);
            if (item.GeocacheId != null)
            {
                if (item.Active == false)
                {
                    return BadRequest("Item is not active and therefore can not be placed in Geocache. Check last known activity.");
                }
                var geocache = await _context.Geocaches
                    .Include(g => g.Items)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.ID == item.GeocacheId);
                if (CheckForSpaceInGeocache(geocache.Items.Count))
                {
                    return BadRequest("Target geocache is full");
                }
            }
            _context.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetItem", new { id = item.ID }, item);
        }

        // PUT: api/Items
        [HttpPut("{id}")]
        public async Task<ActionResult> PutItem(int id, Item item)
        {
           
            if(id != item.ID)
            {
                return BadRequest("ID does not match.");
            }
            item.Active = CheckIfActive(item.LastActive, item.Active);
            if (item.GeocacheId != null)
            {
                var geocacheMovedTo = await _context.Geocaches
                    .Include(g => g.Items)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.ID == item.GeocacheId);
                var oldItemValues = await _context.Items.AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);
                if (oldItemValues.GeocacheId != null)
                {
                    if (item.Active == false && item.GeocacheId != oldItemValues.GeocacheId)
                    {
                        return BadRequest("Item is not active and therefore may not be moved.");
                    }
                    else if (oldItemValues.GeocacheId != item.GeocacheId)
                    {
                        if(CheckForSpaceInGeocache(geocacheMovedTo.Items.Count))
                        {
                            return BadRequest("Target geocache is full");
                        }
                    }
                }
                else
                {
                    if (item.Active == false)
                    {
                        return BadRequest("Item is not active and therefore may not be moved.");
                    }
                    if (CheckForSpaceInGeocache(geocacheMovedTo.Items.Count))
                    {
                        return BadRequest("Target geocache is full");
                    }
                }

            }
            _context.Update(item);
            await _context.SaveChangesAsync();
            return Ok(item);
        }

    // DELETE: Items/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool CheckForSpaceInGeocache(int count)
        {
            if (count >= 3) { return false; }
            else { return true; }
        }

        private bool CheckIfActive(DateTime lastActive, bool currentState)
        {
            if ((lastActive.AddDays(30) < DateTime.Today)) { return false; }
            return currentState;
        }
    }
}
