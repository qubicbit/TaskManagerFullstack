// File: src/components/comments/CommentItem.jsx

import { useState } from "react";
import ConfirmModal from "@/components/ui/ConfirmModal";
import EditModal from "@/components/ui/EditModal";
import "./CommentItem.css";

export default function CommentItem({ comment, onDelete, onSave }) {
    const [showDelete, setShowDelete] = useState(false);
    const [showEdit, setShowEdit] = useState(false);

    function timeAgo(dateString) {
        const date = new Date(dateString + "Z");
        const now = new Date();
        let diffMs = now - date;

        if (diffMs < 0) diffMs = 0;

        const seconds = Math.floor(diffMs / 1000);
        const minutes = Math.floor(seconds / 60);
        const hours = Math.floor(minutes / 60);
        const days = Math.floor(hours / 24);

        if (days > 0) return `${days} day${days > 1 ? "s" : ""} ago`;
        if (hours > 0) return `${hours} hour${hours > 1 ? "s" : ""} ago`;
        if (minutes > 0) return `${minutes} minute${minutes > 1 ? "s" : ""} ago`;
        return "Just now";
    }

    return (
        <div className="comment-item">

            <p className="comment-text">{comment.content}</p>

            <div className="comment-meta">
                <small>{comment.userName}</small>
                <small className="comment-time">{timeAgo(comment.createdAt)}</small>
            </div>

            {comment.isOwner && (
                <div className="comment-actions">
                    <button
                        className="comment-edit"
                        onClick={() => setShowEdit(true)}
                    >
                        Edit
                    </button>

                    <button
                        className="comment-delete"
                        onClick={() => setShowDelete(true)}
                    >
                        Delete
                    </button>
                </div>
            )}

            {showEdit && (
                <EditModal
                    title="Edit comment"
                    initialValue={comment.content}
                    onSave={(value) => {
                        onSave(value);
                        setShowEdit(false);
                    }}
                    onCancel={() => setShowEdit(false)}
                />
            )}

            {showDelete && (
                <ConfirmModal
                    title="Delete comment?"
                    message="Are you sure you want to delete this comment?"
                    onConfirm={() => {
                        onDelete();
                        setShowDelete(false);
                    }}
                    onCancel={() => setShowDelete(false)}
                />
            )}

        </div>
    );
}
