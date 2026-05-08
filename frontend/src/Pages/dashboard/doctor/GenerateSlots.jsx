import { useState } from "react";
import { generateSlot } from "../../../services/slotService";
import DashboardLayout from "../../components/dashboard/DashboardLayout";

const GenerateSlots = () => {
  const [form, setForm] = useState({
    date: "",
    startTime: "",
    endTime: "",
    durationMinutes: 30,
  });

  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState("");
  const [type, setType] = useState("");

  const handleChange = (e) => {
    const { name, value } = e.target;

    setForm((prev) => ({
      ...prev,
      [name]: name === "durationMinutes" ? Number(value) : value,
    }));
  };

  const showMessage = (msg, type) => {
    setMessage(msg);
    setType(type);

    setTimeout(() => {
      setMessage("");
      setType("");
    }, 3000);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);

    try {
      await generateSlot(form);
      showMessage("Slots generated successfully!", "success");

      setForm({
        date: "",
        startTime: "",
        endTime: "",
        durationMinutes: 30,
      });
    } catch (err) {
      showMessage(
        err?.response?.data?.message || "Failed to generate slots",
        "error"
      );
    } finally {
      setLoading(false);
    }
  };

  return (
    <DashboardLayout>
  <div className="section d-flex justify-content-center">
      <div className="card-dark" style={{ width: "100%", maxWidth: "500px" }}>
        
        <h3 className="text-emerald mb-3">Generate Slots</h3>
        <p className="text-muted mb-4">
          Create appointment slots for a selected day
        </p>

        {/* ALERT */}
        {message && (
          <div
            className="mb-3"
            style={{
              padding: "10px",
              borderRadius: "8px",
              border:
                type === "success"
                  ? "1px solid var(--success)"
                  : "1px solid var(--danger)",
              color:
                type === "success"
                  ? "var(--success)"
                  : "var(--danger)",
              background:
                type === "success"
                  ? "#ecfdf5"
                  : "#fef2f2",
              textAlign: "center",
              fontSize: "14px",
            }}
          >
            {message}
          </div>
        )}

        <form onSubmit={handleSubmit}>
          
          {/* DATE */}
          <div className="mb-3">
            <label className="text-muted">Date</label>
            <input
              type="date"
              name="date"
              value={form.date}
              onChange={handleChange}
              className="form-control"
              required
            />
          </div>

          {/* START TIME */}
          <div className="mb-3">
            <label className="text-muted">Start Time</label>
            <input
              type="time"
              name="startTime"
              value={form.startTime}
              onChange={handleChange}
              className="form-control"
              required
            />
          </div>

          {/* END TIME */}
          <div className="mb-3">
            <label className="text-muted">End Time</label>
            <input
              type="time"
              name="endTime"
              value={form.endTime}
              onChange={handleChange}
              className="form-control"
              required
            />
          </div>

          {/* DURATION */}
          <div className="mb-3">
            <label className="text-muted">Duration (minutes)</label>
            <input
              type="number"
              name="durationMinutes"
              value={form.durationMinutes}
              onChange={handleChange}
              className="form-control"
              min="5"
              step="5"
            />
          </div>

          {/* BUTTON */}
          <button
            type="submit"
            disabled={loading}
            className="btn-emerald-solid w-100"
          >
            {loading ? "Generating..." : "Generate Slots"}
          </button>
        </form>
      </div>
    </div>
    </DashboardLayout>
  
  );
};

export default GenerateSlots;