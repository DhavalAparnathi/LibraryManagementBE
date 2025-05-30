using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.TimeTables
{
    public class TimeTableViewModel
    {
    }

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

}
