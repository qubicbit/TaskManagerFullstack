// File: src/pages/Admin/Users/AdminUserEdit.jsx
import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import userService from "@/api/users";
import "./AdminUserEdit.css";

export default function AdminUserEdit() {
    const { id } = useParams();
    const navigate = useNavigate();

    const [form, setForm] = useState({
        fullName: "",
        email: "",
        isActive: false,
        role: "",
    });

    const [loading, setLoading] = useState(true);
    const [saving, setSaving] = useState(false);

    useEffect(() => {
        loadUser();
    }, []);

    async function loadUser() {
        try {
            const data = await userService.adminGetUserById(id);

            setForm({
                fullName: data.fullName,
                email: data.email,
                isActive: data.isActive,
                role: data.role ?? "",
            });
        } catch (err) {
            console.error("Failed to load user:", err);
        } finally {
            setLoading(false);
        }
    }

    function handleChange(e) {
        const { name, value, type, checked } = e.target;
        setForm(prev => ({
            ...prev,
            [name]: type === "checkbox" ? checked : value,
        }));
    }

    async function handleSubmit(e) {
        e.preventDefault();
        setSaving(true);

        try {
            await userService.adminUpdateUser(id, form);
            navigate("/admin/users");
        } catch (err) {
            console.error("Failed to update user:", err);
        } finally {
            setSaving(false);
        }
    }

    if (loading) return <p>Loading user...</p>;

    return (
        <div className="admin-user-edit">
            <h1>Edit User</h1>

            <form onSubmit={handleSubmit} className="admin-form">

                <label>
                    Full Name
                    <input
                        type="text"
                        name="fullName"
                        value={form.fullName}
                        onChange={handleChange}
                    />
                </label>

                <label>
                    Email
                    <input
                        type="email"
                        name="email"
                        value={form.email}
                        onChange={handleChange}
                        required
                    />
                </label>

                <label className="checkbox-row">
                    <input
                        type="checkbox"
                        name="isActive"
                        checked={form.isActive}
                        onChange={handleChange}
                    />
                    Active
                </label>

                <label>
                    Role
                    <select
                        name="role"
                        value={form.role}
                        onChange={handleChange}
                    >
                        <option value="">Select role</option>
                        <option value="Admin">Admin</option>
                        <option value="User">User</option>
                    </select>
                </label>

                <button type="submit" disabled={saving}>
                    {saving ? "Saving..." : "Save Changes"}
                </button>

                <button
                    type="button"
                    className="cancel-btn"
                    onClick={() => navigate("/admin/users")}
                >
                    Cancel
                </button>
            </form>
        </div>
    );
}
