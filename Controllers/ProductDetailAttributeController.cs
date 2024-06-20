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
    public class ProductDetailAttributeController : ControllerBase
    {
        private readonly CheckDbContext _context;

        public ProductDetailAttributeController(CheckDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductDetailAttribute
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChkProductDetailAttribute>>> GetChkProductDetailAttributes()
        {
            return await _context.ChkProductDetailAttributes.ToListAsync();
        }

        // GET: api/ProductDetailAttribute/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChkProductDetailAttribute>> GetChkProductDetailAttribute(int id)
        {
            var chkProductDetailAttribute = await _context.ChkProductDetailAttributes.FindAsync(id);

            if (chkProductDetailAttribute == null)
            {
                return NotFound();
            }

            return chkProductDetailAttribute;
        }

        // GET: api/ProductDetailAttribute/GetByProductDetailId/5
        [HttpGet("GetByProductDetailId/{productDetailId}")]
        public async Task<ActionResult<IEnumerable<ChkProductDetailAttribute>>> GetChkProductDetailAttributesByProductDetailId(int productDetailId)
        {
            var productDetailAttributes = await _context.ChkProductDetailAttributes.Where(pda => pda.ProductDetailId == productDetailId).ToListAsync();

            if(productDetailAttributes == null)
            {
                return NotFound();
            }

            return productDetailAttributes;
        }

        // PUT: api/ProductDetailAttribute/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChkProductDetailAttribute(int id, ChkProductDetailAttribute chkProductDetailAttribute)
        {
            if (id != chkProductDetailAttribute.ProductDetailAttributeId)
            {
                return BadRequest();
            }

            _context.Entry(chkProductDetailAttribute).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChkProductDetailAttributeExists(id))
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

        // POST: api/ProductDetailAttribute
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChkProductDetailAttribute>> PostChkProductDetailAttribute(ChkProductDetailAttribute chkProductDetailAttribute)
        {
            _context.ChkProductDetailAttributes.Add(chkProductDetailAttribute);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChkProductDetailAttribute", new { id = chkProductDetailAttribute.ProductDetailAttributeId }, chkProductDetailAttribute);
        }

        // DELETE: api/ProductDetailAttribute/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChkProductDetailAttribute(int id)
        {
            var chkProductDetailAttribute = await _context.ChkProductDetailAttributes.FindAsync(id);
            if (chkProductDetailAttribute == null)
            {
                return NotFound();
            }

            _context.ChkProductDetailAttributes.Remove(chkProductDetailAttribute);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChkProductDetailAttributeExists(int id)
        {
            return _context.ChkProductDetailAttributes.Any(e => e.ProductDetailAttributeId == id);
        }
    }
}
