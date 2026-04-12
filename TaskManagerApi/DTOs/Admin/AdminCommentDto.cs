namespace TaskManagerApi.DTOs.Admin
{
    public class AdminCommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int TaskId { get; set; }
        public string TaskTitle { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
