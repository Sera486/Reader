using System.Collections.Generic;

namespace Reader.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public string FileURL { get; set; }
        public string Hash { get; set; }
        public ApplicationUser Uploader { get; set; }
        public List<BookAuthor> Authors { get; set; } = new List<BookAuthor>();
        public List<BookGenre> Genres { get; set; } = new List<BookGenre>();
    }
}
