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
      setDoctors(res.data);
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
    <h3 className="text-emerald mb-4">
      Manage Doctors
    </h3>

    {/* CREATE DOCTOR FORM */}
    <div className="card-dark p-4 mb-4">
      <h5 className="text-emerald mb-3">
        Add Doctor
      </h5>

      <form onSubmit={handleSubmit}>
        <div className="row">
          <div className="col-md-6">
            <input
              className="form-control mb-3"
              placeholder="First Name"
              value={form.firstName}
              onChange={(e) =>
                setForm({
                  ...form,
                  firstName: e.target.value
                })
              }
              required
            />
          </div>

          <div className="col-md-6">
            <input
              className="form-control mb-3"
              placeholder="Last Name"
              value={form.lastName}
              onChange={(e) =>
                setForm({
                  ...form,
                  lastName: e.target.value
                })
              }
              required
            />
          </div>

          <div className="col-md-6">
            <input
              className="form-control mb-3"
              placeholder="Email"
              value={form.email}
              onChange={(e) =>
                setForm({
                  ...form,
                  email: e.target.value
                })
              }
              required
            />
          </div>

          <div className="col-md-6">
            <input
              type="password"
              className="form-control mb-3"
              placeholder="Password"
              value={form.password}
              onChange={(e) =>
                setForm({
                  ...form,
                  password: e.target.value
                })
              }
              required
            />
          </div>

          <div className="col-md-6">
            <input
              className="form-control mb-3"
              placeholder="Phone Number"
              value={form.phoneNumber}
              onChange={(e) =>
                setForm({
                  ...form,
                  phoneNumber: e.target.value
                })
              }
            />
          </div>

          <div className="col-md-6">
            <input
              className="form-control mb-3"
              placeholder="Specialization"
              value={form.specialization}
              onChange={(e) =>
                setForm({
                  ...form,
                  specialization: e.target.value
                })
              }
              required
            />
          </div>

          <div className="col-md-6">
            <label className="mb-1">
              Start Time
            </label>

            <input
              type="time"
              className="form-control mb-3"
              value={form.startTime}
              onChange={(e) =>
                setForm({
                  ...form,
                  startTime: e.target.value
                })
              }
              required
            />
          </div>

          <div className="col-md-6">
            <label className="mb-1">
              End Time
            </label>

            <input
              type="time"
              className="form-control mb-3"
              value={form.endTime}
              onChange={(e) =>
                setForm({
                  ...form,
                  endTime: e.target.value
                })
              }
              required
            />
          </div>
        </div>

        <button className="btn btn-emerald w-100">
          Create Doctor
        </button>
      </form>
    </div>

    {/* ALL DOCTORS LIST */}
    <div className="card-dark p-4">
      <h5 className="text-emerald mb-4">
        All Doctors
      </h5>

      {doctors.length === 0 ? (
        <p>No doctors found</p>
      ) : (
        doctors.map((d) => (
          <div
            key={d.userId}
            className="border-bottom pb-3 mb-3"
          >
            <div className="d-flex justify-content-between align-items-center">
              <div>
                <h5 className="text-emerald mb-1">
                  Dr. {d.fullName}
                </h5>

                <p className="mb-1 text-muted">
                  {d.email}
                </p>

                <small>
                  {d.specialization}
                </small>
              </div>

              <button
                className="btn btn-emerald btn-sm"
                onClick={() =>
                  handleDelete(d.userId)
                }
              >
                Delete
              </button>
            </div>
          </div>
        ))
      )}
    </div>
  </DashboardLayout>
);
};

export default ManageDoctors;