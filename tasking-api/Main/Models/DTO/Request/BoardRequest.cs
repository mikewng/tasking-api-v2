namespace tasking_api.Main.Models.DTO.Request
{
    public class BoardRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
