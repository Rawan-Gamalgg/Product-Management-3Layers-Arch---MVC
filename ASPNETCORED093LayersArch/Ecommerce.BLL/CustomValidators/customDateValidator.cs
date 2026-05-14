using System.ComponentModel.DataAnnotations;

namespace Ecommerce.BLL
{
    public static class customDateValidator
    {
        public static ValidationResult ValidateNotFuture(DateTime? expiryDate, ValidationContext context)
        {
            if (expiryDate.HasValue && expiryDate.Value > DateTime.Today)
                return new ValidationResult("Expiry date cannot be in the future.");
            return ValidationResult.Success;
        }
    }
}