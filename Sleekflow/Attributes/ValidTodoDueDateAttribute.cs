using System.ComponentModel.DataAnnotations;

namespace Sleekflow.Attributes
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class ValidTodoDueDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var httpContext = validationContext.GetRequiredService<IHttpContextAccessor>().HttpContext;

            if (httpContext.Request.Method.Equals("PUT", StringComparison.OrdinalIgnoreCase))
            {
                return ValidationResult.Success;
            }

            if (value is DateTime dueDate && dueDate < DateTime.Now.Date)
            {
                return new ValidationResult("Due Date cannot be in the past.");
            }

            return ValidationResult.Success;
        }
    }
}
