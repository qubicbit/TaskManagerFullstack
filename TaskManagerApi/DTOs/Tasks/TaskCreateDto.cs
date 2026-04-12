namespace TaskManagerApi.DTOs.Tasks
{
    public class TaskCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DateTime? Deadline { get; set; }

        // FIX: CategoryId ska vara required → inte nullable
        public int CategoryId { get; set; }

        // FIX: Alltid en lista, aldrig null
        public List<int> TagIds { get; set; } = new();
    }
}
