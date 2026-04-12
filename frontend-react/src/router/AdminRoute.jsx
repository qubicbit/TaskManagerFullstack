// src/router/AdminRoute.jsx
import { Navigate } from "react-router-dom";
import { useAuth } from "@/context/AuthContext";

export default function AdminRoute({ children }) {
    const { user, loading } = useAuth();

    if (loading) return <div>Laddar...</div>;
    if (!user) return <Navigate to="/login" replace />;

    const roles = user.roles || [];

    if (!roles.includes("Admin")) {
        return <Navigate to="/home" replace />;
    }

    return children;
}
