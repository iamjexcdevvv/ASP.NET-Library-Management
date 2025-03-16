using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public class BookManagementContext : DbContext
    {
        public BookManagementContext(DbContextOptions<BookManagementContext> options)
        : base(options)
        {
        }

        public DbSet<BookEntries> Books { get; set; }
    }
}