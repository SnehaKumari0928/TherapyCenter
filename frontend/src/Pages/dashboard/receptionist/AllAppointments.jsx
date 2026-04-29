import { useEffect, useState } from "react";
import DashboardLayout from "../../components/dashboard/DashboardLayout";
import { getAllAppointments } from "../../../services/appointmentService";
const AllAppointments = () => {
  const [appointments, setAppointments] = useState([]);

  useEffect(() => {
    load();
  }, []);

  const load = async () => {
    const res = await getAllAppointments();
    setAppointments(res.data);
  };

  return (
    <DashboardLayout>

      <h3 className="text-emerald mb-4">All Appointments</h3>

      <div className="row">

        {appointments.map(a => (
          <div key={a.appointmentId} className="col-md-6 mb-3">

            <div className="card-dark p-3">

              <p>👤 Patient: {a.patientId}</p>
              <p>👨‍⚕️ Doctor: {a.doctorId}</p>
              <p>📅 {a.appointmentDate}</p>
              <p>⏰ {a.startTime} - {a.endTime}</p>

              <span className="text-emerald">{a.status}</span>

            </div>

          </div>
        ))}

      </div>

    </DashboardLayout>
  );
};

export default AllAppointments;