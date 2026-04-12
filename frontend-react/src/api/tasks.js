// src/api/tasks.js

import { apiFetch } from "@/utils/fetchClient";

// ===============================
// PUBLIC + USER
// ===============================
export function getPublicTasks() {
    return apiFetch("/tasks");
}

export function getPublicTaskById(id) {
    return apiFetch(`/tasks/${id}`);
}

// ===============================
// USER
// ===============================
export function getMyTasks() {
    return apiFetch("/tasks/me");
}

export function getMyTaskById(id) {
    return apiFetch(`/tasks/me/${id}`);
}


export function createTask(dto) {
    return apiFetch("/tasks", {
        method: "POST",
        body: JSON.stringify(dto),
    });
}

export function updateTask(id, dto) {
    return apiFetch(`/tasks/${id}`, {
        method: "PUT",
        body: JSON.stringify(dto),
    });
}

export function deleteTask(id) {
    return apiFetch(`/tasks/${id}`, {
        method: "DELETE",
    });
}


// export function deleteTask(id) {
//     return apiFetch(`/tasks/me/${id}`, {
//         method: "DELETE",
//     });
// }


// ===============================
// ADMIN
// ===============================
export function adminGetAllTasks() {
    return apiFetch("/tasks/admin/all");
}

export function adminGetTaskById(id) {
    return apiFetch(`/tasks/admin/${id}`);
}

export function adminDeleteTask(id) {
    return apiFetch(`/tasks/admin/${id}`, {
        method: "DELETE",
    });
}

// ===============================
// DEFAULT EXPORT (KRITISK)
// ===============================
const taskService = {
    getPublicTasks,
    getPublicTaskById,
    getMyTasks,
    getMyTaskById,
    createTask,
    updateTask,
    deleteTask,
    adminGetAllTasks,
    adminGetTaskById,
    adminDeleteTask,
};

export default taskService;