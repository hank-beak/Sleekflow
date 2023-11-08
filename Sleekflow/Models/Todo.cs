using System.ComponentModel.DataAnnotations;

namespace Sleekflow.Models
{
	public class Todo
	{		
		public int Id { get; set; }
        [Required]
        public string Name { get; set; }
		public string Description { get; set; }
		[DataType(DataType.DateTime)]
		[ValidTodoDueDate]
		public DateTime DueDate { get; set; }
		[ValidTodoStatus]
		public string Status { get; set; }
	}

	public enum TodoStatus
	{
		NotStarted,
		InProgress,
		Completed
	}
}
