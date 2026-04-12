namespace TaskManagerApi.DTOs.Tasks
{
    public class TaskReadDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Deadline { get; set; }

        public string? CategoryName { get; set; }

        public List<TagDto> Tags { get; set; } = new();
        public List<TaskCommentDto> Comments { get; set; } = new();
    }
}
