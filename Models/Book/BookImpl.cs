
using LibraryManagement.Data;
using LibraryManagement.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models.Book
{
    public class BookImpl : IBook
    {
        private readonly ApplicationDbContext _context;
        public BookImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddBook(BookEntries bookEntry)
        {
            bool isBookTitleExists = await _context.Books.AnyAsync(book => book.BookTitle.ToLower() == bookEntry.BookTitle.ToLower());

            if (isBookTitleExists)
            {
                return false;
            }

            await _context.Books.AddAsync(bookEntry);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteBook(int? id)
        {
            var obj = await _context.Books.FindAsync(id);

            if (obj == null)
            {
                return false;
            }

            _context.Books.Remove(obj);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<BookEntries?> GetBookById(int? id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book != null)
            {
                return book;
            }

            return null;
        }

        public async Task<List<BookEntries>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<bool> UpdateBook(BookEntries updatedBookEntry)
        {
            var oldBookEntry = await _context.Books.FindAsync(updatedBookEntry.Id);

            if (oldBookEntry == null)
            {
                return false;
            }

            oldBookEntry.BookTitle = updatedBookEntry.BookTitle;
            oldBookEntry.BookISBN = updatedBookEntry.BookISBN;
            oldBookEntry.BookAuthor = updatedBookEntry.BookAuthor;
            oldBookEntry.BookPublisher = updatedBookEntry.BookPublisher;
            oldBookEntry.BookGenre = updatedBookEntry.BookGenre;
            oldBookEntry.BookStatus = updatedBookEntry.BookStatus;
            oldBookEntry.BookPublishedDate = updatedBookEntry.BookPublishedDate;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
