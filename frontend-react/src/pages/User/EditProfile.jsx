// src/pages/User/EditProfile.jsx

import { useEffect, useState } from "react";
import userService from "@/api/users";
import UserForm from "@/components/users/UserForm";
import "./Profile.css";
import "./EditProfile.css";

export default function EditProfile() {
    const [profile, setProfile] = useState(null);
    const [loading, setLoading] = useState(true);
    const [message, setMessage] = useState("");

    useEffect(() => {
        async function load() {
            const data = await userService.getMe();
            setProfile(data);
            setLoading(false);
        }
        load();
    }, []);

    async function handleSave(dto) {
        setMessage("");

        const oldEmail = profile.email;

        await userService.updateMe(dto);

        const updated = await userService.getMe();
        setProfile(updated);

        // FullName ändrades → visa feedback
        if (dto.fullName && dto.fullName !== profile.fullName) {
            setMessage("Profile updated successfully.");
        }

        // Email ändrades → kräver om-inloggning
        if (dto.email && dto.email !== oldEmail) {
            setMessage("Email updated. Please sign out and sign in again to refresh your profile.");
        }
    }

    if (loading) {
        return <p className="profile-loading">Loading...</p>;
    }

    return (
        <div className="profile-container">
            <h1 className="edit-profile-title">Edit Profile</h1>

            {message && <p className="profile-message">{message}</p>}

            <UserForm
                mode="user"
                data={profile}
                onSave={handleSave}
            />
        </div>
    );
}
