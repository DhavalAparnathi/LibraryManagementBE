using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.TimeTables
{
    public class UpsertTimeTableViewModel
    {
        public int TimeTableId { get; set; } 
        public int DepartmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class UpsertTimeTableRequest
    {
        public int TimeTableId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CreatedBy { get; set; }
    }

    public class TimeTableRequest
    {
        public int TimeTableId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public List<TimeTableSlotRequest> Slots { get; set; } = new List<TimeTableSlotRequest>();
    }

    public class TimeTableSlotRequest
    {
        public int SlotId { get; set; }
        public int DayId { get; set; }
        public int Period { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int AssistantTeacherId { get; set; }
    }
}
