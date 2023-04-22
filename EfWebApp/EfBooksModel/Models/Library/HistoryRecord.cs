namespace EfBooksModel.Models.Library;

public class HistoryRecord
{
    public int Id { get; set; }
    public int MemberId { get; set; }
    public DateOnly Created { get; set; }
    public int ParentId { get; set; }
    //todo figure out the better choice, Id or entity
    public List<Book> Books { get; set; } = null!;
}