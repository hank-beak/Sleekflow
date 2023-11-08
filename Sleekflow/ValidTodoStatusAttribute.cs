using Sleekflow.Models;
using System.ComponentModel.DataAnnotations;

namespace Sleekflow
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class ValidTodoStatusAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is string statusString)
			{
				TodoStatus result;
				if (Enum.TryParse(statusString, true, out result))
				{
					return ValidationResult.Success;
				}
			}

			return new ValidationResult("Invalid todo status.");
		}
	}
}
