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

const [form, setForm] = useState({
  doctorId: "",
  therapyId: "",
  date: "",
  slotId: ""
});
const navigate = useNavigate();

useEffect(() => {

load();

}, []);

useEffect(() => {
  if (form.doctorId && form.date) {
    console.log("CALLING API WITH:", form.doctorId, form.date);
    fetchSlots(form.doctorId, form.date);
  }
}, [form.doctorId, form.date]);

const load = async () => {

const doctorRes = await getDoctors();

const therapyRes = await getTherapies();

console.log(doctorRes)

console.log(therapyRes);





setDoctors(doctorRes?.data || []);

setTherapies(therapyRes?.data || []);

};

// ✅ FIXED SLOT FETCH

const fetchSlots = async (doctorId, date) => {

console.log("CALLING API WITH:", doctorId, date);

if (!doctorId || !date) return;



const res = await getSlotsByDoctor(doctorId, date);



// ✅ show only available slots

setSlots(res?.data?.filter(s => !s.isBooked) || []);



console.log(res?.data?.filter(s => !s.isBooked)|| [])

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
    setForm(prev => ({
      ...prev,
      doctorId: e.target.value
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
  value={form.date}
  onChange={(e) =>
    setForm(prev => ({
      ...prev,
      date: e.target.value
    }))
  }
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

          // navigate("/patient/payment", {

          //   state: { appointmentId },

          // });



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