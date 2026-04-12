namespace TaskManagerApi.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    // Navigation: One Category → Many Tasks
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
