// src/pages/User/Tasks/MyTasks.jsx

import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import taskService from "@/api/tasks";
import TaskCard from "@/components/tasks/TaskCard";
import "./MyTasks.css";

export default function MyTasks() {
    const [tasks, setTasks] = useState([]);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    useEffect(() => {
        loadData();
    }, []);

    async function loadData() {
        try {
            const data = await taskService.getMyTasks();
            setTasks(data);
        } finally {
            setLoading(false);
        }
    }

    if (loading) return <p>Loading...</p>;

    return (
        <div className="my-tasks">
            <button
                className="btn-edit"
                onClick={() => navigate("/my-tasks/create")}
            >
                + Create Task
            </button>

            {tasks.map(t => (
                <TaskCard
                    key={t.id}
                    task={t}
                    onClick={() => navigate(`/my-tasks/${t.id}`)}
                />
            ))}
        </div>
    );
}
