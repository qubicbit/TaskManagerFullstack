

// TaskTag är Join‑modell (ska ALDRIG ut till frontend)
// Den representerar den många-till-många-relationen mellan TaskItem och Tag
// ska aldrig ska ha en DTO.
// Det är bara en relationstabell mellan, se properties nedan, den har inga egna properties utöver FK och navigation properties
// ingen rikting entitet, det är bara en relationstabell.

namespace TaskManagerApi.Models;

public class TaskTag
{
    // FK → Task
    public int TaskId { get; set; }
    public TaskItem Task { get; set; } = null!; // Navigation, en TaskTag måste ha en Task

    // FK → Tag
    public int TagId { get; set; }
    public Tag Tag { get; set; } = null!; // Navigation, en TaskTag måste ha en Tag
}
