namespace TaskManagerApi.Models;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    // Navigation: Many Tags ↔ Many Tasks (via TaskTag)
    public ICollection<TaskTag> TaskTags { get; set; } = new List<TaskTag>();
}
