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
    public class ProductController : ControllerBase
    {
        private readonly CheckDbContext _context;

        public ProductController(CheckDbContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChkProduct>>> GetChkProducts()
        {
            return await _context.ChkProducts.ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChkProduct>> GetChkProduct(int id)
        {
            var chkProduct = await _context.ChkProducts.FindAsync(id);

            if (chkProduct == null)
            {
                return NotFound();
            }

            return chkProduct;
        }

        // GET: api/Product/GetByCategoryId/5
        [HttpGet("GetByCategoryId/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ChkProduct>>> GetChkProductByCategoryId(int categoryId)
        {
            var chkProductList = await _context.ChkProducts.Where(product=>product.CategoryId == categoryId).ToListAsync();

            if (chkProductList == null)
            {
                return NotFound();
            }

            return chkProductList;
        }

        // GET: api/Product/GetByFilter?categoryId=18&freeShipping=true&freeReturn=false
        [HttpGet("GetByFilter")]
        public async Task<ActionResult<IEnumerable<ChkProduct>>> GetChkProductByFilter(int categoryId, bool freeShipping, bool freeReturn)
        {
            var chkProductList = new List<ChkProduct>();
            if(freeShipping){
                chkProductList = await _context.ChkProducts.Join(_context.ChkProductDetails, 
                                                                product => product.ProductId, 
                                                                productDetail => productDetail.ProductId,
                                                                (product, productDetail) => new {Product = product, ProductDetail = productDetail})
                                                        .Where(result => result.Product.CategoryId == categoryId)
                                                        .Where(result => result.ProductDetail.ShippingPrice == 0)
                                                        .GroupBy(result => new { result.Product.ProductId, result.Product.Name, result.Product.Image} )
                                                        .Select(result => new ChkProduct(){
                                                            ProductId = result.Key.ProductId,
                                                            Name = result.Key.Name,
                                                            Image = result.Key.Image
                                                        }).ToListAsync();
            }
            else if(freeReturn){
                chkProductList = await _context.ChkProducts.Join(_context.ChkProductDetails, 
                                                                product => product.ProductId, 
                                                                productDetail => productDetail.ProductId,
                                                                (product, productDetail) => new {Product = product, ProductDetail = productDetail})
                                                        .Where(result => result.Product.CategoryId == categoryId)
                                                        .Where(result => result.ProductDetail.IsReturnChargeable == false)
                                                        .GroupBy(result => new { result.Product.ProductId, result.Product.Name, result.Product.Image} )
                                                        .Select(result => new ChkProduct(){
                                                            ProductId = result.Key.ProductId,
                                                            Name = result.Key.Name,
                                                            Image = result.Key.Image
                                                        }).ToListAsync();
                                                        
            }
            else
            {
                chkProductList = await _context.ChkProducts.Join(_context.ChkProductDetails, 
                                                                product => product.ProductId, 
                                                                productDetail => productDetail.ProductId,
                                                                (product, productDetail) => new {Product = product, ProductDetail = productDetail})
                                                        .Where(result => result.Product.CategoryId == categoryId)
                                                        .GroupBy(result => new { result.Product.ProductId, result.Product.Name, result.Product.Image} )
                                                        .Select(result => new ChkProduct(){
                                                            ProductId = result.Key.ProductId,
                                                            Name = result.Key.Name,
                                                            Image = result.Key.Image
                                                        }).ToListAsync();
            }

                                        

            if (chkProductList == null)
            {
                return NotFound();
            }

            return chkProductList;
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChkProduct(int id, ChkProduct chkProduct)
        {
            if (id != chkProduct.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(chkProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChkProductExists(id))
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

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChkProduct>> PostChkProduct(ChkProduct chkProduct)
        {
            _context.ChkProducts.Add(chkProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChkProduct", new { id = chkProduct.ProductId }, chkProduct);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChkProduct(int id)
        {
            var chkProduct = await _context.ChkProducts.FindAsync(id);
            if (chkProduct == null)
            {
                return NotFound();
            }

            _context.ChkProducts.Remove(chkProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChkProductExists(int id)
        {
            return _context.ChkProducts.Any(e => e.ProductId == id);
        }
    }
}
