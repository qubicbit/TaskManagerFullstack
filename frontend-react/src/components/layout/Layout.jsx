// File: src/components/layout/Layout.jsx

import { Outlet } from "react-router-dom";
import Navbar from "./Navbar";
import Sidebar from "./Sidebar";
import { useAuth } from "@/context/AuthContext"; // <-- för att få user.role
import "./Layout.css";

export default function Layout() {
    const { user } = useAuth(); // { id, name, role }

    console.log("LAYOUT RENDER", user);

    return (
        <div className="app-layout">
            <Navbar />

            <div className="layout-body">
                <Sidebar roles={user?.roles} />

                <main className="layout-content">
                    <Outlet />
                </main>
            </div>

            <footer className="layout-footer">
                <p>© {new Date().getFullYear()} Task Manager</p>
            </footer>
        </div>
    );
}
