namespace TaskManagerApi.DTOs.Tasks
{
    public class TaskUpdateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }

        public DateTime? Deadline { get; set; }

        public bool? IsCompleted { get; set; }

        public int? CategoryId { get; set; }

        public List<int>? TagIds { get; set; }
    }
}
