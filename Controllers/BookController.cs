using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Utility;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagement.Controllers;

[Authorize(Roles = "Admin")]
public class BookController : Controller
{
    private readonly ILogger<BookController> _logger;
    private readonly ApplicationDbContext _dbContext;

    public BookController(ILogger<BookController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AddBook()
    {
        return View();
    }

    public async Task<IActionResult> Books()
    {
        List<BookEntries> booksObj = await _dbContext.Books.ToListAsync();
        return View(booksObj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteBook(int? id)
    {
        if (id == null)
        {
            return Json(new { success = false, message = "Invalid ID." });
        }

        var book = await _dbContext.Books.FindAsync(id);

        if (book == null)
        {
            return Json(new { success = false, message = "Item not found." });
        }

        _dbContext.Books.Remove(book);
        await _dbContext.SaveChangesAsync();

        return Json(new { success = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddBook(BookEntries obj)
    {
        bool bookTitleExists = await _dbContext.Books.AnyAsync(book => book.BookTitle == obj.BookTitle);

        if (bookTitleExists)
        {
            ModelState.AddModelError("BookTitle", "* A book with this title already exists.");
        }

        if (ModelState.IsValid)
        {
            obj.BookTitle = StringHelper.CleanInput(obj.BookTitle);
            obj.BookPublisher = StringHelper.CleanInput(obj.BookPublisher);
            obj.BookAuthor = StringHelper.CleanInput(obj.BookAuthor);
            obj.BookISBN = obj.BookISBN.Replace("-", "");

            await _dbContext.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Books");
        }

        return View(obj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GetBook(int? id)
    {
        if (id == null)
        {
            return Json(new { success = false, message = "Invalid ID." });
        }

        var book = await _dbContext.Books.FindAsync(id);

        if (book == null)
        {
            return Json(new { success = false, message = "Item not found." });
        }

        return Json(new {
            success = true,
            data = new
            {
                bookId = book.Id,
                title = book.BookTitle,
                isbn = book.BookISBN,
                author = book.BookAuthor,
                genre = book.BookGenre,
                publisher = book.BookPublisher,
                publishedDate = book.BookPublishedDate,
                status = book.BookStatus
            }
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateBook(BookEntries obj)
    {
        if (obj == null)
        {
            return Json(new { success = false, message = "Invalid data" });
        }

        var book = await _dbContext.Books.FindAsync(obj.Id);

        if (book == null)
        {
            return Json(new { success = false, message = "Book not found" });
        }

        book.BookTitle = obj.BookTitle;
        book.BookISBN = obj.BookISBN;
        book.BookAuthor = obj.BookAuthor;
        book.BookPublisher = obj.BookPublisher;
        book.BookGenre = obj.BookGenre;
        book.BookStatus = obj.BookStatus;
        book.BookPublishedDate = obj.BookPublishedDate;

        await _dbContext.SaveChangesAsync();

        return Json(new { success = true, message = "Book updated successfully." });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}