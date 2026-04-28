import { useState, useEffect } from "react";
import DashboardLayout from "../../components/dashboard/DashboardLayout";
import { getAllAppointments } from "../../../services/appointmentService";


const AdminDashboard = () => {

  const [appointments, setAppointments] = useState([]);

 

  useEffect(() => {

    load();

  }, []);

 

  const load = async () => {

    const res = await getAllAppointments();

    setAppointments(res.data);

  };

 

  const total = appointments.length;

  const completed = appointments.filter(a => a.status === "Completed").length;

  const cancelled = appointments.filter(a => a.status === "Cancelled").length;

  const scheduled = appointments.filter(a => a.status === "Scheduled").length;

 

  return (

    <DashboardLayout>

 

      <div className="mb-4">

        <h3 className="text-emerald">Admin Dashboard</h3>

        <p className="text-mute">System overview & analytics</p>

      </div>

 

      {/* STATS */}

      <div className="row">

 

        <div className="col-md-3 mb-3">

          <div className="card-dark p-3 text-center">

            <h4 className="text-emerald">{total}</h4>

            <p className="text-mute small">Total Appointments</p>

          </div>

        </div>

 

        <div className="col-md-3 mb-3">

          <div className="card-dark p-3 text-center">

            <h4 className="text-success">{completed}</h4>

            <p className="text-mute small">Completed</p>

          </div>

        </div>

 

        <div className="col-md-3 mb-3">

          <div className="card-dark p-3 text-center">

            <h4 className="text-warning">{scheduled}</h4>

            <p className="text-mute small">Scheduled</p>

          </div>

        </div>

 

        <div className="col-md-3 mb-3">

          <div className="card-dark p-3 text-center">

            <h4 className="text-danger">{cancelled}</h4>

            <p className="text-mute small">Cancelled</p>

          </div>

        </div>

 

      </div>

 

      {/* RECENT APPOINTMENTS */}

      <div className="card-dark p-4 mt-4">

        <h5 className="text-emerald mb-3">Recent Appointments</h5>

 

        <table className="table table-dark">

          <thead>

            <tr>

              <th>ID</th>

              <th>Date</th>

              <th>Status</th>

            </tr>

          </thead>

 

          <tbody>

            {appointments.slice(0, 5).map(a => (

              <tr key={a.appointmentId}>

                <td>{a.appointmentId}</td>

                <td>{a.appointmentDate}</td>

                <td>{a.status}</td>

              </tr>

            ))}

          </tbody>

 

        </table>

      </div>

 

    </DashboardLayout>

  );

};
export default AdminDashboard