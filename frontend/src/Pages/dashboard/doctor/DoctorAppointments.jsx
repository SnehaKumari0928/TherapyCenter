import React from 'react'
import { useState,useEffect,useContext } from 'react'
import DashboardLayout from '../../components/dashboard/DashboardLayout'
import { getAllAppointments, completeAppointment } from '../../../services/appointmentService'
import { getByAppointmentId } from '../../../services/doctorFindingsService'
import { useNavigate } from 'react-router-dom'

const DoctorAppointments = () => {
  const [appointments, setAppointments] = useState([])
  const [findingsMap, setFindingsMap] = useState({})
  const navigate = useNavigate();

  useEffect(()=>{
   load()
  },[]);

  const load = async()=>{
    const res = await getAllAppointments();

    const user = JSON.parse(localStorage.getItem("user"))

    const filtered = res.data.filter(
      a => a.doctorId === user.userId
    );

    setAppointments(filtered);

    const map = {}
    for(let a of filtered){
      try{
       const f = await getByAppointmentId(a.appointmentId)
       if(f.data) map[a.appointmentId] = f.data;
      }
      catch{

      }
    }

    setFindingsMap(map);
  }

  const handleComplete = async(id)=>{
    await completeAppointment(id);
    load();
  }
  return (
   <DashboardLayout>

      <h3 className="text-emerald mb-4">My Appointments</h3>

      <div className="row">

        {appointments.length === 0 && (
          <p className="text-mute">No appointments found</p>
        )}

        {appointments.map(a => {
          const finding = findingsMap[a.appointmentId];

          return (
            <div key={a.appointmentId} className="col-lg-6 mb-4">

              <div className="card-dark p-4">

                {/* HEADER */}
                <div className="d-flex justify-content-between mb-2">
                  <h6>Patient ID: {a.patientId}</h6>
                  <span className="text-emerald">{a.status}</span>
                </div>

                {/* DETAILS */}
                <p className="text-mute mb-1">
                  📅 {a.appointmentDate}
                </p>

                <p className="text-mute mb-3">
                  ⏰ {a.startTime} - {a.endTime}
                </p>

                {/* ACTIONS */}
                <div className="d-flex justify-content-between">

                  <button
                    className="btn btn-emerald btn-sm"
                    onClick={() =>
                      navigate(`/doctor/finding/${a.appointmentId}`)
                    }
                  >
                    {finding ? "Edit Finding" : "Add Finding"}
                  </button>

                  {a.status !== "Completed" && (
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
  )
}

export default DoctorAppointments