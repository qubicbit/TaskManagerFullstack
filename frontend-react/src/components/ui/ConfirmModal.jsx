// File: src/components/ui/ConfirmModal.jsx
import "./Modal.css";

export default function ConfirmModal({ title, message, onConfirm, onCancel }) {
    return (
        <div className="modal-backdrop">
            <div className="modal">
                <h3>{title}</h3>
                <p>{message}</p>

                <div className="modal-actions">
                    <button className="modal-delete" onClick={onConfirm}>
                        Delete
                    </button>

                    <button className="modal-cancel" onClick={onCancel}>
                        Cancel
                    </button>
                </div>
            </div>
        </div>
    );
}
