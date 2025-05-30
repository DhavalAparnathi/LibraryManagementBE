using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.TImeTableSlots
{
    public class TimeTableSlotViewModel

    {
        public int SlotId { get; set; }

        public int DayId { get; set; }

        public int Period { get; set; }

        public int SubjectId { get; set; }

        public string SubjectName { get; set; } = string.Empty;

        public int TeacherId { get; set; }

        public string TeacherName { get; set; } = string.Empty;

        public int AssistantTeacherId { get; set; }

        public string AssistantTeacherName { get; set; } = string.Empty;

    }
}
