// File: src/components/comments/CommentForm.jsx

import { useState } from "react";
import useEnterSubmit from "@/hooks/useEnterSubmit";
import "./CommentForm.css";

export default function CommentForm({ onSubmit, loading = false }) {
    const [text, setText] = useState("");

    async function handleSubmit() {
        const trimmed = text.trim();
        if (!trimmed) return;

        await onSubmit(trimmed);
        setText("");
    }

    // Hooken används här
    const handleKeyDown = useEnterSubmit(handleSubmit);

    return (
        <form
            className="comment-form"
            onSubmit={(e) => {
                e.preventDefault();
                handleSubmit();
            }}
        >
            <textarea
                className="comment-form-textarea"
                placeholder="Write a comment..."
                value={text}
                onChange={(e) => setText(e.target.value)}
                onKeyDown={handleKeyDown}   //Hooken kopplad här
                disabled={loading}
            />

            <div className="comment-form-actions">
                <button
                    type="submit"
                    className="comment-form-submit"
                    disabled={loading || !text.trim()}
                >
                    {loading ? "Saving..." : "Add comment"}
                </button>
            </div>
        </form>
    );
}
