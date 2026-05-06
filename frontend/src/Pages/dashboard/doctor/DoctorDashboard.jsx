
import { useContext, useEffect, useState } from "react";

import DashboardLayout from "../../components/dashboard/DashboardLayout";
import { AuthContext } from "../../../context/AuthContext";
import { getDoctorAppointment } from "../../../services/appointmentService";
 

const DoctorDashboard = () => {

  const { user } = useContext(AuthContext);
const [appointments, setAppointments] = useState([])

useEffect(()=>{
    loadAppointments()
})

const loadAppointments = async()=>{
    try{
        const res = await getDoctorAppointment();
        setAppointments(res.data)
    }
    catch(err){
        alert(err.response?.data)
        console.log(err);
        
    }
}

 const total = appointments.length;
  const completed = appointments.filter(a => a.status === "Completed").length;
  const pending = appointments.filter(a => a.status === "Scheduled").length;

  return (

     <DashboardLayout>

      <div className="mb-4">
        <h2 className="text-emerald">
          Welcome Dr. {user?.firstName || "Doctor"} 
        </h2>
        <p className="text-mute">
          Manage your appointments and patient findings
        </p>
      </div>

      <div className="row">

        <div className="col-md-4 mb-3">
          <div className="card-dark p-4 text-center">
            <h5>Total Appointments</h5>
            <h3 className="text-emerald">{total}</h3>
          </div>
        </div>

        <div className="col-md-4 mb-3">
          <div className="card-dark p-4 text-center">
            <h5>Completed</h5>
            <h3 className="text-emerald">{completed}</h3>
          </div>
        </div>

        <div className="col-md-4 mb-3">
          <div className="card-dark p-4 text-center">
            <h5>Pending</h5>
            <h3 className="text-emerald">{pending}</h3>
          </div>
        </div>

      </div>

    </DashboardLayout>

  );

};

 

export default DoctorDashboard;

