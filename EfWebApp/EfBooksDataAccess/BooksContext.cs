using EfBooksModel.Models.Library;
using Microsoft.EntityFrameworkCore;

namespace EfBooksDataAccess;

public class BooksContext: DbContext
{
    public BooksContext(DbContextOptions options):base(options)
    {
    }
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<HistoryRecord> MemberHistory { get; set; }
    public DbSet<Author> Authors { get; set; }
}