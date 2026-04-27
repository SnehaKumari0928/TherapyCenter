
import { useContext } from "react";

import { NavLink } from "react-router-dom";

import { AuthContext } from "../../../context/AuthContext";

 

const Sidebar = () => {

  const { user } = useContext(AuthContext);

 

  return (

    <div

      className="p-3 vh-100"

      style={{ width: "240px", borderRight: "1px solid #111" }}

    >

      <h5 className="text-emerald mb-4">TherapyCenter</h5>

 

      {/* Admin */}

      {user?.role === "Admin" && (

        <>

          <NavLink className="nav-item" to="/admin">Dashboard</NavLink>

          <NavLink className="nav-item" to="#">Manage Doctors</NavLink>

          <NavLink className="nav-item" to="#">Manage Therapies</NavLink>

        </>

      )}

 

      {/* Doctor */}

      {user?.role === "Doctor" && (

        <>

          <NavLink className="nav-item" to="/doctor">Appointments</NavLink>

          <NavLink className="nav-item" to="#">Add Findings</NavLink>

        </>

      )}

 

      {/* Receptionist */}

      {user?.role === "Receptionist" && (

        <>

          <NavLink className="nav-item" to="/receptionist">Dashboard</NavLink>

          <NavLink className="nav-item" to="/receptionist/book">Book Appointment</NavLink>

          <NavLink className="nav-item" to="#">Patients</NavLink>

        </>

      )}

 

      {/* Patient / Guardian */}

      {(user?.role === "Patient" || user?.role === "Guardian") && (

        <>

          <NavLink className="nav-item" to="/patient">Dashboard</NavLink>

 

          <NavLink className="nav-item" to="/book-appointment">

            Book Appointment

          </NavLink>

 

          <NavLink className="nav-item" to="/patient/appointments">

            My Appointments

          </NavLink>

 

          <NavLink className="nav-item" to="/patient/reports">

            Reports

          </NavLink>

        </>

      )}

    </div>

  );

};

 

export default Sidebar;

