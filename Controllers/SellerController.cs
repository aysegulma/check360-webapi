using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CheckWebApi.Models;

namespace CheckWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly CheckDbContext _context;

        public SellerController(CheckDbContext context)
        {
            _context = context;
        }

        // GET: api/Seller
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChkSeller>>> GetChkSellers()
        {
            return await _context.ChkSellers.ToListAsync();
        }

        // GET: api/Seller/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChkSeller>> GetChkSeller(int id)
        {
            var chkSeller = await _context.ChkSellers.FindAsync(id);

            if (chkSeller == null)
            {
                return NotFound();
            }

            return chkSeller;
        }

        // PUT: api/Seller/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChkSeller(int id, ChkSeller chkSeller)
        {
            if (id != chkSeller.SellerId)
            {
                return BadRequest();
            }

            _context.Entry(chkSeller).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChkSellerExists(id))
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

        // POST: api/Seller
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChkSeller>> PostChkSeller(ChkSeller chkSeller)
        {
            _context.ChkSellers.Add(chkSeller);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChkSeller", new { id = chkSeller.SellerId }, chkSeller);
        }

        // DELETE: api/Seller/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChkSeller(int id)
        {
            var chkSeller = await _context.ChkSellers.FindAsync(id);
            if (chkSeller == null)
            {
                return NotFound();
            }

            _context.ChkSellers.Remove(chkSeller);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChkSellerExists(int id)
        {
            return _context.ChkSellers.Any(e => e.SellerId == id);
        }
    }
}
