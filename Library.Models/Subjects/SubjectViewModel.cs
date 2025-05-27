namespace Library.Models.Subjects
{
    public class SubjectViewModel
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
    }
}
