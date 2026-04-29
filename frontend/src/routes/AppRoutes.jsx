// src/routes/AppRoutes.jsx
import { BrowserRouter, Routes, Route } from "react-router-dom";
import ProtectedRoute from "./ProtectedRoute";
import Home from "../components/common/Home";
import Login from "../Pages/auth/Login";
import Register from "../Pages/auth/Register";
import AdminDashboard from "../Pages/dashboard/admin/AdminDashboard";
import ReceptionistDashboard from "../Pages/dashboard/ReceptionistDashboard";
import PatientDashboard from "../Pages/dashboard/patient/PatientDashboard";
import BookAppointment from "../Pages/dashboard/patient/BookAppointment";
import MyAppointments from "../Pages/dashboard/patient/MyAppointments";
import Reports from "../Pages/dashboard/patient/Reports";
import Users from "../Pages/dashboard/admin/Users";
import DoctorDashboard from "../Pages/dashboard/doctor/DoctorDashboard";
import ManageDoctors from "../Pages/dashboard/admin/ManageDoctors";
import ManageTherapies from "../Pages/dashboard/admin/ManageTherapies";
import CreateSlot from "../Pages/dashboard/admin/CreateSlot";
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
          path="/admin/users"
          element={
            <ProtectedRoute roles={["Admin"]}>
              <Users />
            </ProtectedRoute>
          }
        />

<Route
          path="/admin/doctors"
          element={
            <ProtectedRoute roles={["Admin"]}>
              <ManageDoctors />
            </ProtectedRoute>
          }
        />

         <Route
          path="/admin/therapies"
          element={
            <ProtectedRoute roles={["Admin"]}>
              <ManageTherapies />
            </ProtectedRoute>
          }
        />

           <Route
          path="/admin/slots"
          element={
            <ProtectedRoute roles={["Admin"]}>
              <CreateSlot />
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

 <Route path="/patient/book-appointment" element={
  <ProtectedRoute roles={["Patient"]}>
<BookAppointment />
  </ProtectedRoute>
  } />


  <Route path="/patient/my-appointments" element={
  <ProtectedRoute roles={["Patient"]}>
<MyAppointments />
  </ProtectedRoute>
  } />


<Route path="/patient/reports" element={
  <ProtectedRoute roles={["Patient"]}>
<Reports />
  </ProtectedRoute>
  } />

 
        {/* Unauthorized Page */}
        <Route path="/unauthorized" element={<h2>Access Denied</h2>} />

      </Routes>
    </BrowserRouter>
  );
}

export default AppRoutes;