import { Link } from "react-router-dom";
import DashboardLayout from "../../components/dashboard/DashboardLayout";
const ReceptionistDashboard = () => {
  return (
    <DashboardLayout>

      <h3 className="text-emerald mb-4">Receptionist Dashboard</h3>

      <div className="row">

        <div className="col-md-6 mb-4">
          <div className="card-dark p-4 text-center">
            <h5>Book Appointment</h5>
            <p className="text-mute">Schedule patient visits</p>
            <Link to="/receptionist/book" className="btn btn-emerald">
              Go
            </Link>
          </div>
        </div>

        <div className="col-md-6 mb-4">
          <div className="card-dark p-4 text-center">
            <h5>All Appointments</h5>
            <p className="text-mute">View and manage appointments</p>
            <Link to="/receptionist/appointments" className="btn btn-emerald">
              View
            </Link>
          </div>
        </div>

      </div>

    </DashboardLayout>
  );
};

export default ReceptionistDashboard;