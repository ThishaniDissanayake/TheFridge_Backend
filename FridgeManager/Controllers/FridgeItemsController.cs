using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FridgeManagerAPI.Data;
using FridgeManagerAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgeManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeItemsController : ControllerBase
    {
        private readonly FridgeContext _context;

        public FridgeItemsController(FridgeContext context)
        {
            _context = context;
        }

        // GET: api/fridgeitems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FridgeItem>>> GetFridgeItems()
        {
            return await _context.FridgeItems.ToListAsync();
        }

        // GET: api/fridgeitems/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FridgeItem>> GetFridgeItem(int id)
        {
            var fridgeItem = await _context.FridgeItems.FindAsync(id);

            if (fridgeItem == null)
                return NotFound();

            return fridgeItem;
        }

        // POST: api/fridgeitems
        [HttpPost]
        public async Task<ActionResult<FridgeItem>> PostFridgeItem(FridgeItem fridgeItem)
        {
            var existingItem = await _context.FridgeItems
                                             .FirstOrDefaultAsync(f => f.Name == fridgeItem.Name && f.ExpiryDate == fridgeItem.ExpiryDate);

            if (existingItem != null)
            {
                return Conflict("Item with the same name and expiry date already exists.");
            }

            fridgeItem.Id = 0;
            _context.FridgeItems.Add(fridgeItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFridgeItem), new { id = fridgeItem.Id }, fridgeItem);
        }

        // PUT: api/fridgeitems/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFridgeItem(int id, FridgeItem fridgeItem)
        {
            if (id != fridgeItem.Id)
                return BadRequest();

            _context.Entry(fridgeItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FridgeItemExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/fridgeitems/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFridgeItem(int id)
        {
            var fridgeItem = await _context.FridgeItems.FindAsync(id);
            if (fridgeItem == null)
                return NotFound();

            _context.FridgeItems.Remove(fridgeItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FridgeItemExists(int id)
        {
            return _context.FridgeItems.Any(e => e.Id == id);
        }
    }
}
