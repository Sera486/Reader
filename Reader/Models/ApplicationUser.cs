using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Reader.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageURL { get; set; }
        public List<Book> Books { get; set; }

        public string FullName => $"{LastName} {FirstName}";
    }
}
