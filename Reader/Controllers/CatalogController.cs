using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reader.DataBase;
using Reader.Models;

namespace Reader.Controllers
{
    [Produces("application/json")]
    [Route("api/Catalog")]
    public class CatalogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Catalog
        [HttpGet]
        public IEnumerable<Catalog> GetCatalogs()
        {
            return _context.Catalogs;
        }

        // GET: api/Catalog/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatalog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var catalog = await _context.Catalogs.SingleOrDefaultAsync(m => m.Id == id);

            if (catalog == null)
            {
                return NotFound();
            }

            return Ok(catalog);
        }

        // PUT: api/Catalog/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalog([FromRoute] int id, [FromBody] Catalog catalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != catalog.Id)
            {
                return BadRequest();
            }

            _context.Entry(catalog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogExists(id))
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

        // POST: api/Catalog
        [HttpPost]
        public async Task<IActionResult> PostCatalog([FromBody] Catalog catalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Catalogs.Add(catalog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCatalog", new { id = catalog.Id }, catalog);
        }

        // DELETE: api/Catalog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var catalog = await _context.Catalogs.SingleOrDefaultAsync(m => m.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            _context.Catalogs.Remove(catalog);
            await _context.SaveChangesAsync();

            return Ok(catalog);
        }

        private bool CatalogExists(int id)
        {
            return _context.Catalogs.Any(e => e.Id == id);
        }
    }
}