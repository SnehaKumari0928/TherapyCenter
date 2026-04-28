 
import { useEffect, useState } from "react";
 import DashboardLayout from "../../components/dashboard/DashboardLayout";
 import { getMyReports } from "../../../services/doctorFindingsService";
const Reports = () => {
  const [reports, setReports] = useState([]);
  const [loading, setLoading] = useState(true);
 
  useEffect(() => {
    loadReports();
  }, []);
 
  const loadReports = async () => {
    try {
      const res = await getMyReports();
      setReports(res.data);
    } catch (err) {
      console.error("Error fetching reports:", err);
    } finally {
      setLoading(false);
    }
  };
 
  return (
    <DashboardLayout>
 
      {/* HEADER */}
      <div className="mb-4">
        <h3 className="text-emerald">My Reports</h3>
        <p className="text-mute">View your therapy findings and doctor notes</p>
      </div>
 
      {/* CONTENT */}
      <div className="card-dark p-4">
 
        {loading ? (
          <p className="text-mute text-center">Loading reports...</p>
        ) : reports.length === 0 ? (
          <p className="text-mute text-center">No reports available yet</p>
        ) : (
          <div className="row">
 
            {reports.map((report) => (
              <div key={report.findingId} className="col-lg-6 mb-4">
 
                <div className="card-dark p-3 h-100">
 
                  {/* HEADER */}
                  <div className="d-flex justify-content-between align-items-center mb-2">
                    <h5 className="text-emerald mb-0">
                      Appointment #{report.appointmentId}
                    </h5>
 
                    <span className="badge bg-success">
                      Completed
                    </span>
                  </div>
 
                  {/* DATE */}
                  <p className="text-mute small mb-2">
                    {report.createdAt
                      ? new Date(report.createdAt).toLocaleDateString()
                      : "No date"}
                  </p>
 
                  {/* NOTES */}
                  <div className="p-3 border-emerald rounded">
                    {report.notes || "No notes provided"}
                  </div>
 
                </div>
 
              </div>
            ))}
 
          </div>
        )}
 
      </div>
 
    </DashboardLayout>
  );
};
 
export default Reports;
 