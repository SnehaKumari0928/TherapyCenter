// pages/dashboard/PatientDashboard.jsx

import { useContext,useEffect,useState } from "react";
import { AuthContext } from "../../../context/AuthContext";
import { getMyAppointments } from "../../../services/appointmentService";
import {  useNavigate } from "react-router-dom";
import DashboardLayout from "../../components/dashboard/DashboardLayout.jsx"
const PatientDashboard = () => {
  const {user} = useContext(AuthContext)
  const [appointments, setAppointments] = useState([])
  const navigate = useNavigate();

  useEffect(()=>{
   fetchAppointments();
  },[]);

  const fetchAppointments = async () => {
  try {
    const res = await getMyAppointments();

    // If API returns data inside .data, use this instead:
    const myAppointments = res?.data ?? res;

    setAppointments(myAppointments);
    console.log(myAppointments);
  } catch (error) {
    console.error("Error fetching appointments:", error);
  }
};

 const total = appointments.length;
 const upcoming = appointments.filter(a => a.status === "Scheduled").length;
 const completed = appointments.filter(a => a.status === "Completed").length;
 const recent = appointments.slice(0,5);

  return (
    <DashboardLayout>
 
      {/* HEADER */}
      <div className="d-flex justify-content-between mb-4">
        <div>
          <h3 className="text-emerald">Welcome {user.firstName}</h3>
          <p className="text-mute">Manage your therapy journey</p>
        </div>
 
        <button
          className="btn btn-emerald"
          onClick={() => navigate("book-appointment")}
        >
          + Book Appointment
        </button>
      </div>
 
      {/* STATS */}
      <div className="row mb-4">
 
        <div className="col-md-4">
          <div className="card-dark p-3 text-center">
            <h4 className="text-emerald">{total}</h4>
            <p className="text-mute small">Total</p>
          </div>
        </div>
 
        <div className="col-md-4">
          <div className="card-dark p-3 text-center">
            <h4 className="text-warning">{upcoming}</h4>
            <p className="text-mute small">Upcoming</p>
          </div>
        </div>
 
        <div className="col-md-4">
          <div className="card-dark p-3 text-center">
            <h4 className="text-success">{completed}</h4>
            <p className="text-mute small">Completed</p>
          </div>
        </div>
 
      </div>
 
      {/* RECENT */}
      <div className="card-dark p-4 mb-4">
 
        <div className="d-flex justify-content-between mb-3">
          <h5 className="text-emerald">Recent Appointments</h5>
 
          <button
            className="btn btn-emerald-outline btn-sm"
            onClick={() => navigate("/patient/appointments")}
          >
            View All
          </button>
        </div>
 
        {recent.length === 0 ? (
          <p className="text-mute">No appointments yet</p>
        ) : (
          <table className="table table-dark">
            <thead>
              <tr>
                <th>Date</th>
                <th>Time</th>
                <th>Status</th>
              </tr>
            </thead>
 
            <tbody>
              {recent.map(a => (
                <tr key={a.appointmentId}>
                  <td>{a.appointmentDate}</td>
                  <td>{a.startTime} - {a.endTime}</td>
                  <td>{a.status}</td>
                </tr>
              ))}
            </tbody>
 
          </table>
        )}
 
      </div>
 
      {/* QUICK ACTIONS */}
      {/* <div className="card-dark p-4 text-center">
 
        <h5 className="text-emerald mb-3">Quick Actions</h5>
 
        <button
          className="btn btn-emerald me-2"
          onClick={() => navigate("/patient/book")}
        >
          Book Appointment
        </button>
 
        <button
          className="btn btn-emerald-outline me-2"
          onClick={() => navigate("/patient/appointments")}
        >
          My Appointments
        </button>
 
        <button
          className="btn btn-emerald-outline"
          onClick={() => navigate("/patient/reports")}
        >
          Reports
        </button>
 
      </div>
  */}
    </DashboardLayout>
 
  );
};

export default PatientDashboard;