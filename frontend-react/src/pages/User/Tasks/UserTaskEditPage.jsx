// src/pages/User/Tasks/UserTaskEditPage.jsx

import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import taskService from "@/api/tasks";
import TaskForm from "@/components/tasks/TaskForm";
import "./UserTaskEditPage.css";

export default function UserTaskEditPage() {
    const { id } = useParams();
    const navigate = useNavigate();

    const [task, setTask] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        loadTask();
    }, [id]);

    async function loadTask() {
        const data = await taskService.getMyTaskById(id);
        setTask({
            title: data.title,
            description: data.description,
            deadline: data.deadline,
            categoryId: data.categoryId,
            tagIds: data.tags.map(t => t.id),
            isCompleted: data.isCompleted
        });
        setLoading(false);
    }

    async function handleUpdate(dto) {
        await taskService.updateTask(id, dto);
        navigate(`/my-tasks/${id}`);
    }

    if (loading) return <p>Loading...</p>;

    return (
        <div className="task-edit-page">
            <h1>Edit Task</h1>

            <TaskForm
                initialValues={task}
                onSubmit={handleUpdate}
                mode="edit"
            />
        </div>
    );
}
