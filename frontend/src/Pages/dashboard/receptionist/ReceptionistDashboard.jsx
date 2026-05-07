
import { useEffect, useState } from "react";

import DashboardLayout from "../../components/dashboard/DashboardLayout";

import { getAllAppointments } from "../../../services/appointmentService";

import { getDoctors } from "../../../services/doctorService";

 

const ReceptionistDashboard = () => {

 

  const [appointments, setAppointments] = useState([]);

  const [doctors, setDoctors] = useState([]);

 

  useEffect(() => {

    loadData();

  }, []);

 

  const loadData = async () => {

    try {

 

      const apptRes = await getAllAppointments();

      const doctorRes = await getDoctors();

 

      setAppointments(apptRes?.data || []);

      setDoctors(doctorRes?.data || []);

 

    } catch (err) {

      console.error(err);

    }

  };

 

  const todayAppointments = appointments.filter(a => {

    return a.appointmentDate === new Date().toISOString().split("T")[0];

  });

 

  const pending = appointments.filter(a => a.status === "Scheduled");

 

  return (

    <DashboardLayout>

 

      {/* HEADER */}

      <div className="mb-4">

        <h3 className="text-emerald">

          Receptionist Dashboard

        </h3>

 

        <p className="text-mute">

          Manage appointments & patients

        </p>

      </div>

 

      {/* STATS */}

      <div className="row mb-4">

 

        <div className="col-md-4">

          <div className="card-dark p-3 text-center">

            <h4 className="text-emerald">

              {todayAppointments.length}

            </h4>

 

            <p className="text-mute small">

              Today's Appointments

            </p>

          </div>

        </div>

 

        <div className="col-md-4">

          <div className="card-dark p-3 text-center">

            <h4 className="text-warning">

              {pending.length}

            </h4>

 

            <p className="text-mute small">

              Pending

            </p>

          </div>

        </div>

 

        <div className="col-md-4">

          <div className="card-dark p-3 text-center">

            <h4 className="text-info">

              {doctors.length}

            </h4>

 

            <p className="text-mute small">

              Doctors

            </p>

          </div>

        </div>

 

      </div>

 

      {/* TODAY APPOINTMENTS */}

      <div className="card-dark p-4">

 

        <h5 className="text-emerald mb-3">

          Today's Appointments

        </h5>

 

        {todayAppointments.length === 0 ? (

          <p className="text-mute">

            No appointments today

          </p>

        ) : (

          <div className="table-responsive">

 

            <table className="table table-dark align-middle">

 

              <thead>

                <tr>

                  <th>Patient</th>

                  <th>Doctor</th>

                  <th>Time</th>

                  <th>Status</th>

                </tr>

              </thead>

 

              <tbody>

 

                {todayAppointments.map((a) => (

 

                  <tr key={a.appointmentId}>

 

                    <td>{a.patientName || a.patientId}</td>

 

                    <td>

                      Dr. {a.doctorName || a.doctorId}

                    </td>

 

                    <td>

                      {a.startTime} - {a.endTime}

                    </td>

 

                    <td>

                      <span className="text-warning">

                        {a.status}

                      </span>

                    </td>

 

                  </tr>

 

                ))}

 

              </tbody>

 

            </table>

 

          </div>

        )}

 

      </div>

 

    </DashboardLayout>

  );

};

 

export default ReceptionistDashboard;

