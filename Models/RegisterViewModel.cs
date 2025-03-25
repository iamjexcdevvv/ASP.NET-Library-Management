using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class RegisterViewModel : IAccountViewModel
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

        [Required(ErrorMessage = "You must agree with the terms and condition")]
        public bool TermsAndConditionAgreement { get; set; }
    }
}
