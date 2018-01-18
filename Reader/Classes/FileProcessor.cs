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
        protected readonly string _webRoot;
        protected string _defaultExtension=".txt";

        protected FileProcessor( string savePath)
        {
            _defaultExtension = ".txt";
            _webRoot =  savePath;
        }

        public virtual async Task<Book> SaveAsync(Stream stream)
        {
            var hash = GetHash(stream);
            
            stream.Position = 0;
            var path = await SaveFile(stream, hash);
            stream.Position = 0;
            Book book = new Book {Hash = hash, FileURL = path};

            return book;
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
    }
}
