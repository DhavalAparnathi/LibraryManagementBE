namespace Library.Models.Departments
{
    public class DepartmentList
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; } = string.Empty;
        public string SortDirection { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
    }

}
