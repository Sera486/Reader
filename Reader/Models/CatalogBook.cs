namespace Reader.Models
{
    public class CatalogBook
    {
        public int BookId { get; set; }
        public int CatalogId { get; set; }
        public Book Book { get; set; }
        public Catalog Catalog { get; set; } 
    }
}
