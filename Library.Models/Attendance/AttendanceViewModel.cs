namespace Library.Models.Attendance
{
    public class AttendanceRequest
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
        public int FilledBy { get; set; }
        public bool IsTeacher { get; set; }
    }

    public class ApproveAttendanceRequest
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public DateTime Date { get; set; }
        public int ApprovedBy { get; set; }
    }

    public class RejectAttendanceRequest
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public DateTime Date { get; set; }
        public int RejectedBy { get; set; }
        public string Reason { get; set; } = string.Empty;   
    }
}
