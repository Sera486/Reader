using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using FB2Library;
using System.Xml.Xsl;
using Reader.DataBase;
using Reader.Models;

namespace Reader.Classes
{
    public class Fb2Processor:FileProcessor
    {
        public Fb2Processor(string savePath):base(savePath)
        {
            _defaultExtension = ".fb2";
        }

        public override async Task<Book> SaveAsync(Stream stream)
        {
            var hash = GetHash(stream);
            
            stream.Position = 0;
            var path = await SaveFile(stream,hash);
            stream.Position = 0;
            var htmlPath = SaveToHtml(stream, path);
            stream.Position = 0;
            var fb2File = await ReadFB2FileStreamAsync(stream);
            var genres = fb2File.TitleInfo.Genres;
            var authors = fb2File.TitleInfo.BookAuthors;
            Book book = new Book {Title = fb2File.TitleInfo.BookTitle.Text, Hash = hash, FileURL =$@"\{htmlPath}"};
            foreach (var g in genres)
            {
                var genre = new Genre {Name = g.Genre};
                book.Genres.Add(new BookGenre{Book = book,Genre = genre});
            }
            foreach (var a in authors)
            {
                var author = new Author{Name = a.FirstName.Text,Surname = a.LastName.Text};
                book.Authors.Add(new BookAuthor{Book = book,Author = author});
            }

            foreach (var image in fb2File.Images)
            {
                try
                {
                    SaveImage(image.Value.BinaryData, image.Key, path);
                    if (image.Key.Contains("cover")) book.Cover = $@"\{path}\images\{image.Key}";
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error during saving images"+e);
                }
            }

            return book;
        }

        private string SaveToHtml(Stream stream, string path)
        {
            path = Path.Combine(path, "book.html");

            using (var fileStream = new FileStream(Path.Combine(_webRoot,path), FileMode.Create))
            {
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(Path.Combine(_webRoot, "converters", "fb2_to_xhtml.xsl"));
                xslt.Transform(XmlReader.Create(stream), null,fileStream);
            }
            return path;
        }

        private async Task<FB2File> ReadFB2FileStreamAsync(Stream stream)
        {
            // setup
            var readerSettings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Ignore
            };
            var loadSettings = new XmlLoadSettings(readerSettings);

            FB2File file = null;
            try
            {
                // reading
                file = await new FB2Reader().ReadAsync(stream, loadSettings);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Error loading file : {0}", ex.Message));
            }

            return file;
        }

        private void SaveImage(byte[] img,string name,string folderPath)
        {
            Directory.CreateDirectory(Path.Combine(_webRoot,folderPath, "images"));
            var path = Path.Combine(_webRoot,folderPath, "images", name);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                using (var bw = new BinaryWriter(fileStream))
                {
                    bw.Write(img);
                }
            }
        }
    }
}
