// File: src/router/AppRouter.jsx

import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";

import ProtectedRoute from "./ProtectedRoute";
import AdminRoute from "./AdminRoute";
import CaseNormalizer from "./CaseNormalizer";

import Layout from "@/components/layout/Layout";

// Public pages
import Home from "@/pages/Public/Home";
import Login from "@/pages/Public/Login";
import Register from "@/pages/Public/Register";
import PublicTasks from "@/pages/Public/PublicTasks";
import PublicTaskDetailPage from "@/pages/Public/PublicTaskDetailPage";

// User pages
import Profile from "@/pages/User/Profile";
import EditProfile from "@/pages/User/EditProfile";

import MyTasks from "@/pages/User/Tasks/MyTasks";
import UserTaskDetailPage from "@/pages/User/Tasks/UserTaskDetailPage";
import UserTaskCreatePage from "@/pages/User/Tasks/UserTaskCreatePage";
import UserTaskEditPage from "@/pages/User/Tasks/UserTaskEditPage";

// Admin pages
import Dashboard from "@/pages/Admin/Dashboard";
import AdminUsers from "@/pages/Admin/Users/AdminUsers";
import AdminUserEdit from "@/pages/Admin/Users/AdminUserEdit";

export default function AppRouter() {
    return (
        <BrowserRouter>
            <Routes>

                {/* Case normalizer wrapper */}
                <Route element={<CaseNormalizer />}>

                    {/* PUBLIC */}
                    <Route path="/home" element={<Home />} />
                    <Route path="/public-tasks" element={<PublicTasks />} />
                    <Route path="/tasks/:id" element={<PublicTaskDetailPage />} />
                    <Route path="/" element={<Navigate to="/home" replace />} />
                    <Route path="/login" element={<Login />} />
                    <Route path="/register" element={<Register />} />

                    {/* USER + ADMIN (WITH LAYOUT) */}
                    <Route element={<Layout />}>

                        {/* USER ROUTES */}
                        <Route
                            path="/profile"
                            element={
                                <ProtectedRoute>
                                    <Profile />
                                </ProtectedRoute>
                            }
                        />

                        <Route
                            path="/profile/edit"
                            element={
                                <ProtectedRoute>
                                    <EditProfile />
                                </ProtectedRoute>
                            }
                        />

                        <Route
                            path="/my-tasks"
                            element={
                                <ProtectedRoute>
                                    <MyTasks />
                                </ProtectedRoute>
                            }
                        />

                        <Route
                            path="/my-tasks/create"
                            element={
                                <ProtectedRoute>
                                    <UserTaskCreatePage />
                                </ProtectedRoute>
                            }
                        />

                        <Route
                            path="/my-tasks/:id"
                            element={
                                <ProtectedRoute>
                                    <UserTaskDetailPage />
                                </ProtectedRoute>
                            }
                        />

                        <Route
                            path="/my-tasks/:id/edit"
                            element={
                                <ProtectedRoute>
                                    <UserTaskEditPage />
                                </ProtectedRoute>
                            }
                        />

                        {/* ADMIN ROUTES */}
                        <Route
                            path="/admin/dashboard"
                            element={
                                <AdminRoute>
                                    <Dashboard />
                                </AdminRoute>
                            }
                        />

                        <Route
                            path="/admin/users"
                            element={
                                <AdminRoute>
                                    <AdminUsers />
                                </AdminRoute>
                            }
                        />

                        <Route
                            path="/admin/users/:id/edit"
                            element={
                                <AdminRoute>
                                    <AdminUserEdit />
                                </AdminRoute>
                            }
                        />

                    </Route>

                    {/* FALLBACK */}
                    <Route path="*" element={<Navigate to="/home" replace />} />

                </Route>
            </Routes>
        </BrowserRouter>
    );
}
