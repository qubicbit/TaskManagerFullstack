import { useTheme } from "@/context/ThemeContext";
import "./ThemeToggle.css";

export default function ThemeToggle() {
    const { theme, toggleTheme } = useTheme();

    return (
        <button
            onClick={toggleTheme}
            className={`theme-toggle-btn ${theme}`}
        >
            {theme === "light" ? "🌙" : "☀️"}
        </button>
    );
}
