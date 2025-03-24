using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Validator
{
    public class UserMustAgree : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? objValue,
                                                   ValidationContext validationContext)
        {
            bool? value = objValue as bool? ?? null;

            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value != null)
            {
                if (value == false)
                {
                    return new ValidationResult("You must agree with the terms and condition");
                }
            }

            return ValidationResult.Success;
        }
    }
}
