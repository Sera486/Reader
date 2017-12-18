using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reader.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string FileURL { get; set; }
        public ApplicationUser Uploader { get; set; }
    }
}
