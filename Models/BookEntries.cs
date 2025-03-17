using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class BookEntries
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field should not be empty")]
        public string BookTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field should not be empty")]
        public string BookISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field should not be empty")]
        public string BookAuthor { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field should not be empty")]
        public string BookGenre { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field should not be empty")]
        public string BookPublisher { get; set; } = string.Empty;

        public DateTime BookPublishedDate { get; set; } = DateTime.Now;

        public bool BookStatus { get; set; }
    }
}
