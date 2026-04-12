// File: src/pages/Admin/Users/AdminUsers.jsx
import { useEffect, useState } from "react";
import userService from "@/api/users";
import "./AdminUsers.css";
import { useNavigate } from "react-router-dom";
import ConfirmModal from "@/components/ui/ConfirmModal";

export default function AdminUsers() {
    const [users, setUsers] = useState([]);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    const [deleteId, setDeleteId] = useState(null);
    const [showDeleteModal, setShowDeleteModal] = useState(false);

    useEffect(() => {
        loadUsers();
    }, []);

    async function loadUsers() {
        try {
            const data = await userService.adminGetAllUsers();
            setUsers(data);
        } catch (err) {
            console.error("Failed to load users:", err);
        } finally {
            setLoading(false);
        }
    }

    async function handleDelete() {
        try {
            await userService.adminDeleteUser(deleteId);
            setShowDeleteModal(false);
            setDeleteId(null);
            loadUsers(); // ladda om listan
        } catch (err) {
            console.error("Failed to delete user:", err);
        }
    }

    if (loading) return <p>Loading users...</p>;

    return (
        <div className="admin-users">
            <h1>Users</h1>

            <table className="admin-table">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Email</th>
                        <th>UserName</th>
                        <th>Full Name</th>
                        <th>Role</th>
                        <th>Active</th>
                        <th>Actions</th>
                    </tr>
                </thead>

                <tbody>
                    {users.map(u => (
                        <tr key={u.id}>
                            <td>{u.id}</td>
                            <td>{u.email}</td>
                            <td>{u.userName}</td>
                            <td>{u.fullName}</td>
                            <td>{u.role ?? "—"}</td>
                            <td>{u.isActive ? "Yes" : "No"}</td>
                            <td>
                                <button onClick={() => navigate(`/admin/users/${u.id}/edit`)}>
                                    Edit
                                </button>

                                <button
                                    onClick={() => {
                                        setDeleteId(u.id);
                                        setShowDeleteModal(true);
                                    }}
                                >
                                    Delete
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>

            {showDeleteModal && (
                <ConfirmModal
                    title="Delete User"
                    message="Are you sure you want to delete this user?"
                    onConfirm={handleDelete}
                    onCancel={() => setShowDeleteModal(false)}
                />
            )}
        </div>
    );
}
