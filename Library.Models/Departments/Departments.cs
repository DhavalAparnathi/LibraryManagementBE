namespace Library.Models.Departments
{
    public class Departments
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int HodUserId { get; set; }
        public string HodUserName { get; set; } = string.Empty;
    }
}
