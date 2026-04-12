// src/api/taskTags.js
import { apiFetch } from "@/utils/fetchClient";

// ===============================
// PUBLIC + USER
// ===============================
export function getTagsForTask(taskId) {
    return apiFetch(`/tasks/${taskId}/tags`);
}

// ===============================
// USER
// ===============================
export function addTagsToTask(taskId, tagIds) {
    return apiFetch(`/tasks/${taskId}/tags`, {
        method: "POST",
        body: JSON.stringify(tagIds),
    });
}

export function replaceTagsForTask(taskId, tagIds) {
    return apiFetch(`/tasks/${taskId}/tags`, {
        method: "PUT",
        body: JSON.stringify(tagIds),
    });
}

export function removeTagFromTask(taskId, tagId) {
    return apiFetch(`/tasks/${taskId}/tags/${tagId}`, {
        method: "DELETE",
    });
}

// ===============================
// DEFAULT EXPORT (KRITISK)
// ===============================
const taskTagService = {
    getTagsForTask,
    addTagsToTask,
    replaceTagsForTask,
    removeTagFromTask,
};

export default taskTagService;
