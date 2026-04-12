// src/components/tasks/TaskDetails.jsx
import "./TaskDetails.css";

export default function TaskDetails({ task }) {
    const {
        title,
        description,
        isCompleted,
        createdAt,
        deadline,
        categoryName,
        tags
    } = task;

    const normalizedTags = tags?.map(t =>
        typeof t === "string" ? t : t.name
    );

    const deadlineDate = deadline ? new Date(deadline) : null;
    const now = new Date();

    let deadlineClass = "";
    if (deadlineDate) {
        if (deadlineDate < now) {
            deadlineClass = "deadline-overdue";
        } else if ((deadlineDate - now) / (1000 * 60 * 60 * 24) < 3) {
            deadlineClass = "deadline-soon";
        } else {
            deadlineClass = "deadline-far";
        }
    }

    return (
        <div className="task-detail">
            <h2 className="task-detail-title">{title}</h2>

            {/* ✔ DESCRIPTION FÖRST */}
            <p className="task-detail-description">{description}</p>

            {/* ✔ STATUS EFTER DESCRIPTION */}
            <div className="task-detail-status">
                <span className={isCompleted ? "status-completed" : "status-pending"}>
                    {isCompleted ? "Completed" : "Pending"}
                </span>
            </div>

            <div className="task-detail-meta">
                <p><strong>Created:</strong> {new Date(createdAt).toLocaleDateString("sv-SE")}</p>

                <p className={deadlineClass}>
                    <strong>Deadline:</strong>{" "}
                    {deadlineDate
                        ? deadlineDate.toLocaleDateString("sv-SE")
                        : "None"}
                </p>

                {categoryName && (
                    <p><strong>Category:</strong> {categoryName}</p>
                )}
            </div>

            {normalizedTags?.length > 0 && (
                <div className="task-detail-tags">
                    <strong>Tags:</strong>
                    <div className="tag-list">
                        {normalizedTags.map((tag, i) => (
                            <span key={i} className="tag">{tag}</span>
                        ))}
                    </div>
                </div>
            )}
        </div>
    );
}
