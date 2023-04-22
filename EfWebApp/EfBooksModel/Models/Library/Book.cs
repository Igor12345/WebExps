namespace EfBooksModel.Models.Library;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int Year { get; set; } 
    public List<Author> Authors { get; } = null!;
}