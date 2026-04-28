import React from 'react'
import { useEffect, useState } from 'react'
import DashboardLayout from '../../components/dashboard/DashboardLayout'
import { getMyAppointments, cancelAppointment } from '../../../services/appointmentService'

const MyAppointments = () => {

    const [data,setData] = useState([])

    const load = async()=>{
        const res = await getMyAppointments();
        setData(res.data)
    }

    useEffect(()=>{
      load()
    },[])
  return (
     <DashboardLayout>
 
      <h3 className="text-emerald mb-4">My Appointments</h3>
 
      <div className="card-dark p-4">
 
        <table className="table table-dark">
          <thead>
            <tr>
              <th>Date</th>
              <th>Time</th>
              <th>Status</th>
              <th>Action</th>
            </tr>
          </thead>
 
          <tbody>
            {data.map(a => (
              <tr key={a.appointmentId}>
                <td>{a.appointmentDate}</td>
                <td>{a.startTime} - {a.endTime}</td>
                <td>{a.status}</td>
                <td>
                  {a.status === "Scheduled" && (
                    <button
                      className="btn btn-danger btn-sm"
                      onClick={() => cancelAppointment(a.appointmentId)}
                    >
                      Cancel
                    </button>
                  )}
                </td>
              </tr>
            ))}
          </tbody>
 
        </table>
 
      </div>
 
    </DashboardLayout>
 
  )
}

export default MyAppointments