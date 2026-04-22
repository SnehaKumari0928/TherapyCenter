// components/dashboard/DashboardLayout.jsx
import Sidebar from "./Sidebar";
import Topbar from "./Topbar";

const DashboardLayout = ({ children }) => {
  return (
    <div className="d-flex" style={{ minHeight: "100vh", background: "#000" }}>
      <Sidebar />

      <div className="flex-grow-1">
        <Topbar />

        <div className="p-4">
          {children}
        </div>
      </div>
    </div>
  );
};

export default DashboardLayout;