// src/api/categories.js
import { apiFetch } from "@/utils/fetchClient";

// ===============================
// PUBLIC
// ===============================
export function getAllCategories() {
    return apiFetch("/categories");
}

// ===============================
// ADMIN
// ===============================
export function adminCreateCategory(dto) {
    return apiFetch("/categories", {
        method: "POST",
        body: JSON.stringify(dto),
    });
}

export function adminUpdateCategory(id, dto) {
    return apiFetch(`/categories/${id}`, {
        method: "PUT",
        body: JSON.stringify(dto),
    });
}

export function adminDeleteCategory(id) {
    return apiFetch(`/categories/${id}`, {
        method: "DELETE",
    });
}

// ===============================
// DEFAULT EXPORT (KRITISK)
// ===============================
const categoryService = {
    getAllCategories,
    adminCreateCategory,
    adminUpdateCategory,
    adminDeleteCategory,
};

export default categoryService;
