using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models.Auth
{
    public interface IAccount
    {
        string Email { get; set; }
        string Password { get; set; }
    }
}
