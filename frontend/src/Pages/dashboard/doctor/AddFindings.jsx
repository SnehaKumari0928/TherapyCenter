import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import DashboardLayout from "../../components/dashboard/DashboardLayout";
import {
  createDoctorFinding,
  updateDoctorFinding,
  getByAppointmentId,
} from "../../../services/doctorFindingsService";

const AddFindings = () => {
  const { appointmentId } = useParams();

  console.log("Route Appointment Id:", appointmentId);

  const [form, setForm] = useState({
    appointmentId: "",
    observations: "",
    recommendations: "",
    nextSessionDate: "",
  });

  const [existingId, setExistingId] = useState(null);

  useEffect(() => {
    if (appointmentId) {
      setForm((prev) => ({
        ...prev,
        appointmentId: Number(appointmentId),
      }));

      loadFinding();
    }
  }, [appointmentId]);

  const loadFinding = async () => {
    try {
      const res = await getByAppointmentId(appointmentId);

      if (res.data) {
        setForm({
          appointmentId: Number(appointmentId),
          observations: res.data.observations || "",
          recommendations: res.data.recommendations || "",
          nextSessionDate: res.data.nextSessionDate || "",
        });

        setExistingId(res.data.findingId);
      }
    } catch (err) {
      console.log("No finding exists yet");
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    console.log("Submitting Form:", form);

    try {
      if (existingId) {
        await updateDoctorFinding(existingId, form);
        alert("Updated Successfully");
      } else {
        await createDoctorFinding(form);
        alert("Created Successfully");
      }

      setForm({
    appointmentId: "",
    observations: "",
    recommendations: "",
    nextSessionDate: "",
  })
    } catch (err) {
      console.log(err);
      alert(err.response?.data || "Error");
    }
  };

  return (
    <DashboardLayout>
      <h3 className="text-emerald mb-4">
        {existingId ? "Edit Report" : "Add Report"}
      </h3>

      <div className="card-dark p-4">
        <form onSubmit={handleSubmit}>
          <textarea
            className="form-control mb-3"
            placeholder="Observations"
            rows={4}
            value={form.observations}
            onChange={(e) =>
              setForm({
                ...form,
                observations: e.target.value,
              })
            }
          />

          <textarea
            className="form-control mb-3"
            placeholder="Recommendations"
            rows={4}
            value={form.recommendations}
            onChange={(e) =>
              setForm({
                ...form,
                recommendations: e.target.value,
              })
            }
          />

          <input
            type="date"
            className="form-control mb-3"
            value={form.nextSessionDate}
            onChange={(e) =>
              setForm({
                ...form,
                nextSessionDate: e.target.value,
              })
            }
          />

          <button className="btn btn-emerald w-100">
            {existingId ? "Update Report" : "Save Report"}
          </button>
        </form>
      </div>
    </DashboardLayout>
  );
};

export default AddFindings;