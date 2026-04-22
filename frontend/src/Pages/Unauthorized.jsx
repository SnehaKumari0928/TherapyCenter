// pages/Unauthorized.jsx
import { Link } from "react-router-dom";

const Unauthorized = () => {
  return (
    <div className="text-center mt-5">
      <h2 className="text-emerald">Access Denied</h2>
      <p className="text-mute">You don’t have permission to view this page.</p>
      <Link to="/" className="btn btn-emerald mt-3">
        Go Home
      </Link>
    </div>
  );
};

export default Unauthorized;