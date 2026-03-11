using System.ComponentModel.DataAnnotations;

namespace TeAtiendo.Domain.Validation
{
    public static class ValidationHelper
    {
        public static (bool IsValid, List<string> Errors) ValidateEntity<T>(T entity)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(entity!);
            bool isValid = Validator.TryValidateObject(entity!, context, results, true);

            var errors = new List<string>();
            foreach (var error in results)
                errors.Add(error.ErrorMessage ?? "Error de validación");

            return (isValid, errors);
        }
    }
}