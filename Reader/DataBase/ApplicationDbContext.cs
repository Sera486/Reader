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
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Genre> Genres { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(u =>
            {
                u.HasMany(b => b.Books).WithOne(e => e.Uploader);
                u.HasMany(l => l.Catalogs).WithOne(l => l.User);
            });
            builder.Entity<Catalog>(l =>
            {
                l.HasMany(b => b.CatalogBooks).WithOne(c=>c.Catalog);
            });

            //configure many-to-many relationship
            builder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });
            builder.Entity<BookGenre>()
                .HasKey(bg => new { bg.BookId, bg.GenreId });
            builder.Entity<CatalogBook>()
                .HasKey(cb => new { cb.BookId, cb.CatalogId });
        } 
    }
}
