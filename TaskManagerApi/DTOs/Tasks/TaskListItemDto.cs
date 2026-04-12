namespace TaskManagerApi.DTOs.Tasks
{
    public class TaskListItemDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime? Deadline { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CategoryName { get; set; }

        public List<string> Tags { get; set; } = new();
    }
}
