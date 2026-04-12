// src/pages/User/Profile.jsx

import { useEffect, useState } from "react";
import { useAuth } from "@/context/AuthContext";
import { useNavigate } from "react-router-dom";
import userService from "@/api/users";
import "./Profile.css";

export default function Profile() {
    const { user: authUser } = useAuth();
    const navigate = useNavigate();

    const [profile, setProfile] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        async function load() {
            const data = await userService.getMe();
            setProfile(data);
            setLoading(false);
        }
        load();
    }, []);

    if (!authUser || loading) {
        return (
            <div className="profile-loading">
                <p>Loading profile...</p>
            </div>
        );
    }

    return (
        <div className="profile-container">
            <div className="profile-header">
                <div className="profile-avatar">
                    {profile.email[0].toUpperCase()}
                </div>

                <div>
                    <h1 className="profile-email">{profile.email}</h1>
                    <p className="profile-created">
                        Member since: {new Date(profile.createdAt).toLocaleDateString()}
                    </p>
                </div>
            </div>

            <button
                className="profile-button"
                onClick={() => navigate("/profile/edit")}
            >
                Edit Profile
            </button>
        </div>
    );
}
