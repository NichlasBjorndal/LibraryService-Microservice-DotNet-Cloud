using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookService.Models;

namespace BookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompletedOrdersController : ControllerBase
    {
        private readonly BookServiceContext _context;

        public CompletedOrdersController(BookServiceContext context)
        {
            _context = context;
        }

        // GET: api/CompletedOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompletedOrder>>> GetCompletedOrder()
        {
            return await _context.CompletedOrder.ToListAsync();
        }

        // GET: api/CompletedOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompletedOrder>> GetCompletedOrder(int id)
        {
            var completedOrder = await _context.CompletedOrder.FindAsync(id);

            if (completedOrder == null)
            {
                return NotFound();
            }

            return completedOrder;
        }

        // PUT: api/CompletedOrders/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompletedOrder(int id, CompletedOrder completedOrder)
        {
            if (id != completedOrder.Id)
            {
                return BadRequest();
            }

            _context.Entry(completedOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompletedOrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CompletedOrders
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CompletedOrder>> PostCompletedOrder(CompletedOrder completedOrder)
        {
            _context.CompletedOrder.Add(completedOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompletedOrder", new { id = completedOrder.Id }, completedOrder);
        }

        // DELETE: api/CompletedOrders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CompletedOrder>> DeleteCompletedOrder(int id)
        {
            var completedOrder = await _context.CompletedOrder.FindAsync(id);
            if (completedOrder == null)
            {
                return NotFound();
            }

            _context.CompletedOrder.Remove(completedOrder);
            await _context.SaveChangesAsync();

            return completedOrder;
        }

        private bool CompletedOrderExists(int id)
        {
            return _context.CompletedOrder.Any(e => e.Id == id);
        }
    }
}
