using EfBooksModel.Models.Library;
using Microsoft.EntityFrameworkCore;

namespace EfBooksDataAccess;

public class BooksContext: DbContext
{
    // public BooksContext(DbContextOptions options):base(options)
    // {
    // }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=EfBooks;Integrated Security=true;");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().Property(nameof(Book.Title)).HasMaxLength(300);
        modelBuilder.Entity<Author>().Property(nameof(Author.Name)).HasMaxLength(50);
        modelBuilder.Entity<Member>().Property(nameof(Member.Name)).HasMaxLength(50);
        modelBuilder.Entity<Member>().Property(nameof(Member.MemberId)).HasMaxLength(50);
    }
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<HistoryRecord> MemberHistory { get; set; }
    public DbSet<Author> Authors { get; set; }
}