using LibraryManagement.Models.Book;

namespace LibraryManagement.Services
{
    public interface IBook {
        Task<bool> AddBook(BookEntries bookEntry);
        Task<int> GetBooksCount();
        Task<bool> DeleteBook(int? id);
        Task<bool> UpdateBook(BookEntries updatedBookEntry);
        Task<BookEntries?> GetBookById(int? id);
        Task<List<BookEntries>> GetBooks();
    }
}
