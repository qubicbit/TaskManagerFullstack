// src/api/admin.js
import { apiFetch } from "@/utils/fetchClient";

// ===============================
// ADMIN
// ===============================
export function adminGetDashboard() {
    return apiFetch("/admin/dashboard");
}

export function adminGetTasksForUser(userId) {
    return apiFetch(`/admin/users/${userId}/tasks`);
}

export function adminGetCommentsForUser(userId) {
    return apiFetch(`/admin/users/${userId}/comments`);
}

const adminService = {
    adminGetDashboard,
    adminGetTasksForUser,
    adminGetCommentsForUser,
};

export default adminService;