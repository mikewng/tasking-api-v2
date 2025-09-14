namespace tasking_api.Main.Models.DTO.Request
{
    public class BoardTaskRequest
    {
        public Guid? Id { get; set; }
        public Guid BoardId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? Deadline { get; set; }
    }
}
