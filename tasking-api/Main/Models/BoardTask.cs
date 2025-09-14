namespace tasking_api.Main.Models
{
    public class BoardTask
    {
        public Guid Id { get; set; }
        public Guid BoardId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? Deadline { get; set; }
        public BoardTaskStatus TaskStatus { get; set; } = BoardTaskStatus.NotStarted;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public List<BoardTaskTag> Tags { get; set; } = new List<BoardTaskTag>();
    }
}
