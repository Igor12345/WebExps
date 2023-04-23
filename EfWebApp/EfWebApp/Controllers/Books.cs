using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EfBooksDataAccess;
using EfBooksModel.Models.Library;
using EfWebApp.Models;

namespace EfWebApp.Controllers
{
    public class Books : Controller
    {
        private readonly BooksContext _context;

        public Books(BooksContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return _context.Books != null
                ? View((await _context.Books.ToListAsync()).Select(b => new BookViewModel(b)))
                : Problem("Entity set 'BooksContext.Books'  is null.");
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            BookViewModel viewModel = new BookViewModel(book);

            return View(viewModel);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Year")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            
            if (book == null)
            {
                return NotFound();
            }

            List<SelectListItem> authors = _context.Authors.Select(a => 
                new SelectListItem 
                {
                    Value = a.Id.ToString(),
                    Text =  a.Name
                }).ToList();
            BookViewModel viewModel = new BookViewModel(book) { AllAuthors = authors };
            
            return View(viewModel);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Year, Authors")] CreateBookModel book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            Book? originBook = null;
            if (ModelState.IsValid)
            {
                try
                {
                    originBook = await _context.Books.FindAsync(id);
                    originBook.Title = book.Title;
                    originBook.Year = book.Year;

                    if (book.Authors.Any())
                    {
                        var author = await _context.Authors.FindAsync(book.Authors.First());
                        if (author != null && originBook.Authors.All(a => a.Id != author.Id))
                            originBook.Authors.Add(author);
                    } 
                    
                    _context.Update(originBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            List<SelectListItem> allAuthors = _context.Authors.Select(a => 
                new SelectListItem 
                {
                    Value = a.Id.ToString(),
                    Text =  a.Name
                }).ToList();
            BookViewModel viewModel = new BookViewModel(originBook) { AllAuthors = allAuthors };
            return View(viewModel);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'BooksContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
