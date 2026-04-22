// components/dashboard/Sidebar.jsx
import { useContext } from "react";
import { Link } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
const Sidebar = () => {
  const { user } = useContext(AuthContext);

  return (
    <div className="p-3" style={{ width: "240px", borderRight: "1px solid #111" }}>
      <h5 className="text-emerald mb-4">Therapy</h5>

      {/* Admin */}
      {user?.role === "Admin" && (
        <>
          <Link className="d-block mb-2 text-light" to="/admin">Dashboard</Link>
          <Link className="d-block mb-2 text-light" to="#">Manage Doctors</Link>
          <Link className="d-block mb-2 text-light" to="#">Manage Therapies</Link>
        </>
      )}

      {/* Doctor */}
      {user?.role === "Doctor" && (
        <>
          <Link className="d-block mb-2 text-light" to="/doctor">Appointments</Link>
          <Link className="d-block mb-2 text-light" to="#">Add Findings</Link>
        </>
      )}

      {/* Receptionist */}
      {user?.role === "Receptionist" && (
        <>
          <Link className="d-block mb-2 text-light" to="/receptionist">Book Appointment</Link>
          <Link className="d-block mb-2 text-light" to="#">Patients</Link>
        </>
      )}

      {/* Patient / Guardian */}
      {(user?.role === "Patient" || user?.role === "Guardian") && (
        <>
          <Link className="d-block mb-2 text-light" to="/patient">Dashboard</Link>
          <Link className="d-block mb-2 text-light" to="#">My Appointments</Link>
          <Link className="d-block mb-2 text-light" to="#">Reports</Link>
        </>
      )}
    </div>
  );
};

export default Sidebar;