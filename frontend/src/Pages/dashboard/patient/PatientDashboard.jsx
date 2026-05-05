import { useContext, useEffect, useState } from "react";
import { AuthContext } from "../../../context/AuthContext";
import { getMyAppointments } from "../../../services/appointmentService";
import { getMyPayments, createOrder } from "../../../services/paymentService";
import { useNavigate } from "react-router-dom";
import DashboardLayout from "../../components/dashboard/DashboardLayout";

const PatientDashboard = () => {
  const { user } = useContext(AuthContext);
  const navigate = useNavigate();

  const [appointments, setAppointments] = useState([]);
  const [payments, setPayments] = useState([]);

  useEffect(() => {
    loadData();
  }, []);

  // 🔥 LOAD DATA
  const loadData = async () => {
    try {
      const apptRes = await getMyAppointments();
      const payRes = await getMyPayments();

      setAppointments(apptRes?.data || []);
      setPayments(payRes?.data || []);
    } catch (err) {
      console.error(err);
    }
  };

  // 🔥 MAP PAYMENTS
  const paymentMap = {};
  payments.forEach((p) => {
    paymentMap[p.appointmentId] = p;
  });

  // 🔥 PAY HANDLER
  const handlePay = async (appointmentId) => {
    try {
      const res = await createOrder({ appointmentId });

      navigate("/patient/payment", {
        state: {
          clientSecret: res.data.clientSecret,
          paymentIntentId: res.data.paymentIntentId,
        },
      });
    } catch (err) {
      console.error(err);
      alert("Payment failed");
    }
  };

  // 🔥 STATS
  const total = appointments.length;
  const upcoming = appointments.filter(a => a.status === "Scheduled").length;
  const completed = appointments.filter(a => a.status === "Completed").length;

  const recent = appointments.slice(0, 5);

  return (
    <DashboardLayout>

      {/* HEADER */}
      <div className="d-flex justify-content-between mb-4">
        <div>
          <h3 className="text-emerald">
            Welcome, {user?.firstName}
          </h3>
          <p className="text-mute">
            Track your therapy & payments
          </p>
        </div>

        
      </div>

      {/* STATS */}
      <div className="row mb-4">

        <div className="col-md-4">
          <div className="card-dark p-3 text-center">
            <h4 className="text-emerald">{total}</h4>
            <p className="text-mute small">Total</p>
          </div>
        </div>

        <div className="col-md-4">
          <div className="card-dark p-3 text-center">
            <h4 className="text-warning">{upcoming}</h4>
            <p className="text-mute small">Upcoming</p>
          </div>
        </div>

        <div className="col-md-4">
          <div className="card-dark p-3 text-center">
            <h4 className="text-success">{completed}</h4>
            <p className="text-mute small">Completed</p>
          </div>
        </div>

      </div>

      {/* RECENT APPOINTMENTS */}
      <div className="card-dark p-4">

        <div className="d-flex justify-content-between mb-3">
          <h5 className="text-emerald">Recent Appointments</h5>

          <button
            className="btn btn-emerald-outline btn-sm"
            onClick={() => navigate("/patient/appointments")}
          >
            View All
          </button>
        </div>

        {recent.length === 0 ? (
          <p className="text-mute">No appointments yet</p>
        ) : (
          <div className="table-responsive">
            <table className="table align-middle">

              <thead>
                <tr>
                  <th>Date</th>
                  <th>Time</th>
                  <th>Status</th>
                  <th>Payment</th>
                  <th>Action</th>
                </tr>
              </thead>

              <tbody>
                {recent.map((a) => {
                  const payment = paymentMap[a.appointmentId];

                  const isPaid = payment?.status === "Paid";

                  return (
                    <tr key={a.appointmentId}>

                      <td>{a.appointmentDate}</td>

                      <td>
                        {a.startTime} - {a.endTime}
                      </td>

                      {/* STATUS */}
                      <td>
                        <span
                          // className={
                          //   a.status === "Completed"
                          //     ? "text-success"
                          //     : a.status === "Confirmed"
                          //     ? "text-info"
                          //     : "text-warning"
                          // }
                        >
                          {a.status}
                        </span>
                      </td>

                      {/* PAYMENT */}
                      <td>
                        {isPaid ? (
                          <span className="text-success">Paid</span>
                        ) : (
                          <span className="text-danger">Pending</span>
                        )}
                      </td>

                      {/* ACTION */}
                      <td>
                        {!isPaid && a.status === "Scheduled" && (
                          <button
                            className="btn btn-emerald btn-sm"
                            onClick={() =>
                              handlePay(a.appointmentId)
                            }
                          >
                            Pay Now
                          </button>
                        )}

                        {isPaid && (
                          <span className="text-success">✔</span>
                        )}
                      </td>

                    </tr>
                  );
                })}
              </tbody>

            </table>
          </div>
        )}

      </div>

    </DashboardLayout>
  );
};

export default PatientDashboard;