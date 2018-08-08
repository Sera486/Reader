using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reader.DataBase;
using Reader.Models;
using Reader.ViewModels;

namespace Reader.Controllers
{
    [Route("api/Catalog")]
    [Authorize]
    public class CatalogController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CatalogController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("GetCatalogs")]
        public async Task<IEnumerable<CatalogViewModel>> GetCatalogs([FromQuery] int userId)
        {
            var addedBooks = _context.Books.Include(b => b.Authors).ThenInclude(a => a.Author).Where(b => b.Uploader.Id == userId).AsNoTracking();
            var catalog = new Catalog() {Name = "Uploaded books"};
            foreach (var addedBook in addedBooks)
            {
                catalog.CatalogBooks.Add(new CatalogBook(){Book = addedBook});
            }
            var catalogs = await _context.Catalogs.Include(c => c.CatalogBooks).ThenInclude(cb => cb.Book).ThenInclude(b => b.Authors).ThenInclude(a => a.Author).Where(c => c.User.Id == userId).AsNoTracking().ToListAsync();
            catalogs.Add(catalog);
            return catalogs.OrderBy(c=>c.Id).Select(c=> new CatalogViewModel(c));
        }

        [HttpGet("GetCatalogNames")]
        public async Task<IActionResult> GetCatalogNames([FromQuery] int userId, int bookId)
        {
            var catalogNames = await _context.Catalogs.Where(c => c.User.Id == userId && (bookId==0 || c.CatalogBooks.All(cb=>cb.BookId!=bookId))).Select(c=> new{ c.Id, c.Name }).AsNoTracking().ToListAsync();
            return Ok(catalogNames);
        }

        [HttpPost("AddBookToCatalog")]
        public async Task<IActionResult> AddBookToCatalog([FromBody] AddBookViewModel addBookViewModel)
        {
            var catalog = _context.Catalogs.Include(c => c.CatalogBooks).ThenInclude(cb => cb.Book)
                .First(c => c.Id == addBookViewModel.CatalogId);
            catalog.CatalogBooks.Add(new CatalogBook(){BookId = addBookViewModel.BookId});
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("RemoveBookFromCatalog")]
        public async Task<IActionResult> RemoveBookFromCatalog([FromBody] AddBookViewModel removeBookViewModel)
        {
            var catalog = _context.Catalogs.Include(c => c.CatalogBooks).ThenInclude(cb => cb.Book)
                .First(c => c.Id == removeBookViewModel.CatalogId);
            catalog.CatalogBooks.RemoveAll(cb=>cb.BookId==removeBookViewModel.BookId);
            await _context.SaveChangesAsync();
            return Ok();
        }
        
        // GET: api/Catalog/5
        [HttpGet("GetCatalog")]
        public async Task<IActionResult> GetCatalog([FromRoute] int catalogId)
        {
            var catalog = await _context.Catalogs.Include(c => c.CatalogBooks).ThenInclude(cb => cb.Book).ThenInclude(b => b.Authors).ThenInclude(a => a.Author)
                .SingleOrDefaultAsync(m => m.Id == catalogId);

            if (catalog == null)
            {
                return NotFound();
            }

            return Ok(new CatalogViewModel(catalog));
        }

        [HttpGet("GetUploadedBooks")]
        public IActionResult GetUploadedBooks([FromRoute]int userId)
        {
            var addedBooks = _context.Books.Where(b => b.Uploader.Id == userId).Include(b => b.Authors).ThenInclude(a => a.Author).AsNoTracking();
            var catalog = new Catalog() { Name = "Uploaded books" };
            foreach (var addedBook in addedBooks)
            {
                catalog.CatalogBooks.Add(new CatalogBook() { Book = addedBook });
            }

            return Ok(new CatalogViewModel(catalog));
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
        [HttpPost("AddCatalog")]
        public async Task<IActionResult> AddCatalog([FromBody] Catalog catalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            catalog.User = await _userManager.GetUserAsync(HttpContext.User);
           

            _context.Catalogs.Add(catalog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCatalog", new { id = catalog.Id }, catalog);
        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddCatalog([FromBody] int catalogId, [FromBody] int bookId)
        {
            if (catalogId==default(int)||bookId==default(int))
            {
                return Json(new { error = $"One or more parameters is empty: catalogId={catalogId}, bookId={bookId}"});
            }

            var catalog = await _context.Catalogs.Include(c=>c.User).Include(c=>c.CatalogBooks).
                FirstOrDefaultAsync(c => c.Id == catalogId);
            var user = _userManager.GetUserAsync(HttpContext.User);

            if (catalog.User.Id != user.Id)
            {
                return Json(new { error = $"You have no permission to add new book" });
            }

            catalog.CatalogBooks.Add(new CatalogBook{BookId = bookId});

            _context.Catalogs.Update(catalog);
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
