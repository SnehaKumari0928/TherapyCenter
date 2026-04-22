// components/dashboard/Topbar.jsx
import { useContext } from "react";
import { AuthContext } from "../../../context/AuthContext";

const Topbar = () => {
  const { user, logout } = useContext(AuthContext);

  return (
    <div className="d-flex justify-content-between align-items-center px-4 py-3 border-bottom">
      <h6 className="mb-0 text-light">{user?.role} Panel</h6>

      <button className="btn btn-emerald" onClick={logout}>
        Logout
      </button>
    </div>
  );
};

export default Topbar;