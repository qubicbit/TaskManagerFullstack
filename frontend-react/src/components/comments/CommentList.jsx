import CommentItem from "./CommentItem";
import "./CommentList.css";

export default function CommentList({ comments, onDelete, onSave }) {
    if (!comments.length) {
        return <p className="comment-list-empty">No comments yet.</p>;
    }

    return (
        <div className="comment-list">
            {comments.map(c => (
                <CommentItem
                    key={c.id}
                    comment={c}
                    onDelete={() => onDelete(c.id)}
                    onSave={(text) => onSave(c.id, text)}
                />
            ))}
        </div>
    );
}
