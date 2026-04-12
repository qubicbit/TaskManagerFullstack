// File: src/components/tasks/TaskItem.jsx

import { useState } from "react";
import ConfirmModal from "@/components/ui/ConfirmModal";
import EditModal from "@/components/ui/EditModal";
import "./TaskItem.css";

export default function TaskItem({ task, onDelete, onSave }) {
    const [showDelete, setShowDelete] = useState(false);
    const [showEdit, setShowEdit] = useState(false);

    return (
        <div className="task-item">
            <div className="task-item-header">
                <h3 className="task-item-title">{task.title}</h3>

                <div className="task-item-actions">
                    <button
                        className="task-item-edit"
                        onClick={() => setShowEdit(true)}
                    >
                        Edit
                    </button>

                    <button
                        className="task-item-delete"
                        onClick={() => setShowDelete(true)}
                    >
                        Delete
                    </button>
                </div>
            </div>

            <p className="task-item-desc">
                {task.description?.length > 140
                    ? task.description.substring(0, 140) + "..."
                    : task.description}
            </p>

            {showEdit && (
                <EditModal
                    title="Edit task"
                    initialValue={task.title}
                    onSave={(value) => {
                        onSave(task.id, value);
                        setShowEdit(false);
                    }}
                    onCancel={() => setShowEdit(false)}
                />
            )}

            {showDelete && (
                <ConfirmModal
                    title="Delete task?"
                    message="Are you sure you want to delete this task?"
                    onConfirm={() => {
                        onDelete(task.id);
                        setShowDelete(false);
                    }}
                    onCancel={() => setShowDelete(false)}
                />
            )}
        </div>
    );
}

