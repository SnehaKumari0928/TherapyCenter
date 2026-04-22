// pages/dashboard/AdminDashboard.jsx
import DashboardLayout from "../components/dashboard/DashboardLayout";
import StatCard from "../components/dashboard/StatCard";
const AdminDashboard = () => {
  return (
    <DashboardLayout>
      <h3 className="text-emerald mb-4">Admin Dashboard</h3>

      <div className="row">
        <div className="col-md-3 mb-3">
          <StatCard title="Doctors" value="12" />
        </div>

        <div className="col-md-3 mb-3">
          <StatCard title="Patients" value="120" />
        </div>

        <div className="col-md-3 mb-3">
          <StatCard title="Appointments" value="45" />
        </div>
      </div>
    </DashboardLayout>
  );
};

export default AdminDashboard;