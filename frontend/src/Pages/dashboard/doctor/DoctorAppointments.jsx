import React, { useState, useEffect } from "react";
import DashboardLayout from "../../components/dashboard/DashboardLayout";
import {
  getDoctorAppointment,
  completeAppointment,
} from "../../../services/appointmentService";
import { getByAppointmentId } from "../../../services/doctorFindingsService";
import { useNavigate } from "react-router-dom";

const DoctorAppointments = () => {
  const [appointments, setAppointments] = useState([]);
  const [findingsMap, setFindingsMap] = useState({});
  const [loading, setLoading] = useState(false);

  const navigate = useNavigate();

  useEffect(() => {
    loadAppointments();
  }, []);

 const loadAppointments = async () => {
  try {
    setLoading(true);

    const res =
      await getDoctorAppointment();

    const data =
      res.data || [];

    setAppointments(data);

    // Fetch findings
    const promises = data.map(
      async (a) => {
        try {
          const res =
            await getByAppointmentId(
              a.appointmentId
            );

          return {
            id: a.appointmentId,
            data: res.data,
          };
        } catch {
          return null;
        }
      }
    );

    const results =
      await Promise.all(
        promises
      );

    const map = {};

    results.forEach((r) => {
      // finding exists
      if (r?.data) {
        map[r.id] = r.data;
      }
    });

    setFindingsMap(map);
  } catch (err) {
    console.error(
      "Error loading appointments:",
      err
    );

    alert(
      "Failed to load appointments"
    );
  } finally {
    setLoading(false);
  }
};

  const handleComplete = async (id) => {
    try {
      await completeAppointment(id);
      loadAppointments();
    } catch (err) {
      console.error(err);
      alert("Failed to complete appointment");
    }
  };

  const handleNavigateToFinding = (appointment) => {

    console.log(appointment);
    
    
    navigate(`/doctor/findings/${appointment.appointmentId}`, {
      state: { appointment },
    });
  };

  return (
    <DashboardLayout>
      <h3 className="text-emerald mb-4">My Appointments</h3>

      {loading && <p className="text-mute">Loading...</p>}

      <div className="row">
        {!loading && appointments.length === 0 && (
          <p className="text-mute">No appointments found</p>
        )}

        {appointments.map((a) => {
          const finding = findingsMap[a.appointmentId];

          const isCompleted = a.status === "Completed";
          const isCancelled = a.status === "Cancelled";

          return (
            <div key={a.appointmentId} className="col-lg-6 mb-4">
              <div className="card-dark p-4">

                {/* HEADER */}
                <div className="d-flex justify-content-between mb-2">
                  <h6>
                    Patient: {a.patientName || `ID: ${a.patientId}`}
                  </h6>
                  <span className="text-emerald">{a.status}</span>
                </div>

                {/* DATE */}
                <p className="text-mute mb-1">
                  📅 {new Date(a.appointmentDate).toLocaleDateString()}
                </p>

                {/* TIME */}
                <p className="text-mute mb-3">
                  ⏰ {a.startTime} - {a.endTime}
                </p>

                {/* ACTIONS */}
                <div className="d-flex justify-content-between">

                  {/* Add / Edit Finding */}
                  <button
                    className="btn btn-emerald btn-sm"
                    disabled={!isCompleted || isCancelled}
                    onClick={() => handleNavigateToFinding(a)}
                  >
                    {finding ? "Edit Finding" : "Add Finding"}
                  </button>

                  {/* Complete Button */}
                  {!isCompleted && !isCancelled && (
                    <button
                      className="btn btn-emerald-solid btn-sm"
                      onClick={() => handleComplete(a.appointmentId)}
                    >
                      Complete
                    </button>
                  )}

                </div>

              </div>
            </div>
          );
        })}
      </div>
    </DashboardLayout>
  );
};

export default DoctorAppointments;