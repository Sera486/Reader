using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reader.Models
{
    public class Catalog
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Catalog name must be specified")]
        [StringLength(30)]
        public string Name { get; set; }
        public ApplicationUser User { get; set; }
        public List<CatalogBook> CatalogBooks { get; set; }=new List<CatalogBook>();
    }
}
