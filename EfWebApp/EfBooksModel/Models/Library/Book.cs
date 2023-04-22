namespace EfBooksModel.Models.Library;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Author> Authors { get; } = null!;

}