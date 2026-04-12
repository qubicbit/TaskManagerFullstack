/*
 * -------------------------
 * Applikationens entrypoint.

 * - Renderar hela React-appen
 * - Kopplar in globala providers:

 * - Detta är roten där hela appen startar
 * - Alla context-providers måste ligga här för att gälla globalt
 */

import { StrictMode } from "react";
import { createRoot } from "react-dom/client";

import App from "./App.jsx";
import "./styles/global.css";

import { ThemeProvider } from "@/context/ThemeContext";
import { AuthProvider } from "@/context/AuthContext";

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <ThemeProvider>
      <AuthProvider>
        <App />
      </AuthProvider>
    </ThemeProvider>
  </StrictMode>
);
