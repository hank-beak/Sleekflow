namespace Sleekflow.Models.RequestModels
{
    public class TodoSort
    {
        public SortBy? TodoSortBy { get; set; }
        public SortOrder? TodoSortOrder { get; set; }

        public enum SortBy
        {
            DueDate,
            Status,
            Name
        }

        public enum SortOrder
        {
            Ascending,
            Descending,
        }
    }

}
