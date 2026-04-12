// src/api/comments.js
import { apiFetch } from "@/utils/fetchClient";

// ===============================
// PUBLIC + USER
// ===============================
export function getComments(taskId) {
    return apiFetch(`/tasks/${taskId}/comments`);
}

// ===============================
// USER
// ===============================
export function getCommentById(taskId, commentId) {
    return apiFetch(`/tasks/${taskId}/comments/me/${commentId}`);
}

export function createComment(taskId, dto) {
    return apiFetch(`/tasks/${taskId}/comments`, {
        method: "POST",
        body: JSON.stringify(dto),
    });
}

export function updateComment(taskId, id, dto) {
    return apiFetch(`/tasks/${taskId}/comments/${id}`, {
        method: "PUT",
        body: JSON.stringify(dto),
    });
}

export function deleteComment(taskId, id) {
    return apiFetch(`/tasks/${taskId}/comments/${id}`, {
        method: "DELETE",
    });
}

// ===============================
// ADMIN
// ===============================
export function adminGetComments(taskId) {
    return apiFetch(`/tasks/${taskId}/comments/admin/all`);
}

export function adminDeleteComment(taskId, id) {
    return apiFetch(`/tasks/${taskId}/comments/admin/${id}`, {
        method: "DELETE",
    });
}

// ===============================
// DEFAULT EXPORT (KRITISK)
// ===============================
const commentService = {
    getComments,
    getCommentById,
    createComment,
    updateComment,
    deleteComment,
    adminGetComments,
    adminDeleteComment,
};

export default commentService;
