namespace EfWebApp.Models;

public class CreateBookModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int Year { get; set; } 
    public List<int> Authors { get; set; } = new();
}