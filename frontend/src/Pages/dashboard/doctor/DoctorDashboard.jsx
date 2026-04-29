// pages/dashboard/DoctorDashboard.jsx

import DashboardLayout from "../../components/dashboard/DashboardLayout";
const DoctorDashboard = () => {
  return (
    <DashboardLayout>
      <h3 className="text-emerald mb-4">Doctor Dashboard</h3>

      <div className="card-dark p-4">
        <h5>Today's Appointments</h5>
        <p className="text-muted">No appointments yet</p>
      </div>
    </DashboardLayout>
  );
};

export default DoctorDashboard;