namespace tasking_api.Main.Models.DTO
{
    public class CalendarDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TimeZone { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public string Provider { get; set; } = string.Empty;
    }
}