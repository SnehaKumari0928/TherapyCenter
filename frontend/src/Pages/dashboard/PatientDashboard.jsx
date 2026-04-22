// pages/dashboard/PatientDashboard.jsx

import { useContext,useEffect,useState } from "react";
import { AuthContext } from "../../context/AuthContext";
import DashboardLayout from "../components/dashboard/DashboardLayout";

const PatientDashboard = () => {
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