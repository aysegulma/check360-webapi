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
    public class CategoryController : ControllerBase
    {
        private readonly CheckDbContext _context;

        public CategoryController(CheckDbContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChkCategory>>> GetChkCategories()
        {
            return await _context.ChkCategories.ToListAsync();
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChkCategory>> GetChkCategory(int id)
        {
            var chkCategory = await _context.ChkCategories.FindAsync(id);

            if (chkCategory == null)
            {
                return NotFound();
            }

            return chkCategory;
        }

        // GET: api/Category/GetByParentId/5
        [HttpGet("GetByParentId/{id}")]
        public async Task<ActionResult<IEnumerable<ChkCategory>>> GetChkCategoryByParentId(int id)
        {
            var childCategoryList = await _context.ChkCategories.Where(category => category.ParentCategoryId == id).ToListAsync();

            if (childCategoryList == null)
            {
                return NotFound();
            }

            return childCategoryList;
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChkCategory(int id, ChkCategory chkCategory)
        {
            if (id != chkCategory.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(chkCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChkCategoryExists(id))
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

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChkCategory>> PostChkCategory(ChkCategory chkCategory)
        {
            _context.ChkCategories.Add(chkCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChkCategory", new { id = chkCategory.CategoryId }, chkCategory);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChkCategory(int id)
        {
            var chkCategory = await _context.ChkCategories.FindAsync(id);
            if (chkCategory == null)
            {
                return NotFound();
            }

            _context.ChkCategories.Remove(chkCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChkCategoryExists(int id)
        {
            return _context.ChkCategories.Any(e => e.CategoryId == id);
        }
    }
}
