namespace Library.Models.TImeTableSlots

{
    public class TimeTableSlots
    {
        public int SlotId { get; set; }
        public int TimeTableId { get; set; } 
        public int DayId { get; set; } 
        public int Period { get; set; } 
        public int SubjectId { get; set; } 
        public int TeacherId { get; set; } 
        public int AssistantTeacherId { get; set; } 
    }
}
