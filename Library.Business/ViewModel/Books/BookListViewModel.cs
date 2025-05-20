namespace Library.Business.ViewModel
{
    public class BookListViewModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
        public BookFilterViewModel Filters { get; set; } = new BookFilterViewModel();
    }

    public class BookFilterViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
    }
}
