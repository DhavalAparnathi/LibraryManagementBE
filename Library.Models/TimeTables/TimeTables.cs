namespace Library.Models.TimeTables

{
    public class TimeTables
    {
        public int TimeTableId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;

    }
}
