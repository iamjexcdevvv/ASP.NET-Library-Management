using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    interface IAccountViewModel {
        string Email { get; set; }
        string Password { get; set; }
    }
}
