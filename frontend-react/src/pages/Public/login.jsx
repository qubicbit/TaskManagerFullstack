// src/pages/Public/Login.jsx

import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { useAuth } from "@/context/AuthContext";
import Navbar from "@/components/layout/Navbar";
import "./Login.css";

export default function Login() {
    const navigate = useNavigate();
    const { login } = useAuth();

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState(null);

    async function handleSubmit(e) {
        e.preventDefault();
        setError(null);

        try {
            await login(email, password);
            navigate("/profile");
        } catch (err) {
            setError(err.message || "Login failed");
        }
    }

    return (
        <>
            <Navbar />

            <div className="login-container">
                <h1>Login</h1>

                <form onSubmit={handleSubmit} className="login-form">
                    <input
                        type="text"
                        placeholder="Email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />

                    <input
                        type="password"
                        placeholder="Password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />

                    {error && <p className="login-error">{error}</p>}

                    <button type="submit">Login</button>
                </form>

                <p className="login-register-text">
                    Don’t have an account?{" "}
                    <Link to="/register" className="login-register-link">
                        Create one
                    </Link>
                </p>
            </div>
        </>
    );
}
