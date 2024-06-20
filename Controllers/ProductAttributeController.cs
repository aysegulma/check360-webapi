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
    public class ProductAttributeController : ControllerBase
    {
        private readonly CheckDbContext _context;

        public ProductAttributeController(CheckDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductAttribute
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChkProductAttribute>>> GetChkProductAttributes()
        {
            return await _context.ChkProductAttributes.ToListAsync();
        }

        // GET: api/ProductAttribute/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChkProductAttribute>> GetChkProductAttribute(int id)
        {
            var chkProductAttribute = await _context.ChkProductAttributes.FindAsync(id);

            if (chkProductAttribute == null)
            {
                return NotFound();
            }

            return chkProductAttribute;
        }

        // GET: api/ProductAttribute/GetByProductId/5
        [HttpGet("GetByProductId/{productId}")]
        public async Task<ActionResult<IEnumerable<ChkProductAttribute>>> GetChkProductAttributesByProductId(int productId)
        {
            var productAttributes = await _context.ChkProductAttributes.Where(pa => pa.ProductId == productId).ToListAsync();

            if(productAttributes == null)
            {
                return NotFound();
            }

            return productAttributes;
        }

        // PUT: api/ProductAttribute/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChkProductAttribute(int id, ChkProductAttribute chkProductAttribute)
        {
            if (id != chkProductAttribute.ProductAttributeId)
            {
                return BadRequest();
            }

            _context.Entry(chkProductAttribute).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChkProductAttributeExists(id))
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

        // POST: api/ProductAttribute
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChkProductAttribute>> PostChkProductAttribute(ChkProductAttribute chkProductAttribute)
        {
            _context.ChkProductAttributes.Add(chkProductAttribute);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ChkProductAttributeExists(chkProductAttribute.ProductAttributeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetChkProductAttribute", new { id = chkProductAttribute.ProductAttributeId }, chkProductAttribute);
        }

        // DELETE: api/ProductAttribute/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChkProductAttribute(int id)
        {
            var chkProductAttribute = await _context.ChkProductAttributes.FindAsync(id);
            if (chkProductAttribute == null)
            {
                return NotFound();
            }

            _context.ChkProductAttributes.Remove(chkProductAttribute);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChkProductAttributeExists(int id)
        {
            return _context.ChkProductAttributes.Any(e => e.ProductAttributeId == id);
        }
    }
}
