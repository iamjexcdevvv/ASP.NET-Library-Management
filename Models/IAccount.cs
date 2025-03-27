using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public interface IAccount
    {
        string Email { get; set; }
        string Password { get; set; }
    }
}
