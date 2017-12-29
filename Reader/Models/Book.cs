using System.Collections.Generic;

namespace Reader.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileURL { get; set; }
        public ApplicationUser Uploader { get; set; }

        public List<BookAuthor> Authors { get; set; }
        public List<BookGenre> Genres { get; set; }
    }
}
