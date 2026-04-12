//src/validation/taskValidation.js
export function validateTaskField(field, value) {
    if (field === "title") {
        if (!value.trim()) return "Title is required.";
        if (value.trim().length < 3) return "Title must be at least 3 characters long.";
    }

    if (field === "description") {
        if (!value.trim()) return "Description is required.";
        if (value.trim().length < 5) return "Description must be at least 5 characters long.";
    }

    if (field === "categoryId") {
        if (!value) return "Category is required.";
        if (Number(value) <= 0) return "Category must be valid.";
    }

    if (field === "tagIds") {
        if (!Array.isArray(value)) return "";
        if (value.some(id => Number(id) <= 0)) {
            return "Tags must contain valid tag IDs.";
        }
    }

    return "";
}

export function validateTask({ title, description, categoryId, tagIds }) {
    const errors = {};

    const titleError = validateTaskField("title", title);
    if (titleError) errors.title = titleError;

    const descError = validateTaskField("description", description);
    if (descError) errors.description = descError;

    const categoryError = validateTaskField("categoryId", categoryId);
    if (categoryError) errors.categoryId = categoryError;

    const tagsError = validateTaskField("tagIds", tagIds);
    if (tagsError) errors.tagIds = tagsError;

    return errors;
}
