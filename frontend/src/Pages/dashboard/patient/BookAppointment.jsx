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
  const [form, setForm] = useState({});

  const navigate = useNavigate();

  useEffect(() => {
    load();
  }, []);

  const load = async () => {
    const doctorRes = await getDoctors();
    const therapyRes = await getTherapies();

    setDoctors(doctorRes?.data || []);
    setTherapies(therapyRes?.data || []);
  };

  // ✅ FIXED SLOT FETCH
  const fetchSlots = async (doctorId, date) => {
    if (!doctorId || !date) return;

    const res = await getSlotsByDoctor(doctorId, date);

    // ✅ show only available slots
    setSlots(res?.data?.filter(s => !s.isBooked) || []);
  };

  return (
    <DashboardLayout>

      <h3 className="text-emerald mb-4">Book Appointment</h3>

      <div className="card-dark p-4">

        {/* DOCTOR */}
        <select
          className="form-select mb-3"
          onChange={(e) => {
            const doctorId = e.target.value;
            setForm((prev) => ({ ...prev, doctorId }));

            fetchSlots(doctorId, form.date);
          }}
        >
          <option>Select Doctor</option>
          {doctors.map((d) => (
            <option key={d.userId} value={d.userId}>
              Dr. {d.firstName}
            </option>
          ))}
        </select>

        {/* THERAPY */}
        <select
          className="form-select mb-3"
          onChange={(e) =>
            setForm({ ...form, therapyId: e.target.value })
          }
        >
          <option>Select Therapy</option>
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
          onChange={(e) => {
            const date = e.target.value;
            setForm((prev) => ({ ...prev, date }));

            fetchSlots(form.doctorId, date);
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
                    setForm({ ...form, slotId: s.slotId })
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
          onClick={async () => {
            try {
              const res = await createAppointment(form);

              const appointmentId = res?.data?.appointmentId;

              alert("Appointment booked successfully!");

              // 🔥 REDIRECT TO PAYMENT
              navigate("/patient/payment", {
                state: { appointmentId },
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