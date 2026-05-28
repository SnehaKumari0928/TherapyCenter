import { useState, useEffect } from "react";
import DashboardLayout from "../../components/dashboard/DashboardLayout";
import { getDoctors } from "../../../services/doctorService";
import { createAppointment } from "../../../services/appointmentService";
import { getSlotsByDoctor } from "../../../services/slotService";
import { getTherapies } from "../../../services/therapyService";

const BookAppointment = () => {
  const [doctors, setDoctors] = useState([]);
  const [therapies, setTherapies] = useState([]);
  const [slots, setSlots] = useState([]);

  const [form, setForm] = useState({
    doctorId: "",
    therapyId: "",
    date: "",
    slotId: "",
  });

  useEffect(() => {
    load();
  }, []);

  useEffect(() => {
    if (form.doctorId && form.date) {
      fetchSlots(form.doctorId, form.date);
    }
  }, [form.doctorId, form.date]);

  const load = async () => {
    const doctorRes = await getDoctors();
    const therapyRes = await getTherapies();

    setDoctors(doctorRes?.data || []);
    setTherapies(therapyRes?.data || []);
  };

  const fetchSlots = async (doctorId, date) => {
    if (!doctorId || !date) return;

    const res = await getSlotsByDoctor(doctorId, date);
    setSlots(res?.data?.filter((s) => !s.isBooked) || []);
  };

  const handleBooking = async () => {
    try {
      const res = await createAppointment(form);

      alert("Appointment booked successfully!");

      setForm({
        doctorId: "",
        therapyId: "",
        date: "",
        slotId: "",
      });

      setSlots([]);
    } catch (err) {
      console.error(err);
      alert("Booking failed");
    }
  };

  return (
    <DashboardLayout>
      <div className="section d-flex justify-content-center">
        <div className="card-dark" style={{ width: "100%", maxWidth: "700px" }}>

          <h3 className="text-emerald mb-3">Book Appointment</h3>
          <p className="text-muted mb-4">
            Select doctor, therapy, date and available slot
          </p>

          {/* DOCTOR */}
          <select
            className="form-select mb-3"
            value={form.doctorId}
            onChange={(e) =>
              setForm((prev) => ({
                ...prev,
                doctorId: e.target.value,
              }))
            }
          >
            <option value="">Select Doctor</option>
            {doctors.map((d) => (
              <option key={d.doctorId} value={d.doctorId}>
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
                therapyId: e.target.value,
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
            value={form.date}
            onChange={(e) =>
              setForm((prev) => ({
                ...prev,
                date: e.target.value,
              }))
            }
          />

          {/* SLOTS */}
          <div className="mb-3">
            <h6 className="text-muted mb-2">Available Slots</h6>

            <div className="row">
              {slots.length === 0 ? (
                <p className="text-muted">No available slots</p>
              ) : (
                slots.map((s) => (
                  <div key={s.slotId} className="col-md-4 mb-2">
                    <div
                      className="card-dark text-center"
                      style={{
                        cursor: "pointer",
                        border:
                          form.slotId === s.slotId
                            ? "1px solid var(--primary)"
                            : "1px solid var(--border)",
                        background:
                          form.slotId === s.slotId
                            ? "#e6f2f7"
                            : "var(--surface)",
                      }}
                      onClick={() =>
                        setForm((prev) => ({
                          ...prev,
                          slotId: s.slotId,
                        }))
                      }
                    >
                      {s.startTime} - {s.endTime}
                    </div>
                  </div>
                ))
              )}
            </div>
          </div>

          {/* BUTTON */}
          <button
            className="btn-emerald-solid w-100"
            onClick={handleBooking}
            disabled={!form.slotId || !form.doctorId || !form.date}
          >
            Book Appointment
          </button>
        </div>
      </div>
    </DashboardLayout>
  );
};

export default BookAppointment;