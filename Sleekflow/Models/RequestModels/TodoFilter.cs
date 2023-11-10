using Sleekflow.Models.DTOs;

namespace Sleekflow.Models.RequestModels
{
    public class TodoFilter
    {
        public TodoStatus? Status { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
