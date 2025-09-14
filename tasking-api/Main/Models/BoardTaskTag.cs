namespace tasking_api.Main.Models
{
    public class BoardTaskTag
    {
        public required Guid IdTask_Tag { get; set; }
        public required Guid TaskParent_Id { get; set; }
        public required string Tag_Value { get; set; }
    }
}
