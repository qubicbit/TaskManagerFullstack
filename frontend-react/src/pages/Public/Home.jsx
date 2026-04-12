// File: src/pages/Public/Home.jsx

import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import taskService from "@/api/tasks";
import TaskCard from "@/components/tasks/TaskCard";
import Navbar from "@/components/layout/Navbar";
import "./Home.css";

export default function Home() {
    const [tasks, setTasks] = useState([]);

    useEffect(() => {
        loadPublicTasks();
    }, []);

    async function loadPublicTasks() {
        try {
            const data = await taskService.getPublicTasks();
            setTasks(data.slice(0, 3)); // Show only 3 featured tasks
        } catch (err) {
            console.error("Could not load public tasks:", err);
        }
    }

    return (
        <>
            {/* GLOBAL NAVBAR */}
            <Navbar />

            {/* PAGE CONTENT */}
            <div className="landing-container">

                {/* HERO */}
                <section className="hero">
                    <h1>Welcome to Task Manager</h1>
                    <p className="hero-subtitle">
                        Organize your life, track your tasks, and stay productive.
                    </p>

                    <Link to="/public-tasks" className="btn-primary hero-cta">
                        Browse Tasks →
                    </Link>
                </section>

                {/* FEATURES SECTION */}
                <section className="features">
                    <div className="feature-item">
                        <div className="feature-icon">📝</div>
                        <h3>Create Tasks</h3>
                        <p>Build your personal task list and stay organized.</p>
                    </div>

                    <div className="feature-item">
                        <div className="feature-icon">📊</div>
                        <h3>Track Progress</h3>
                        <p>Mark tasks as completed and follow your productivity.</p>
                    </div>

                    <div className="feature-item">
                        <div className="feature-icon">⚡</div>
                        <h3>Stay Productive</h3>
                        <p>Use categories and tags to keep everything structured.</p>
                    </div>
                </section>

                {/* CTA BOX */}
                <section className="cta-box">
                    <h2>Want to create your own tasks?</h2>
                    <p>Sign up and start building your personal task list today.</p>
                    <Link to="/register" className="cta-btn">Get Started</Link>
                </section>

                {/* FEATURED TASKS */}
                <section className="public-preview">
                    <h2>Featured Tasks</h2>

                    <div className="task-list">
                        {tasks.map((task) => (
                            <TaskCard key={task.id} task={task} />
                        ))}
                    </div>

                    <Link to="/public-tasks" className="btn-view-all">
                        Browse Tasks →
                    </Link>
                </section>

                {/* FOOTER */}
                <footer className="footer">
                    <p>© {new Date().getFullYear()} Task Manager. All rights reserved.</p>
                </footer>

            </div>
        </>
    );
}
