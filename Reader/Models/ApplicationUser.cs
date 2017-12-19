using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Reader.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageURL { get; set; }
        public List<Book> Books { get; set; }

        public string FullName => $"{LastName} {FirstName}";
    }
}
