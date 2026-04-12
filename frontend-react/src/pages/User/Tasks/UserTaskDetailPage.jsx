// File: src/pages/User/Tasks/UserTaskDetailPage.jsx

import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import TaskDetails from "@/components/tasks/TaskDetails";
import commentService from "@/api/comments";
import taskService from "@/api/tasks";
import CommentList from "@/components/comments/CommentList";
import CommentForm from "@/components/comments/CommentForm";
import ConfirmModal from "@/components/ui/ConfirmModal";
import "./UserTaskDetailPage.css";

export default function UserTaskDetailPage() {
    const { id } = useParams();
    const navigate = useNavigate();

    const [task, setTask] = useState(null);
    const [comments, setComments] = useState([]);
    const [showDeleteModal, setShowDeleteModal] = useState(false);

    useEffect(() => {
        loadTask();
        loadComments();
    }, [id]);

    async function loadTask() {
        const taskData = await taskService.getMyTaskById(id);
        setTask(taskData);
    }

    async function loadComments() {
        const commentData = await commentService.getComments(id);
        setComments(commentData);
    }

    async function handleAddComment(text) {
        const created = await commentService.createComment(id, { content: text });
        setComments(prev => [created, ...prev]);
    }

    async function handleDeleteComment(commentId) {
        await commentService.deleteComment(id, commentId);
        await loadComments();
    }

    async function handleSaveEdit(commentId, text) {
        await commentService.updateComment(id, commentId, { content: text });
        await loadComments();
    }

    async function handleConfirmDelete() {
        console.log("CONFIRM DELETE → calling deleteTask(", id, ")");
        await taskService.deleteTask(id);
        navigate("/my-tasks");
    }

    return (
        <div className="task-detail-page">

            {task && (
                <div className="task-detail-header">
                    <h1 className="task-title">{task.title}</h1>

                    <div className="task-detail-actions">
                        <button
                            onClick={() => navigate(`/my-tasks/${id}/edit`)}
                            className="btn-small"
                        >
                            Edit Task
                        </button>

                        <button
                            onClick={() => {
                                setShowDeleteModal(true);
                            }}
                            className="btn-small btn-danger"
                        >
                            Delete Task
                        </button>
                    </div>
                </div>
            )}

            {task && <TaskDetails task={task} />}

            <h2>Comments</h2>

            <CommentList
                comments={comments}
                onDelete={handleDeleteComment}
                onSave={handleSaveEdit}
            />

            <CommentForm onSubmit={handleAddComment} />

            {showDeleteModal && (
                <ConfirmModal
                    title="Delete Task"
                    message="Are you sure you want to delete this task?"
                    onConfirm={handleConfirmDelete}
                    onCancel={() => {
                        setShowDeleteModal(false);
                    }}
                />
            )}
        </div>
    );
}
