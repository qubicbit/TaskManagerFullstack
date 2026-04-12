// src/components/ui/EditModal.jsx
import { useState } from "react";
import "./Modal.css";

export default function EditModal({ title, initialValue, onSave, onCancel }) {
    const [value, setValue] = useState(initialValue);

    return (
        <div className="modal-backdrop">
            <div className="modal">
                <h3>{title}</h3>

                <textarea
                    value={value}
                    onChange={(e) => setValue(e.target.value)}
                />

                <div className="modal-actions">
                    <button className="modal-confirm" onClick={() => onSave(value)}>
                        Save
                    </button>

                    <button className="modal-cancel" onClick={onCancel}>
                        Cancel
                    </button>
                </div>
            </div>
        </div>
    );
}
