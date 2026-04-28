import { useState,useEffect } from "react";
import DashboardLayout from "../../components/dashboard/DashboardLayout";
import { getDoctors } from "../../../services/doctorService";
import { createAppointment } from "../../../services/appointmentService";
import { getSlotsByDoctor } from "../../../services/slotService";
import { getTherapies } from "../../../services/therapyService";

const BookAppointment = () => {
  const [doctors, setDoctors] = useState([]);
  const [therapies,setTherapies] = useState([]);
  const [slots,setSlots] = useState([])
  const [form, setForm] = useState({})


  useEffect(()=>{
    load()
  },[])

  const load = async () => {
  const doctorRes = await getDoctors();
  const therapyRes = await getTherapies();

  console.log("Doctors response:", doctorRes);

  setDoctors(Array.isArray(doctorRes) ? doctorRes : doctorRes.data || []);
  setTherapies(Array.isArray(therapyRes) ? therapyRes : therapyRes.data || []);
};

  const fetchSlots = async(doctorId,date)=>{
    const res = await getSlotsByDoctor(doctorId,date)
    setSlots(res.filter(s => s.IsBooked))
  }
  return (
    <DashboardLayout>
 
      <h3 className="text-emerald mb-4">Book Appointment</h3>
 
      <div className="card-dark p-4">
 
        <select className="form-select mb-3"
          onChange={(e) => {
            setForm({ ...form, doctorId: e.target.value });
          }}>
          <option>Select Doctor</option>
          {doctors.map(d => (
            <option key={d.userId} value={d.userId}>
              Dr. {d.firstName}
            </option>
          ))}
        </select>
 
        <select className="form-select mb-3"
          onChange={(e) => setForm({ ...form, therapyId: e.target.value })}>
          <option>Select Therapy</option>
          {therapies.map(t => (
            <option key={t.therapyId} value={t.therapyId}>
              {t.name}
            </option>
          ))}
        </select>
 
        <input type="date" className="form-control mb-3"
          onChange={(e) => {
            setForm({ ...form, date: e.target.value });
            fetchSlots(form.doctorId, e.target.value);
          }}
        />
 
        <div className="row">
          {slots.map(s => (
            <div key={s.slotId} className="col-md-3">
              <div className="card-dark p-2 text-center cursor-pointer"
                onClick={() => setForm({ ...form, slotId: s.slotId })}>
                {s.startTime} - {s.endTime}
              </div>
            </div>
          ))}
        </div>
 
        <button className="btn btn-emerald w-100 mt-3"
          onClick={async () => {
            await createAppointment(form);
            alert("Booked!");
          }}>
          Book Appointment
        </button>
 
      </div>
 
    </DashboardLayout>
 
  )
}

export default BookAppointment