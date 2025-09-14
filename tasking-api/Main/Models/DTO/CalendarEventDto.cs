namespace tasking_api.Main.Models.DTO
{
    public class CalendarEventDto
    {
        public string Id { get; set; } = string.Empty;
        public string CalendarId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; } = string.Empty;
        public List<string> Attendees { get; set; } = new();
        public bool IsAllDay { get; set; }
        public string Provider { get; set; } = string.Empty;
    }
}