//src/components/tasks/TaskCard.jsx
import "./TaskCard.css";

export default function TaskCard({ task, onClick }) {
    const {
        title,
        description,
        createdAt,
        deadline,
        isCompleted,
        categoryName,
        tags
    } = task;

    // Normalisera tags (string[] eller TagDto[])
    const normalizedTags = tags?.map(t =>
        typeof t === "string" ? t : t.name
    );

    const deadlineDate = deadline ? new Date(deadline) : null;
    const now = new Date();

    // Deadline färgkodning
    let deadlineClass = "";
    if (deadlineDate) {
        const diffDays = (deadlineDate - now) / (1000 * 60 * 60 * 24);
        if (deadlineDate < now) deadlineClass = "deadline-overdue";
        else if (diffDays < 3) deadlineClass = "deadline-soon";
        else deadlineClass = "deadline-far";
    }

    return (
        <div className="task-card" onClick={onClick}>
            <h3>{title}</h3>

            <p className="task-card-desc">
                {description?.length > 120
                    ? description.substring(0, 120) + "..."
                    : description}
            </p>

            <div className="task-card-meta">
                <span className={isCompleted ? "status-completed" : "status-pending"}>
                    Status: {isCompleted ? "Completed" : "Pending"}
                </span>

                <span>
                    Created: {new Date(createdAt).toLocaleDateString("sv-SE")}
                </span>
            </div>

            <div className={`task-card-deadline ${deadlineClass}`}>
                <strong>Deadline:</strong>{" "}
                {deadlineDate
                    ? deadlineDate.toLocaleDateString("sv-SE")
                    : "None"}
            </div>

            {categoryName && (
                <div className="task-card-category">
                    <strong>Category:</strong> {categoryName}
                </div>
            )}

            {normalizedTags?.length > 0 && (
                <div className="task-card-tags">
                    {normalizedTags.map((tag, i) => (
                        <span key={i} className="tag">{tag}</span>
                    ))}
                </div>
            )}
        </div>
    );
}
