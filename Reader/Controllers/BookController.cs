using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reader.Classes;
using Reader.DataBase;
using Reader.Models;

namespace Reader.Controllers
{
    [Produces("application/json")]
    [Route("api/Book")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        
        public BookController(ApplicationDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: api/Book
        [HttpGet]
        public IEnumerable<Book> GetBook()
        {
            return _context.Books;
        }

        // GET: api/Book/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _context.Books.SingleOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
        
        // POST: api/Book
        [HttpPost("addBook")]
        public async Task<IActionResult> AddBook(IFormFile uploadedFile)
        {
            if (uploadedFile == null) return BadRequest("No data was specified");
            try
            {
                var processor = new Fb2Processor(_context, _appEnvironment.WebRootPath);
                var id=await processor.SaveAsync(uploadedFile.OpenReadStream());
                var book=_context.Books.Find(id);
                return CreatedAtAction("GetBook",id,book);
            }
            catch (Exception e)
            {
                
                return BadRequest(e);
            }
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
