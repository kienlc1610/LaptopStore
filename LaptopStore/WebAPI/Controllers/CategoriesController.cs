using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using Microsoft.AspNetCore.Cors;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Categories")]
    [EnableCors("MyPolicyA")]
    public class CategoriesController : Controller
    {
        private readonly LaptopStoreContext _context;

        public CategoriesController(LaptopStoreContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public IEnumerable<Category> GetCategory()
        {
            return _context.Category;
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.Category.SingleOrDefaultAsync(m => m.CateId == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        [Route("Update")]
        public JsonResult Update(Category category)
        {
            var cate = _context.Category.Find(category.CateId);
            if (cate==null)
            {
                throw new Exception("Can not update this Category");
            }
            else
            {
                cate.Name = category.Name;
                cate.Alias = category.Alias;
                _context.Entry(cate).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return Json(true);
        }

       

        // POST: api/Categories
        [HttpPost]
        [Route("Create")]
        public JsonResult PostCategory( Category category)
        {
            var cate = _context.Category.Find(category.CateId);
            if (cate!=null)
            {
                throw new Exception("Exist CateId the same!");
            }
            _context.Category.Add(category);
            _context.SaveChanges();
            return Json(true);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.Category.SingleOrDefaultAsync(m => m.CateId == id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.CateId == id);
        }
    }
}