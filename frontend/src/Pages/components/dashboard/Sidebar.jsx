
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

          <NavLink className="nav-item" to="/admin" >Dashboard</NavLink>
          <NavLink className="nav-item" to="/admin/users">Users</NavLink>

          <NavLink className="nav-item" to="/admin/doctors">Manage Doctors</NavLink>

          <NavLink className="nav-item" to="/admin/therapies">Manage Therapies</NavLink>
          <NavLink className="nav-item" to="/admin/patients">Patients</NavLink>
          <NavLink className="nav-item" to="/admin/appointments">Appointments</NavLink>
          <NavLink className="nav-item" to="/admin/slots">Create Slot</NavLink>

          <NavLink className="nav-item" to="/admin/reports">Reports</NavLink>



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

 

          <NavLink className="nav-item" to="/patient/book-appointment">

            Book Appointment

          </NavLink>

 

          <NavLink className="nav-item" to="/patient/my-appointments">

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

