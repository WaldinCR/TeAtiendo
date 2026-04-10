using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TeAtiendo.Desktop.Helpers
{
    public static class ValidationHelper
    {
        public static bool TryValidate(object model, out string message)
        {
            var ctx = new ValidationContext(model);
            var results = new List<ValidationResult>();

            var ok = Validator.TryValidateObject(model, ctx, results, true);
            if (ok)
            {
                message = "";
                return true;
            }

            message = string.Join("\n", results.Select(r => r.ErrorMessage).Where(m => !string.IsNullOrWhiteSpace(m)));
            return false;
        }
    }
}