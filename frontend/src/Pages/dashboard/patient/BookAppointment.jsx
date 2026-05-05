import { useState, useEffect } from "react";
import DashboardLayout from "../../components/dashboard/DashboardLayout";
import { getDoctors } from "../../../services/doctorService";
import { createAppointment } from "../../../services/appointmentService";
import { getSlotsByDoctor } from "../../../services/slotService";
import { getTherapies } from "../../../services/therapyService";
import { useNavigate } from "react-router-dom";

const BookAppointment = () => {
  const [doctors, setDoctors] = useState([]);
  const [therapies, setTherapies] = useState([]);
  const [slots, setSlots] = useState([]);

  // ✅ IMPORTANT: initialize properly
  const [form, setForm] = useState({
    doctorId: "",
    therapyId: "",
    date: "",
    slotId: ""
  });

  const navigate = useNavigate();

  // 🔹 Load doctors + therapies
  useEffect(() => {
    const loadData = async () => {
      try {
        const doctorRes = await getDoctors();
        const therapyRes = await getTherapies();

        console.log("Doctors:", doctorRes?.data);
        console.log("Therapies:", therapyRes?.data);

        setDoctors(doctorRes?.data || []);
        setTherapies(therapyRes?.data || []);
      } catch (err) {
        console.error(err);
      }
    };

    loadData();
  }, []);

  // 🔥 ONLY place where slots API is called
  useEffect(() => {
    if (form.doctorId && form.date) {
      fetchSlots(form.doctorId, form.date);
    }
  }, [form.doctorId, form.date]);

  const fetchSlots = async (doctorId, date) => {
    try {
      console.log("CALLING API WITH:", doctorId, date);

      const res = await getSlotsByDoctor(doctorId, date);

      const available = res?.data?.filter((s) => !s.isBooked) || [];

      console.log("AVAILABLE SLOTS:", available);

      setSlots(available);
    } catch (err) {
      console.error("Slot fetch error:", err);
      setSlots([]);
    }
  };

  return (
    <DashboardLayout>
      <h3 className="text-emerald mb-4">Book Appointment</h3>

      <div className="card-dark p-4">

        {/* DOCTOR */}
        <select
          className="form-select mb-3"
          value={form.doctorId}
          onChange={(e) =>
            setForm((prev) => ({
              ...prev,
              doctorId: e.target.value
            }))
          }
        >
          <option value="">Select Doctor</option>
          {doctors.map((d) => (
            <option key={d.userId} value={d.userId}>
              Dr. {d.fullName}
            </option>
          ))}
        </select>

        {/* THERAPY */}
        <select
          className="form-select mb-3"
          value={form.therapyId}
          onChange={(e) =>
            setForm((prev) => ({
              ...prev,
              therapyId: e.target.value
            }))
          }
        >
          <option value="">Select Therapy</option>
          {therapies.map((t) => (
            <option key={t.therapyId} value={t.therapyId}>
              {t.name}
            </option>
          ))}
        </select>

        {/* DATE */}
        <input
          type="date"
          className="form-control mb-3"
          value={form.date || ""}
          onChange={(e) => {
            const value = e.target.value;
            console.log("DATE SELECTED:", value);

            setForm((prev) => ({
              ...prev,
              date: value
            }));
          }}
        />

        {/* SLOTS */}
        <div className="row">
          {slots.length === 0 ? (
            <p className="text-mute">No available slots</p>
          ) : (
            slots.map((s) => (
              <div key={s.slotId} className="col-md-3">
                <div
                  className={`card-dark p-2 text-center ${
                    form.slotId === s.slotId ? "border-emerald" : ""
                  }`}
                  style={{ cursor: "pointer" }}
                  onClick={() =>
                    setForm((prev) => ({
                      ...prev,
                      slotId: s.slotId
                    }))
                  }
                >
                  {s.startTime} - {s.endTime}
                </div>
              </div>
            ))
          )}
        </div>

        {/* BUTTON */}
        <button
          className="btn btn-emerald w-100 mt-3"
          disabled={
            !form.doctorId || !form.therapyId || !form.date || !form.slotId
          }
          onClick={async () => {
            try {
              console.log("FINAL FORM:", form);

              const res = await createAppointment(form);

              const appointmentId = res?.data?.appointmentId;

              alert("Appointment booked successfully!");

              navigate("/patient/payment", {
                state: { appointmentId }
              });
            } catch (err) {
              console.error(err);
              alert("Booking failed");
            }
          }}
        >
          Book Appointment
        </button>

      </div>
    </DashboardLayout>
  );
};

export default BookAppointment;