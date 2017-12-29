using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reader.Models;

namespace Reader.DataBase
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Genre> Genres { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(u =>
            {
                u.HasMany(b => b.Books).WithOne(e => e.Uploader);
                u.HasOne(l => l.Library).WithOne(l => l.User);
            });
            builder.Entity<Library>(l =>
            {
                l.HasMany(b => b.Books);
                l.HasOne(u => u.User).WithOne(lib => lib.Library);
            });

            //configure many-to-many relationship
            builder.Entity<BookAuthor>()
                .HasKey(b => new { b.BookId, b.AuthorId });
            builder.Entity<BookGenre>()
                .HasKey(b => new { b.BookId, b.GenreId });
        } 
    }
}
