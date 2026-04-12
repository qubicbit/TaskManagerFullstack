//src/components/users/UserForm.jsx
import { useState, useEffect } from "react";
import "./UserForm.css";

export default function UserForm({ mode = "user", data, onSave }) {
    const [fullName, setFullName] = useState("");
    const [email, setEmail] = useState("");
    const [isActive, setIsActive] = useState(true);
    const [role, setRole] = useState("");

    useEffect(() => {
        if (!data) return;

        setFullName(data.fullName || "");
        setEmail(data.email || "");

        if (mode === "admin") {
            setIsActive(data.isActive ?? true);
            setRole(data.roles?.[0] || "");
        }
    }, [data, mode]);

    function handleSubmit(e) {
        e.preventDefault();

        if (mode === "user") {
            onSave({ fullName, email });
        }

        if (mode === "admin") {
            onSave({ fullName, isActive, role });
        }
    }

    return (
        <form className="user-form" onSubmit={handleSubmit}>
            <div className="user-form-group">
                <label>Full Name</label>
                <input
                    type="text"
                    value={fullName}
                    onChange={(e) => setFullName(e.target.value)}
                />
            </div>

            <div className="user-form-group">
                <label>Email</label>
                <input
                    type="text"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    disabled={mode === "admin"}
                />
            </div>

            {mode === "admin" && (
                <>
                    <div className="user-form-group">
                        <label>Active</label>
                        <input
                            type="checkbox"
                            checked={isActive}
                            onChange={(e) => setIsActive(e.target.checked)}
                        />
                    </div>

                    <div className="user-form-group">
                        <label>Role</label>
                        <select value={role} onChange={(e) => setRole(e.target.value)}>
                            <option value="">Select role</option>
                            <option value="User">User</option>
                            <option value="Admin">Admin</option>
                        </select>
                    </div>
                </>
            )}

            <button type="submit" className="user-form-save">
                Save
            </button>
        </form>
    );
}
