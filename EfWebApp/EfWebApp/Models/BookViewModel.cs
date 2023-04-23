using EfBooksModel.Models.Library;
using EfWebApp.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EfWebApp.Models;

public class BookViewModel
{
    private string? _allAuthors;
    private readonly Book _book;

    public BookViewModel(Book book)
    {
        _book = book;
    }
    
    public Book Book => _book;

    public string AuthorNames
    {
        get
        {
            if (!string.IsNullOrEmpty(_allAuthors))
                return _allAuthors;
            return _allAuthors = string.Join(",", _book.Authors.Select(a => a.Name));
        }
    }

    public List<SelectListItem> AllAuthors { get; set; } = new List<SelectListItem>();
}