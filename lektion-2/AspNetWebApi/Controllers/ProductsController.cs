using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetWebApi;
using AspNetWebApi.Models.Entities;
using AspNetWebApi.Models;

namespace AspNetWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SqlContext _context;

        public ProductsController(SqlContext context)
        {
            _context = context;
        }





        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {
            var items = new List<ProductModel>();
            foreach (var p in await _context.Products.Include(x => x.Category).ToListAsync())
                items.Add(new ProductModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryName = p.Category.CategoryName
                });

            return items;
        }








        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProductEntity(int id)
        {
            var productEntity = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);

            if (productEntity == null)
            {
                return NotFound();
            }

            return new ProductModel
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Description = productEntity.Description,
                Price = productEntity.Price,
                CategoryName = productEntity.Category.CategoryName
            };
        }












        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductEntity(int id, ProductUpdateModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var productEntity = await _context.Products.FindAsync(id);
            productEntity.Name = model.Name;
            productEntity.Description = model.Description;  
            productEntity.Price = model.Price;
            productEntity.CategoryId = model.CategoryId;

            _context.Entry(productEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductEntityExists(id))
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductEntity>> PostProductEntity(ProductCreateModel model)
        {
            var productEntity = new ProductEntity();

            var categoryEntity = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == model.CategoryName);  
            if (categoryEntity != null)
            {
                productEntity = new ProductEntity
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    CategoryId = categoryEntity.Id
                };
                _context.Products.Add(productEntity);
            }
            else
            {
                productEntity = new ProductEntity
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Category = new CategoryEntity { CategoryName = model.CategoryName }
                };
                _context.Products.Add(productEntity);
            }

            await _context.SaveChangesAsync();
            var p = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == productEntity.Id);

            return CreatedAtAction("GetProductEntity", new { id = productEntity.Id }, new ProductModel
            {
                Id = p.Id,
                Name = p.Name,
                Description= p.Description,
                Price= p.Price,
                CategoryName = p.Category.CategoryName
            });
        }














        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductEntity(int id)
        {
            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }















        private bool ProductEntityExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
