namespace Library.Models
{
    public class PaginationModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
    }
}
