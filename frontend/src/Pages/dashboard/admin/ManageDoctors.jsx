import { useEffect, useState } from "react";
import DashboardLayout from "../../components/dashboard/DashboardLayout";
import {
  getDoctors,
  createDoctor,
  deleteDoctor
} from "../../../services/doctorService";

const ManageDoctors = () => {
  const [doctors, setDoctors] = useState([]);

  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    phoneNumber: "",
    specialization: "",
    startTime: "",
    endTime: ""
  });

  useEffect(() => {
    loadDoctors();
  }, []);

  const loadDoctors = async () => {
    try {
      const res = await getDoctors();
      setDoctors(res.data.filter((u) => u.role === "Doctor"));
    } catch (err) {
      console.error("Error loading doctors:", err);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      await createDoctor(form);

      alert("Doctor created successfully");

      setForm({
        firstName: "",
        lastName: "",
        email: "",
        password: "",
        phoneNumber: "",
        specialization: "",
        startTime: "",
        endTime: ""
      });

      loadDoctors();
    } catch (err) {
      console.error("Create doctor error:", err.response?.data || err);
    }
  };

  const handleDelete = async (id) => {
    if (!window.confirm("Delete doctor?")) return;

    try {
      await deleteDoctor(id);
      loadDoctors();
    } catch (err) {
      console.error("Delete error:", err.response?.data || err);
    }
  };

  return (
    <DashboardLayout>
      <h3 className="text-emerald mb-4">Manage Doctors</h3>

      <div className="row">
        {/* CREATE DOCTOR */}
        <div className="col-md-4">
          <div className="card-dark p-3">
            <h5 className="text-emerald mb-3">Add Doctor</h5>

            <form onSubmit={handleSubmit}>
              <input
                className="form-control mb-2"
                placeholder="First Name"
                value={form.firstName}
                onChange={(e) =>
                  setForm({ ...form, firstName: e.target.value })
                }
                required
              />

              <input
                className="form-control mb-2"
                placeholder="Last Name"
                value={form.lastName}
                onChange={(e) =>
                  setForm({ ...form, lastName: e.target.value })
                }
                required
              />

              <input
                className="form-control mb-2"
                placeholder="Email"
                value={form.email}
                onChange={(e) =>
                  setForm({ ...form, email: e.target.value })
                }
                required
              />

              <input
                type="password"
                className="form-control mb-2"
                placeholder="Password"
                value={form.password}
                onChange={(e) =>
                  setForm({ ...form, password: e.target.value })
                }
                required
              />

              <input
                className="form-control mb-2"
                placeholder="Phone Number"
                value={form.phoneNumber}
                onChange={(e) =>
                  setForm({ ...form, phoneNumber: e.target.value })
                }
              />

              <input
                className="form-control mb-2"
                placeholder="Specialization"
                value={form.specialization}
                onChange={(e) =>
                  setForm({ ...form, specialization: e.target.value })
                }
                required
              />

              <input
                type="time"
                className="form-control mb-2"
                value={form.startTime}
                onChange={(e) =>
                  setForm({ ...form, startTime: e.target.value })
                }
                required
              />

              <input
                type="time"
                className="form-control mb-3"
                value={form.endTime}
                onChange={(e) =>
                  setForm({ ...form, endTime: e.target.value })
                }
                required
              />

              <button className="btn btn-emerald w-100">
                Create Doctor
              </button>
            </form>
          </div>
        </div>

        {/* DOCTOR LIST */}
        <div className="col-md-8">
          <div className="row">
            {doctors.map((d) => (
              <div key={d.userId} className="col-md-6 mb-3">
                <div className="card-dark p-3">
                  <h5 className="text-emerald">
                    {d.firstName} {d.lastName}
                  </h5>

                  <p className="text-muted small">{d.email}</p>

                  <button
                    className="btn btn-danger btn-sm mt-2"
                    onClick={() => handleDelete(d.userId)}
                  >
                    Delete
                  </button>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </DashboardLayout>
  );
};

export default ManageDoctors;