using Microsoft.AspNetCore.Identity;

namespace TaskManagerApi.Models;

// ApplicationUser är din IdentityUser + extra fält + navigationer
public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    // Navigation: One User → Many Tasks
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

    // Navigation: One User → Many Comments
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}

