namespace EfBooksModel.Models.Library;

public class Book
{
    private string? _allAuthors;
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int Year { get; set; } 
    public List<Author> Authors { get; } = new List<Author>();

    public string AllAuthors
    {
        get
        {
            if (!string.IsNullOrEmpty(_allAuthors))
                return _allAuthors;
            return _allAuthors = string.Join(",", Authors.Select(a => a.Name));
        }
    }
}