//src/router/ProtectedRoute.jsx
/**
 * ProtectedRoute.jsx
 * -------------------------
 * Skyddar routes som kräver att användaren är inloggad.
 *
 * Funktion:
 * - Om AuthContext fortfarande laddar → visa "Laddar..."
 * - Om ingen användare är inloggad → redirect till /login
 * - Om användaren är inloggad → visa children (den skyddade sidan)
 *
 * Exempel:
 *   <ProtectedRoute>
 *      <Profile />
 *   </ProtectedRoute>
 */
import { Navigate } from "react-router-dom";
import { useAuth } from "@/context/AuthContext";

export default function ProtectedRoute({ children }) {
    const { user, loading } = useAuth();

    if (loading) return <div>Laddar...</div>;
    if (!user) return <Navigate to="/login" replace />;

    return children;
}
