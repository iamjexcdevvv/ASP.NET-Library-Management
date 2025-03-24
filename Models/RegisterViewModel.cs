using System.ComponentModel.DataAnnotations;
using LibraryManagement.Validator;

namespace LibraryManagement.Models
{
    public class RegisterViewModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email address format")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Your password length must be between 6 to 20 characters")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [UserMustAgree]
        public bool TermsAndConditionAgreement { get; set; }
    }
}
