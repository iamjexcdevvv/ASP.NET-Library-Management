using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Validator
{
    public class DateNotInTheFuture : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? objValue,
                                                   ValidationContext validationContext)
        {
            if (objValue != null)
            {
                DateTime dateValue = Convert.ToDateTime(objValue);

                if (dateValue.Date.Year > DateTime.Now.Year)
                {
                    return new ValidationResult("* Invalid date");
                }
            }

            return ValidationResult.Success;
        }
    }
}
