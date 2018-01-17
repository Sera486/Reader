using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Reader.DataBase;
using Reader.Models;

namespace Reader.Classes
{
    public abstract class FileProcessor
    {
        protected readonly ApplicationDbContext _context;
        protected readonly string _webRoot;
        protected string _defaultExtension=".txt";

        protected FileProcessor(ApplicationDbContext context, string savePath)
        {
            _defaultExtension = ".txt";
            _context = context;
            _webRoot =  savePath;
        }

        public virtual async Task<int> SaveAsync(Stream stream)
        {
            var hash = GetHash(stream);
            int id = GetBookIdByHash(hash);
            if (id != 0) return id;

            stream.Position = 0;
            await SaveFile(stream, hash);
            stream.Position = 0;
            var book = new Book {Hash = hash};
            
            _context.Books.Add(book);
            _context.SaveChanges();
            return book.Id;
        }

        public string GetHash(Stream bytes)
        {
            using (var md5 = MD5.Create())
            {
                return BitConverter.ToString(md5.ComputeHash(bytes)).Replace("-", "");
            }
        }

        protected async Task<string> SaveFile(Stream stream, string hash)
        {
            var relativePath = Path.Combine("books" ,hash);
            Directory.CreateDirectory(Path.Combine(_webRoot,relativePath));
            var path = Path.Combine(_webRoot,relativePath, "book"+_defaultExtension);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await stream.CopyToAsync(fileStream);
            }
            return relativePath;
        }

        protected int GetBookIdByHash(string hash)
        {
            var book= _context.Books.FirstOrDefault(b => b.Hash == hash);
            return book?.Id ?? 0;
        }
    }
}
