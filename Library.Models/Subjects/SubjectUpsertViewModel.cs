namespace Library.Business.ViewModel
{
    public class SubjectUpsertViewModel
    {
        public int SubjectId { get; set; }

        public string SubjectName { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        public string Year { get; set; } = string.Empty;
    }
}
