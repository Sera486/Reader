using System.Collections.Generic;

namespace Reader.Models
{
    public class Catalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ApplicationUser User { get; set; }
        public List<CatalogBook> CatalogBooks { get; set; }=new List<CatalogBook>();
    }
}
