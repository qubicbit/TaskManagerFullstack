// src/pages/Public/PublicTaskDetailPage.jsx

import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import taskService from "@/api/tasks";
import commentService from "@/api/comments";
import TaskDetails from "@/components/tasks/TaskDetails"; // ✔ rätt import
import "./PublicTaskDetailPage.css";

export default function PublicTaskDetailPage() {
    const { id } = useParams();

    const [task, setTask] = useState(null);
    const [comments, setComments] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        loadData();
    }, [id]);

    async function loadData() {
        try {
            const [taskData, commentData] = await Promise.all([
                taskService.getPublicTaskById(id),
                commentService.getComments(id)
            ]);

            setTask(taskData);
            setComments(commentData);
        } catch (err) {
            console.error("Could not load task or comments:", err);
        } finally {
            setLoading(false);
        }
    }

    if (loading) return <p>Loading...</p>;
    if (!task) return <p>Task not found.</p>;

    return (
        <div className="public-task-detail-page">
            <TaskDetails task={task} />

            <div className="task-detail-comments">
                <h3>Comments</h3>

                {comments.length === 0 && <p>No comments yet.</p>}

                {comments.map((c) => (
                    <div key={c.id} className="comment">
                        <p className="comment-text">{c.content}</p>
                        <p className="comment-date">
                            {new Date(c.createdAt).toLocaleString("sv-SE")}
                        </p>
                    </div>
                ))}
            </div>
        </div>
    );
}
