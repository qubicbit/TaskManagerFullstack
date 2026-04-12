namespace TaskManagerApi.DTOs.Tasks
{
    public class TaskCommentDto
    {
        public int Id { get; set; }

        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public string UserFullName { get; set; } = string.Empty;
    }
}
