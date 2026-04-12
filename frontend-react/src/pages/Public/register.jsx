/**
 * Register.jsx
 * -------------------------
 * Publik registreringssida för nya användare.
 */

import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { apiFetch } from "@/utils/fetchClient";
import Navbar from "@/components/layout/Navbar";   // ← NYTT
import "./Register.css";

export default function Register() {
    const navigate = useNavigate();

    const [fullName, setFullName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(false);

    async function handleSubmit(e) {
        e.preventDefault();
        setError(null);
        setLoading(true);

        try {
            await apiFetch("/auth/register", {
                method: "POST",
                body: JSON.stringify({ fullName, email, password }),
            });

            setLoading(false);
            navigate("/login");

        } catch (err) {
            setError(err.message || "Registration failed");
            setLoading(false);
        }
    }

    return (
        <>
            <Navbar /> {/* ← FIXAR HEADERN */}

            <div className="register-container">
                <h1>Create Account</h1>

                <form onSubmit={handleSubmit} className="register-form">
                    <input
                        type="text"
                        placeholder="Full name"
                        value={fullName}
                        onChange={(e) => setFullName(e.target.value)}
                        required
                    />

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

                    {error && <p className="register-error">{error}</p>}

                    <button type="submit" disabled={loading}>
                        {loading ? "Creating account..." : "Register"}
                    </button>
                </form>

                <p className="register-login-text">
                    Already have an account?{" "}
                    <Link to="/login" className="register-login-link">
                        Sign in
                    </Link>
                </p>
            </div>
        </>
    );
}
