using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Reader.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string ImageURL { get; set; }
        public List<Book> Books { get; set; }=new List<Book>();
        public Library Library { get; set; }
    }
}
