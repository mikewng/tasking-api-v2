namespace tasking_api.Main.Models.DTO.Request
{
    public class CreateCalendarEventRequest
    {
        public string CalendarId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; } = string.Empty;
        public List<string> Attendees { get; set; } = new();
        public bool IsAllDay { get; set; }
    }
}