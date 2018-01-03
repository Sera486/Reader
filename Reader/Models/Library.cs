using System.Collections.Generic;

namespace Reader.Models
{
    public class Library
    {
        public int Id { get; set; }
        public string Name { get; set; }//возможно юзлесс
        public ApplicationUser User { get; set; }
        public List<Book> Books { get; set; }=new List<Book>();
    }
}
