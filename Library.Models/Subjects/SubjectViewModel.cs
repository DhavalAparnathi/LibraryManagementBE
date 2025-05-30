namespace Library.Models.Subjects
{
    public class SubjectViewModel
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
    }

    public class SubjectList
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortColumn { get; set; } = string.Empty;
        public string SortDirection { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
    }

    public class SubjectListViewModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortColumn { get; set; } = string.Empty;
        public string SortDirection { get; set; } = string.Empty;
        public SubjectListFilterViewModel Filters { get; set; } = new SubjectListFilterViewModel();
    }

    public class SubjectListFilterViewModel
    {
        public int DepartmentId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
    }
}
