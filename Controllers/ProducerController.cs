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
    public class ProducerController : ControllerBase
    {
        private readonly CheckDbContext _context;

        public ProducerController(CheckDbContext context)
        {
            _context = context;
        }

        // GET: api/Producer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChkProducer>>> GetChkProducers()
        {
            return await _context.ChkProducers.ToListAsync();
        }

        // GET: api/Producer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChkProducer>> GetChkProducer(int id)
        {
            var chkProducer = await _context.ChkProducers.FindAsync(id);

            if (chkProducer == null)
            {
                return NotFound();
            }

            return chkProducer;
        }

        // PUT: api/Producer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChkProducer(int id, ChkProducer chkProducer)
        {
            if (id != chkProducer.ProducerId)
            {
                return BadRequest();
            }

            _context.Entry(chkProducer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChkProducerExists(id))
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

        // POST: api/Producer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChkProducer>> PostChkProducer(ChkProducer chkProducer)
        {
            _context.ChkProducers.Add(chkProducer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChkProducer", new { id = chkProducer.ProducerId }, chkProducer);
        }

        // DELETE: api/Producer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChkProducer(int id)
        {
            var chkProducer = await _context.ChkProducers.FindAsync(id);
            if (chkProducer == null)
            {
                return NotFound();
            }

            _context.ChkProducers.Remove(chkProducer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChkProducerExists(int id)
        {
            return _context.ChkProducers.Any(e => e.ProducerId == id);
        }
    }
}
