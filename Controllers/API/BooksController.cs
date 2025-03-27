using System.Threading.Tasks;
using LibraryManagement.Data;
using LibraryManagement.Models.Book;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public BooksController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookEntries>>> GetBooks()
        {
            return await _dbContext.Books.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookEntries>> GetBook(int id)
        {
            var bookEntry = await _dbContext.Books.FindAsync(id);

            if (bookEntry == null)
            {
                return NotFound();
            }

            return Ok(bookEntry);
        }

        [HttpPost]
        public async Task<ActionResult<BookEntries>> AddBook(BookEntries newBook)
        {
            if (newBook == null)
            {
                return BadRequest();
            }

            await _dbContext.Books.AddAsync(newBook);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookEntries newBook)
        {
            var bookEntry = await _dbContext.Books.FindAsync(id);

            if (bookEntry == null)
            {
                return NotFound();
            }

            bookEntry.BookTitle = newBook.BookTitle;
            bookEntry.BookISBN = newBook.BookISBN;
            bookEntry.BookAuthor = newBook.BookAuthor;
            bookEntry.BookPublisher = newBook.BookPublisher;
            bookEntry.BookGenre = newBook.BookGenre;
            bookEntry.BookStatus = newBook.BookStatus;
            bookEntry.BookPublishedDate = newBook.BookPublishedDate;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var bookEntry = await _dbContext.Books.FindAsync(id);

            if (bookEntry == null)
            {
                return NotFound();
            }

            _dbContext.Books.Remove(bookEntry);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
