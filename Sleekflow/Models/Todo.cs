using System.ComponentModel.DataAnnotations;

namespace Sleekflow.Models
{
	public class Todo: IValidatableObject
	{
		
		public int Id { get; set; }
        [Required]
        public string Name { get; set; }
		public string Description { get; set; }
		[DataType(DataType.DateTime)]
		public DateTime DueDate { get; set; }
		public TodoStatus Status { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if(DueDate < DateTime.Now.Date)
			{
				yield return new ValidationResult("Due Date cannot be in the past");
			}
		}
	}

	public enum TodoStatus
	{
		NotStarted,
		InProgress,
		Completed
	}
}
