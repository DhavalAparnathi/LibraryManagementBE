namespace Library.Models.Attendance

{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
        public string FilledBy { get; set; } = string.Empty;
        public string ApprovedBy {  get; set; } = string.Empty;
        public string Reason {  get; set; } = string.Empty;
    }
}
