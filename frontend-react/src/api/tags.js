// src/api/tags.js
import { apiFetch } from "@/utils/fetchClient";

// ===============================
// PUBLIC
// ===============================
export function getAllTags() {
    return apiFetch("/tags");
}

// ===============================
// ADMIN
// ===============================
export function adminCreateTag(dto) {
    return apiFetch("/tags", {
        method: "POST",
        body: JSON.stringify(dto),
    });
}

export function adminUpdateTag(id, dto) {
    return apiFetch(`/tags/${id}`, {
        method: "PUT",
        body: JSON.stringify(dto),
    });
}

export function adminDeleteTag(id) {
    return apiFetch(`/tags/${id}`, {
        method: "DELETE",
    });
}

// ===============================
// DEFAULT EXPORT (KRITISK)
// ===============================
const tagService = {
    getAllTags,
    adminCreateTag,
    adminUpdateTag,
    adminDeleteTag,
};

export default tagService;
