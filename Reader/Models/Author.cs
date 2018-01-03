using System.Collections.Generic;

namespace Reader.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<BookAuthor> PublishedBooks { get; set; } = new List<BookAuthor>();
    }
}
