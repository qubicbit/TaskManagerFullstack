// File: src/components/layout/Navbar.jsx

import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "@/context/AuthContext";
import ThemeToggle from "./ThemeToggle";
import "./Navbar.css";

export default function Navbar() {
    const { user, logout } = useAuth();
    const navigate = useNavigate();

    function handleLogout() {
        logout();
        navigate("/home");
    }

    return (
        <nav className="navbar">
            <div className="navbar-left">
                <Link to="/home" className="navbar-logo">
                    Task Manager
                </Link>
            </div>

            <div className="navbar-right">
                <ThemeToggle />

                {!user && (
                    <>
                        <Link to="/login" className="navbar-link">
                            Sign in
                        </Link>
                        <Link to="/register" className="navbar-button">
                            Get Started
                        </Link>
                    </>
                )}

                {user && (
                    <>
                        <Link to="/profile" className="navbar-link">
                            {user.email}
                        </Link>

                        <button className="navbar-logout" onClick={handleLogout}>
                            Sign out
                        </button>
                    </>
                )}
            </div>
        </nav>
    );
}
