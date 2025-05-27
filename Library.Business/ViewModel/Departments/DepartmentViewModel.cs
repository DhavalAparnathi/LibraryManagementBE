namespace Library.Business.ViewModel
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? HodUserId { get; set; }
    }

    public class DepartmentListFilterViewModel
    {
        public string Name { get; set; } = string.Empty;
    }

    public class DepartmentListViewModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortColumn { get; set; } = string.Empty;
        public string SortDirection { get; set; } = string.Empty;
        public DepartmentListFilterViewModel Filters { get; set; } = new DepartmentListFilterViewModel();
    }
}
