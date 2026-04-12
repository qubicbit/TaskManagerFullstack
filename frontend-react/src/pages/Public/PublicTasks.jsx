// File: src/pages/Public/PublicTasks.jsx

import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import taskService from "@/api/tasks";
import TaskCard from "@/components/tasks/TaskCard";
import Navbar from "@/components/layout/Navbar"; // ← NYTT
import "./PublicTasks.css";

export default function PublicTasks() {
    const [tasks, setTasks] = useState([]);
    const [page, setPage] = useState(1);

    // Swipe state
    const [touchStart, setTouchStart] = useState(null);
    const [touchEnd, setTouchEnd] = useState(null);
    const minSwipeDistance = 50;

    useEffect(() => {
        loadAllPublicTasks();
    }, []);

    async function loadAllPublicTasks() {
        try {
            const data = await taskService.getPublicTasks();
            setTasks(data);
        } catch (err) {
            console.error("Could not load public tasks:", err);
        }
    }

    // Swipe handlers
    function onTouchStart(e) {
        setTouchEnd(null);
        setTouchStart(e.targetTouches[0].clientX);
    }

    function onTouchMove(e) {
        setTouchEnd(e.targetTouches[0].clientX);
    }

    function onTouchEnd() {
        if (!touchStart || !touchEnd) return;

        const distance = touchStart - touchEnd;

        if (distance > minSwipeDistance && page < tasks.length) {
            setPage(page + 1);
        }

        if (distance < -minSwipeDistance && page > 1) {
            setPage(page - 1);
        }
    }

    return (
        <>
            <Navbar /> {/* ← HEADER FIX */}

            <div className="public-tasks-container">
                <h1 className="public-title">Browse Tasks</h1>

                <div
                    className="slider-wrapper"
                    onTouchStart={onTouchStart}
                    onTouchMove={onTouchMove}
                    onTouchEnd={onTouchEnd}
                >
                    <div
                        className="slider-track"
                        style={{ transform: `translateX(-${(page - 1) * 100}%)` }}
                    >
                        {tasks.map((task) => (
                            <div key={task.id} className="slider-card">
                                <Link to={`/tasks/${task.id}`} className="public-task-card">
                                    <TaskCard task={task} />
                                </Link>
                            </div>
                        ))}
                    </div>
                </div>

                <div className="slider-controls">
                    <button disabled={page === 1} onClick={() => setPage(page - 1)}>
                        ←
                    </button>

                    <span>{page} / {tasks.length}</span>

                    <button disabled={page === tasks.length} onClick={() => setPage(page + 1)}>
                        →
                    </button>
                </div>
            </div>
        </>
    );
}
