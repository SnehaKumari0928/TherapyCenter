
import { useContext } from "react";

import DashboardLayout from "../../components/dashboard/DashboardLayout";
import { AuthContext } from "../../../context/AuthContext";
 

const DoctorDashboard = () => {

  const { user } = useContext(AuthContext);

 

  return (

    <DashboardLayout>

 

      {/* HEADER */}

      <div className="mb-4">

        <h2 className="text-emerald">

          Welcome Dr. {user?.firstName || "Doctor"} 👨‍⚕️

        </h2>

        <p className="text-mute">

          Manage your appointments and patient findings

        </p>

      </div>

 

      {/* QUICK OVERVIEW */}

      <div className="row">

 

        <div className="col-md-4 mb-3">

          <div className="card-dark p-4 text-center">

            <h5>Total Appointments</h5>

            <h3 className="text-emerald">--</h3>

          </div>

        </div>

 

        <div className="col-md-4 mb-3">

          <div className="card-dark p-4 text-center">

            <h5>Completed</h5>

            <h3 className="text-emerald">--</h3>

          </div>

        </div>

 

        <div className="col-md-4 mb-3">

          <div className="card-dark p-4 text-center">

            <h5>Pending</h5>

            <h3 className="text-emerald">--</h3>

          </div>

        </div>

 

      </div>

 

    </DashboardLayout>

  );

};

 

export default DoctorDashboard;

