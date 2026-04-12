namespace TaskManagerApi.Models;

public class Comment
{
    //En kommentar tillhör en Task och en User(två 1‑många relationer)
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // FK → Task
    public int TaskId { get; set; } //FK för att koppla Comment till Task
    public TaskItem Task { get; set; } = null!; //navigationsproperty för att komma åt Task från Comment

    // FK → User
    public string UserId { get; set; } = null!; //FK för att koppla Comment till User.
    public ApplicationUser User { get; set; } = null!; //navigationsproperty för att komma åt User från Comment
}
