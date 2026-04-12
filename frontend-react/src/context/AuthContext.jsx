//src/context/AuthContext.jsx

/**
 * AuthContext.jsx
 * -------------------------
 * Globalt autentiserings-state för hela applikationen.
 *
 * Funktion:
 * - Håller reda på inloggad användare (user)
 * - Lagrar och läser JWT-token från localStorage
 * - Auto-login: hämtar profil om token finns
 * - login(): loggar in och hämtar profil
 * - logout(): rensar token och användardata
 *
 * Varför?
 * - Gör auth tillgängligt överallt i appen via useAuth()
 * - Skyddar routes (ProtectedRoute, AdminRoute)
 * - Gör det enkelt att visa/hide UI baserat på roll
 *
 * Användning:
 *   const { user, login, logout } = useAuth()
 *
 * Detta är fundamentet för hela auth-systemet.
 */

import { createContext, useContext, useEffect, useState } from "react";
import { loginRequest, getProfile } from "@/api/auth";

const AuthContext = createContext();

export function AuthProvider({ children }) {
    const [user, setUser] = useState(null);
    const [token, setToken] = useState(localStorage.getItem("token") || null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        async function loadUser() {
            if (!token) {
                setLoading(false);
                return;
            }

            try {
                const profile = await getProfile();
                setUser(profile);
            } catch {
                setToken(null);
                localStorage.removeItem("token");
            }

            setLoading(false);
        }

        loadUser();
    }, [token]);

    async function login(email, password) {
        const result = await loginRequest(email, password);

        setToken(result.token);
        localStorage.setItem("token", result.token);

        const profile = await getProfile();
        setUser(profile);
    }

    function logout() {
        setUser(null);
        setToken(null);
        localStorage.removeItem("token");
    }

    return (
        <AuthContext.Provider value={{ user, token, loading, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
}

export function useAuth() {
    return useContext(AuthContext);
}
