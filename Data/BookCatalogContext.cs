namespace Library.Data;
using Microsoft.EntityFrameworkCore;
using Library.Models;
public class BookCatalogContext : DbContext
{
    public BookCatalogContext(DbContextOptions<BookCatalogContext> options) : base(options)
    {
        
    }

    public DbSet<Book> Books
    {
        get{return Set<Book>();}
    }
}