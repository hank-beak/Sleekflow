using Sleekflow.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sleekflow.Models
{
	public class Todo
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
