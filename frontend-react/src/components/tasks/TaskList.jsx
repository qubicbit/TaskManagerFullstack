// File: src/components/tasks/TaskList.jsx

import TaskItem from "./TaskItem";
import "./TaskList.css";

export default function TaskList({ tasks, onDelete, onSave }) {
    if (!tasks || tasks.length === 0) {
        return <p className="task-list-empty">No tasks found.</p>;
    }

    return (
        <div className="task-list">
            {tasks.map(task => (
                <TaskItem
                    key={task.id}
                    task={task}
                    onDelete={onDelete}
                    onSave={onSave}
                />
            ))}
        </div>
    );
}
