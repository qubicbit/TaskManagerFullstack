// src/router/CaseNormalizer.jsx
import { Navigate, Outlet, useLocation } from "react-router-dom";

export default function CaseNormalizer() {
    const location = useLocation();
    const lower = location.pathname.toLowerCase();

    if (location.pathname !== lower) {
        return <Navigate to={lower} replace />;
    }

    return <Outlet />;
}
