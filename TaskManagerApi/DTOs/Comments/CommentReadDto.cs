namespace TaskManagerApi.DTOs.Comments
{
    public class CommentReadDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        // NYTT: används bara av frontend
        public bool IsOwner { get; set; }
    }
}
