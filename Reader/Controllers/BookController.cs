using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _appEnvironment;
        
        public BookController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,IHostingEnvironment appEnvironment)
        {
            _context = context;
            _userManager = userManager;
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
                Book book = BookByHash(GetHash(uploadedFile.OpenReadStream()));
                if (book==null)
                {
                    var processor = new Fb2Processor(_appEnvironment.WebRootPath);
                    book = await processor.SaveAsync(uploadedFile.OpenReadStream());
                }
                
                if (HttpContext.User != null)//заливающий человек залогинен
                {
                    var user =await _userManager.GetUserAsync(HttpContext.User);
                    if (book.Uploader == null) book.Uploader = user;
                }

                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetBook",book.Id,book);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        public string GetHash(Stream bytes)
        {
            using (var md5 = MD5.Create())
            {
                return BitConverter.ToString(md5.ComputeHash(bytes)).Replace("-", "");
            }
        }


        private Book BookByHash(string hash)
        {
            return _context.Books.FirstOrDefault(b => b.Hash == hash);
        }
    }
}
