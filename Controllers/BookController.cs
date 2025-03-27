using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Utility;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using LibraryManagement.Models.Book;
using LibraryManagement.Services;

namespace LibraryManagement.Controllers;

[Authorize(Roles = "Admin")]
public class BookController : Controller
{
    private readonly ILogger<BookController> _logger;
    private readonly IBook _bookRepository;

    public BookController(ILogger<BookController> logger, IBook bookRepository)
    {
        _logger = logger;
        _bookRepository = bookRepository;
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
        List<BookEntries> booksObj = await _bookRepository.GetBooks();
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

        bool result = await _bookRepository.DeleteBook(id);

        if (!result)
        {
            return Json(new { success = false, message = "Item not found." });
        }

        return Json(new { success = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddBook(BookEntries obj)
    {
        BookEntries cleanObj = new BookEntries
        {
            BookAuthor = StringHelper.CleanInput(obj.BookAuthor),
            BookPublisher = StringHelper.CleanInput(obj.BookPublisher),
            BookTitle = StringHelper.CleanInput(obj.BookTitle),
            BookISBN = obj.BookISBN.Replace("-", ""),
            BookGenre = obj.BookGenre,
            BookPublishedDate = obj.BookPublishedDate,
            BookStatus = obj.BookStatus
        };

        bool result = await _bookRepository.AddBook(cleanObj);

        if (!result)
        {
            ModelState.AddModelError("BookTitle", "* A book with this title already exists.");
        }

        if (ModelState.IsValid)
        {
            return RedirectToAction("Books");
        }

        return View(obj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GetBookById(int? id)
    {
        if (id == null)
        {
            return BadRequest();
        }

        BookEntries? book = await _bookRepository.GetBookById(id);

        if (book == null)
        {
            return NotFound();
        }

        return PartialView("_EditBookPartial", book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateBook(BookEntries obj)
    {
        if (obj == null)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return PartialView("_EditBookPartial", obj);
        }

        bool result = await _bookRepository.UpdateBook(obj);

        if (!result)
        {
            return NotFound();
        }

        return RedirectToAction("Books", "Book");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}