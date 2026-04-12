// File: src/components/tasks/TaskForm.jsx
import { useState, useEffect } from "react";
import categoryService from "@/api/categories";
import tagService from "@/api/tags";
import "./TaskForm.css";
import { validateTask } from "@/validation/taskValidation";

export default function TaskForm({ initialValues = {}, onSubmit, mode }) {
    const [title, setTitle] = useState(initialValues.title || "");
    const [description, setDescription] = useState(initialValues.description || "");
    const [deadline, setDeadline] = useState(
        initialValues.deadline ? initialValues.deadline.substring(0, 10) : ""
    );
    const [categoryId, setCategoryId] = useState(initialValues.categoryId || "");
    const [tagIds, setTagIds] = useState(initialValues.tagIds || []);
    const [isCompleted, setIsCompleted] = useState(initialValues.isCompleted || false);

    const [categories, setCategories] = useState([]);
    const [tags, setTags] = useState([]);

    const [errors, setErrors] = useState({});

    useEffect(() => {
        loadMeta();
    }, []);

    async function loadMeta() {
        const [cats, tgs] = await Promise.all([
            categoryService.getAllCategories(),
            tagService.getAllTags()
        ]);
        setCategories(cats);
        setTags(tgs);
    }

    function toggleTag(id) {
        setTagIds(prev =>
            prev.includes(id)
                ? prev.filter(x => x !== id)
                : [...prev, id]
        );
    }

    function handleSubmit(e) {
        e.preventDefault();

        const dto = {
            title,
            description,
            deadline: deadline ? new Date(deadline).toISOString() : null,
            categoryId: categoryId ? Number(categoryId) : null,
            tagIds
        };

        if (mode === "edit") {
            dto.isCompleted = isCompleted;
        }

        // FRONTEND VALIDATION
        const validationErrors = validateTask(dto);
        if (Object.keys(validationErrors).length > 0) {
            setErrors(validationErrors);
            return;
        }

        setErrors({});
        onSubmit(dto);
    }

    return (
        <form className="task-form" onSubmit={handleSubmit}>
            <label>Title</label>
            <input
                value={title}
                onChange={e => setTitle(e.target.value)}
            />
            {errors.title && <p className="form-error">{errors.title}</p>}

            <label>Description</label>
            <textarea
                value={description}
                onChange={e => setDescription(e.target.value)}
                rows={5}
            />
            {errors.description && <p className="form-error">{errors.description}</p>}

            <label>Deadline</label>
            <input
                type="date"
                value={deadline}
                onChange={e => setDeadline(e.target.value)}
            />

            <label>Category</label>
            <select
                value={categoryId}
                onChange={e => setCategoryId(e.target.value)}
            >
                <option value="">None</option>
                {categories.map(c => (
                    <option key={c.id} value={c.id}>
                        {c.name}
                    </option>
                ))}
            </select>
            {errors.categoryId && <p className="form-error">{errors.categoryId}</p>}

            <label>Tags</label>
            <div className="task-form-tags">
                {tags.map(t => (
                    <label key={t.id} className="tag-checkbox">
                        <input
                            type="checkbox"
                            checked={tagIds.includes(t.id)}
                            onChange={() => toggleTag(t.id)}
                        />
                        {t.name}
                    </label>
                ))}
            </div>
            {errors.tagIds && <p className="form-error">{errors.tagIds}</p>}

            {mode === "edit" && (
                <div className="task-form-completed">
                    <label>
                        <input
                            type="checkbox"
                            checked={isCompleted}
                            onChange={e => setIsCompleted(e.target.checked)}
                        />
                        Completed
                    </label>
                </div>
            )}

            <button type="submit" className="btn-primary">
                Save
            </button>
        </form>
    );
}
