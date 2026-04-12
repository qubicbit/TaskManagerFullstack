// File: src/components/layout/Sidebar.jsx

import { NavLink } from "react-router-dom";
import "./Sidebar.css";

export default function Sidebar({ roles }) {
    return (
        <aside className="sidebar">
            <nav className="sidebar-nav">

                <h3 className="sidebar-title">Navigation</h3>

                <NavLink to="/home" className="sidebar-link">
                    Home
                </NavLink>

                <NavLink to="/my-tasks" className="sidebar-link">
                    Tasks
                </NavLink>

                <NavLink to="/profile" className="sidebar-link">
                    Profile
                </NavLink>

                {roles?.includes("Admin") && (
                    <>
                        <h3 className="sidebar-title">Admin</h3>

                        <NavLink to="/admin/dashboard" className="sidebar-link">
                            Dashboard
                        </NavLink>

                        <NavLink to="/admin/users" className="sidebar-link">
                            Users
                        </NavLink>

                        <NavLink to="/admin/tasks" className="sidebar-link">
                            Tasks
                        </NavLink>

                        <NavLink to="/admin/comments" className="sidebar-link">
                            Comments
                        </NavLink>
                    </>
                )}

            </nav>
        </aside>
    );
}
