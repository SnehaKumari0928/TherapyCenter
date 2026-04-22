// src/routes/AppRoutes.jsx
import { BrowserRouter, Routes, Route } from "react-router-dom";
import ProtectedRoute from "./ProtectedRoute";
import Home from "../components/common/Home";
import Login from "../Pages/auth/Login";
import Register from "../Pages/auth/Register";
import AdminDashboard from "../Pages/dashboard/AdminDashboard";
import DoctorDashboard from "../Pages/dashboard/DoctorDashboard";
import ReceptionistDashboard from "../Pages/dashboard/ReceptionistDashboard";
import PatientDashboard from "../Pages/dashboard/PatientDashboard";

function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>

        {/* Public */}
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />

        {/* Protected */}
        <Route
          path="/admin"
          element={
            <ProtectedRoute roles={["Admin"]}>
              <AdminDashboard />
            </ProtectedRoute>
          }
        />

        <Route
          path="/doctor"
          element={
            <ProtectedRoute roles={["Doctor"]}>
              <DoctorDashboard />
            </ProtectedRoute>
          }
        />

        <Route
          path="/receptionist"
          element={
            <ProtectedRoute roles={["Receptionist"]}>
              <ReceptionistDashboard />
            </ProtectedRoute>
          }
        />

        <Route
          path="/patient"
          element={
            <ProtectedRoute roles={["Patient", "Guardian"]}>
              <PatientDashboard />
            </ProtectedRoute>
          }
        />

        {/* Unauthorized Page */}
        <Route path="/unauthorized" element={<h2>Access Denied</h2>} />

      </Routes>
    </BrowserRouter>
  );
}

export default AppRoutes;