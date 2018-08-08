using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reader.Models;

namespace Reader.ViewModels
{
    public class CatalogViewModel
    {
        public CatalogViewModel(Catalog catalog)
        {
            Id = catalog.Id;
            Name = catalog.Name;
            Books = catalog.CatalogBooks.Select(cb => cb.Book);
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
