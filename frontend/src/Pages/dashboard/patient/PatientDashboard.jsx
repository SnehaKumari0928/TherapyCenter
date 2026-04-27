// pages/dashboard/PatientDashboard.jsx

import { useContext,useEffect,useState } from "react";
import { AuthContext } from "../../../context/AuthContext";
import { getAppointments } from "../../../services/appointmentService";
import { useNavigate } from "react-router-dom";
import DashboardLayout from "../../components/dashboard/DashboardLayout.jsx"
const PatientDashboard = () => {
  const {user} = useContext(AuthContext)
  const [appointments, setAppointments] = useState([])
  const navigate = useNavigate();

  useEffect(()=>{
   fetchAppointments();
  },[]);

  const fetchAppointments = async ()=>{
    const res = await getAppointments();
    const my = res.filter(a => a.patientId === user.userId)
    setAppointments(my)
    console.log(my);
    
  }
  return (
    <DashboardLayout>
      <h3 className="text-emerald mb-4">Patient Dashboard</h3>

      <div className="card-dark p-4">
        <h5>My Appointments</h5>
      </div>
    </DashboardLayout>
  );
};

export default PatientDashboard;