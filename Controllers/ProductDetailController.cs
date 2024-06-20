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
    public class ProductDetailController : ControllerBase
    {
        private readonly CheckDbContext _context;

        public ProductDetailController(CheckDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChkProductDetail>>> GetChkProductDetails()
        {
            return await _context.ChkProductDetails.ToListAsync();
        }

        // GET: api/ProductDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChkProductDetail>> GetChkProductDetail(int id)
        {
            var chkProductDetail = await _context.ChkProductDetails.FindAsync(id);

            if (chkProductDetail == null)
            {
                return NotFound();
            }

            return chkProductDetail;
        }

        // GET: api/ProductDetail/GetByProductId/5
        [HttpGet("GetByProductId/{productId}")]
        public async Task<ActionResult<IEnumerable<ChkProductDetail>>> GetChkProductDetailByProductId(int productId)
        {
            var chkProductDetailList = await _context.ChkProductDetails
                                                            .Where(pd=>pd.ProductId == productId)
                                                            .OrderBy(pd=>pd.Price).ToListAsync();

            if (chkProductDetailList == null)
            {
                return NotFound();
            }

            return chkProductDetailList;
        }

        // GET: api/ProductDetail/GetByFilter?productId=1&freeShipping=true&freeReturn=false
        [HttpGet("GetByFilter")]
        public async Task<ActionResult<IEnumerable<ChkProductDetail>>> GetChkProductDetailByFilter(int productId, bool freeShipping, bool freeReturn)
        {
            var chkProductDetailList = new List<ChkProductDetail>();
            if(freeShipping)
            {
                chkProductDetailList = await _context.ChkProductDetails
                                                            .Where(pd=>pd.ProductId == productId)
                                                            .Where(pd=>pd.ShippingPrice == 0)
                                                            .OrderBy(pd=>pd.Price).ToListAsync();
            }
            else if(freeReturn)
            {
                chkProductDetailList = await _context.ChkProductDetails
                                                            .Where(pd=>pd.ProductId == productId)
                                                            .Where(pd=>pd.IsReturnChargeable == false)
                                                            .OrderBy(pd=>pd.Price).ToListAsync();
            }
            else{
                chkProductDetailList = await _context.ChkProductDetails
                                                            .Where(pd=>pd.ProductId == productId)
                                                            .OrderBy(pd=>pd.Price).ToListAsync();
            }

            if (chkProductDetailList == null)
            {
                return NotFound();
            }

            return chkProductDetailList;
        }

        // GET: api/ProductDetail/GetMinPriceByFilter?..
        [HttpGet("GetMinPriceByFilter")]
        public async Task<ActionResult<ChkProductDetail>> GetMinPriceItemByFilter(int productId, bool freeShipping, bool freeReturn)
        {
            var chkProductDetail = new ChkProductDetail();
            if(freeShipping)
            {
                chkProductDetail = await _context.ChkProductDetails
                                                        .Where(pd=>pd.ProductId == productId)
                                                        .Where(pd=>pd.ShippingPrice == 0)
                                                        .OrderBy(pd => pd.Price).FirstOrDefaultAsync();
            }
            else if(freeReturn)
            {
                chkProductDetail = await _context.ChkProductDetails
                                                        .Where(pd=>pd.ProductId == productId)
                                                        .Where(pd=>pd.IsReturnChargeable == false)
                                                        .OrderBy(pd => pd.Price).FirstOrDefaultAsync();
            }
            else
            {
                chkProductDetail = await _context.ChkProductDetails
                                                        .Where(pd=>pd.ProductId == productId)
                                                        .OrderBy(pd => pd.Price).FirstOrDefaultAsync();
            }

            if (chkProductDetail == null)
            {
                return NotFound();
            }

            return chkProductDetail;
        }

        // PUT: api/ProductDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChkProductDetail(int id, ChkProductDetail chkProductDetail)
        {
            if (id != chkProductDetail.ProductDetailId)
            {
                return BadRequest();
            }

            _context.Entry(chkProductDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChkProductDetailExists(id))
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

        // POST: api/ProductDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChkProductDetail>> PostChkProductDetail(ChkProductDetail chkProductDetail)
        {
            _context.ChkProductDetails.Add(chkProductDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChkProductDetail", new { id = chkProductDetail.ProductDetailId }, chkProductDetail);
        }

        // DELETE: api/ProductDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChkProductDetail(int id)
        {
            var chkProductDetail = await _context.ChkProductDetails.FindAsync(id);
            if (chkProductDetail == null)
            {
                return NotFound();
            }

            _context.ChkProductDetails.Remove(chkProductDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChkProductDetailExists(int id)
        {
            return _context.ChkProductDetails.Any(e => e.ProductDetailId == id);
        }
    }
}
