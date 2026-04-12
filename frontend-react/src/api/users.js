// src/api/users.js
import { apiFetch } from "@/utils/fetchClient";

// ===============================
// USER
// ===============================
export function getMe() {
    return apiFetch("/users/me");
}

export function updateMe(dto) {
    return apiFetch("/users/me", {
        method: "PUT",
        body: JSON.stringify(dto),
    });
}

// ===============================
// ADMIN
// ===============================
export function adminGetAllUsers() {
    return apiFetch("/users");
}

export function adminGetUserById(id) {
    return apiFetch(`/users/${id}`);
}

export function adminUpdateUser(id, dto) {
    return apiFetch(`/users/${id}`, {
        method: "PUT",
        body: JSON.stringify(dto),
    });
}

export function adminDeleteUser(id) {
    return apiFetch(`/users/${id}`, {
        method: "DELETE",
    });
}

// ===============================
// DEFAULT EXPORT (KRITISK)
// ===============================
const userService = {
    getMe,
    updateMe,
    adminGetAllUsers,
    adminGetUserById,
    adminUpdateUser,
    adminDeleteUser,
};

export default userService;
