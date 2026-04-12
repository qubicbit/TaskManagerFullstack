//src/api/auth.js
/**
 * auth.js
 * -------------------------
 * API-anrop för autentisering och användarprofil.
 *
 * Funktion:
 * - Skickar login-data till backend och tar emot JWT-token
 * - Hämtar inloggad användares profil (inkl. roll)
 *
 * Varför?
 * - Håller all auth-relaterad kommunikation samlad på ett ställe
 * - Används av AuthContext för login, auto-login och profilhämtning
 *
 * Användning:
 *   loginRequest(email, password)
 *   getProfile()
 *
 * Dessa funktioner används INTE direkt i UI,
 * utan via AuthContext som sköter state + token-hantering.
 */

import { apiFetch } from "@/utils/fetchClient";

export function loginRequest(email, password) {
    return apiFetch("/auth/login", {
        method: "POST",
        body: JSON.stringify({ email, password }),
    });
}

export function getProfile() {
    return apiFetch("/users/me");
}

