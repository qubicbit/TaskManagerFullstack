/**
 * fetchClient.js
 * -------------------------
 * Centraliserad wrapper runt fetch för hela frontend-appen.
 *
 * Funktion:
 * - Lägger automatiskt till baseURL för API:t
 * - Skickar med JWT-token från localStorage om den finns
 * - Sätter standardheaders (JSON)
 * - Hanterar alla API-svar på ett ställe
 * - Returnerar JSON-data eller kastar ett fel med tydligt meddelande
 *
 * Detta är grunden för alla API-anrop i projektet.
 */

const API_URL = "https://localhost:7238/api";

export async function apiFetch(endpoint, options = {}) {
    const token = localStorage.getItem("token");

    const headers = {
        "Content-Type": "application/json",
        ...(token ? { Authorization: `Bearer ${token}` } : {}),
        ...options.headers,
    };

    const response = await fetch(`${API_URL}${endpoint}`, {
        ...options,
        headers,
    });

    // 🔥 DELETE/PUT kan returnera 204 eller tom body → returnera null
    if (response.status === 204) return null;

    // 🔥 Läs rå text först (för att undvika JSON-parse error)
    const raw = await response.text();

    // 🔥 Om tom body → returnera null
    if (!raw) {
        if (!response.ok) {
            throw new Error(`HTTP error ${response.status}`);
        }
        return null;
    }

    let data;
    try {
        data = JSON.parse(raw);
    } catch {
        throw new Error("Invalid JSON response from server");
    }

    // 🔥 Hantera API-fel
    if (!response.ok) {
        const message = data?.message || data?.error || "API error";
        throw new Error(message);
    }

    return data;
}