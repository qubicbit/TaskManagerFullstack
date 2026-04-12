//src/pages/Admin/Dashboard.jsx

import { useEffect, useState } from "react";
import adminService from "@/api/admin";
import "./Dashboard.css";

export default function Dashboard() {
    const [stats, setStats] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        loadDashboard();
    }, []);

    async function loadDashboard() {
        try {
            const data = await adminService.adminGetDashboard();
            setStats(data);
        } catch (err) {
            console.error("Failed to load dashboard:", err);
        } finally {
            setLoading(false);
        }
    }

    if (loading) return <p>Loading admin dashboard...</p>;
    if (!stats) return <p>Could not load dashboard data.</p>;

    return (
        <div className="admin-dashboard">
            <h1>Admin Dashboard</h1>

            <div className="dashboard-grid">
                <div className="dashboard-card">
                    <h3>Total Users</h3>
                    <p>{stats.totalUsers}</p>
                </div>

                <div className="dashboard-card">
                    <h3>Active Users</h3>
                    <p>{stats.activeUsers}</p>
                </div>

                <div className="dashboard-card">
                    <h3>Total Tasks</h3>
                    <p>{stats.totalTasks}</p>
                </div>

                <div className="dashboard-card">
                    <h3>Completed Tasks</h3>
                    <p>{stats.completedTasks}</p>
                </div>

                <div className="dashboard-card">
                    <h3>Public Tasks</h3>
                    <p>{stats.publicTasks}</p>
                </div>

                <div className="dashboard-card">
                    <h3>Total Comments</h3>
                    <p>{stats.totalComments}</p>
                </div>
            </div>
        </div>
    );
}
