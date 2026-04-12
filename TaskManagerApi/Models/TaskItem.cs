namespace TaskManagerApi.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? Deadline { get; set; }

    // FK → ApplicationUser (nullable för public tasks)
    public string? UserId { get; set; }

    // Navigation → ApplicationUser
    public ApplicationUser? User { get; set; } // En TaskItem kan ha en User (om det är en privat task), annars null (om det är en public task)

    // FK → Category
    public int? CategoryId { get; set; }
    public Category? Category { get; set; } // Navigation, en TaskItem kan ha en Category

    // Many-to-many → Tags
    public ICollection<TaskTag> TaskTags { get; set; } = new List<TaskTag>();

    // One-to-many → Comments
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
