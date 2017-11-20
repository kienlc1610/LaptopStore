using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using Microsoft.AspNetCore.Cors;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [EnableCors("MyPolicyA"), Route("api/Products")]
    public class ProductsController : Controller
    {     
        private readonly LaptopStoreContext _context;

        public ProductsController(LaptopStoreContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<List<Product>> GetProduct([FromQuery] FilterProduct filter)
        {
            var allProducts = _context.Product.AsQueryable();
            var countProduct = _context.Product.Count();

            if (filter.CateID != 0)
            {
                allProducts = allProducts.Where(m => m.CateId == filter.CateID);
            }

            if (filter.PriceTo != 0)
            {
                allProducts = allProducts.Where(m => m.Price >= filter.PriceFrom && m.Price <= filter.PriceTo);
            }
            else
            {
                allProducts = allProducts.Where(m => m.Price >= filter.PriceFrom);
            }

            if (filter.Discount != 0)
            {
                allProducts = allProducts.Where(m => m.Discount == filter.Discount);
            }

            if (filter.PageSize != 0)
            {
                allProducts = allProducts.Skip(filter.PageIndex * filter.PageSize).Take(filter.PageSize);
            }

            return await allProducts.ToListAsync();
        }
        [HttpGet]
        [Route("Count")]
        public async Task<int> CountAllProducts()
        {
            return await _context.Product.CountAsync();
        }

        [HttpGet]
        [Route("Latest")]
        public IEnumerable<Product> GetAllLatestProducts()
        {
            return _context.Product.OrderByDescending(parameter => parameter.ProductId).Take(5);
        }
        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Product.SingleOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        [HttpPost]
        [Route("Create")]
        public async Task<JsonResult> PostProductAsync([FromBody] Product product)
        {
            var prodt = _context.Product.Find(product.ProductId);
            if (prodt != null)
            {
                throw new Exception("Exist ProductId the same!");
            }
            await _context.Product.AddAsync(product);
            _context.SaveChanges();
            return Json(true);
        }


        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Product.SingleOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
        [Route("Price/Max")]
        public async Task<double> GetMaxPriceOfAllProducts()
        {
            return await _context.Product.MaxAsync(p => p.Price);
        }

        [Route("Price/Min")]
        public async Task<double> GetMinPriceOfAllProducts()
        {
            return await _context.Product.MinAsync(p => p.Price);
        }

        [HttpGet]
        [Route("Search")]
        public JsonResult SearchProducts([FromQuery]string productName)
        {
            if (!string.IsNullOrEmpty(productName))
            {
                var products = _context.Product.Where(p => p.Name.Contains(productName));
                return Json(products);
            }
            return Json(new List<Product>());
        }
    }
}