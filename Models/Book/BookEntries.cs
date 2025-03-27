using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagement.Validator;

namespace LibraryManagement.Models.Book
{
    public class BookEntries
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "* Book title is required"), RegularExpression(@"^(?!\s*$).+", ErrorMessage = "* Book title is required")]
        public string BookTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "* Book isbn number is required"), RegularExpression(@"^(?:ISBN(?:-1[03])?:?●)?(?=[0-9X]{10}$|(?=(?:[0-9]+[-●]){3})[-●0-9X]{13}$|97[89][0-9]{10}$|(?=(?:[0-9]+[-●]){4})[-●0-9]{17}$)(?:97[89][-●]?)?[0-9]{1,5}[-●]?[0-9]+[-●]?[0-9]+[-●]?[0-9X]$", ErrorMessage = "Invalid ISBN number")]
        public string BookISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "* Book author name is required"), RegularExpression(@"^(?!\s*$).+", ErrorMessage = "* Book author name is required")]
        public string BookAuthor { get; set; } = string.Empty;

        [Required(ErrorMessage = "* Book genre is required")]
        public string BookGenre { get; set; } = string.Empty;

        [Required(ErrorMessage = "* Book publisher is required"), RegularExpression(@"^(?!\s*$).+", ErrorMessage = "* Book publisher is required")]
        public string BookPublisher { get; set; } = string.Empty;

        [Required(ErrorMessage = "* Book published date is required")]
        [DateNotInTheFuture]
        [Column(TypeName = "date")]
        public DateTime BookPublishedDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "* Book availability status is required")]
        public bool BookStatus { get; set; }
    }
}
