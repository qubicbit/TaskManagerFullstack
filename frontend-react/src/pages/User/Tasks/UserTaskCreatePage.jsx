// File: src/pages/User/Tasks/UserTaskCreatePage.jsx

import { useNavigate } from "react-router-dom";
import taskService from "@/api/tasks";
import TaskForm from "@/components/tasks/TaskForm";
import "./UserTaskCreatePage.css";

export default function UserTaskCreatePage() {
    const navigate = useNavigate();

    async function handleCreate(dto) {
        const created = await taskService.createTask(dto);
        navigate(`/my-tasks/${created.id}`);
    }

    return (
        <div className="task-create-page">
            <h1>Create Task</h1>
            <TaskForm onSubmit={handleCreate} />
        </div>
    );
}
