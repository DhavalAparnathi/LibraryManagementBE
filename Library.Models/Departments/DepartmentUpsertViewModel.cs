namespace Library.Models.Departments
{
    public class DepartmentUpsertViewModel
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? HodUserId { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
