import { useState, useEffect, useContext } from "react"
import { getDoctors } from "../../../services/doctorService"
import { getslots } from "../../../services/slotService"
import { getTherapies } from "../../../services/therapyService"
import { createAppointment } from "../../../services/appointmentService"
import { AuthContext } from "../../../context/AuthContext"
import DashboardLayout from "../../components/dashboard/DashboardLayout"
const BookAppointment = () => {
  const {user} = useContext(AuthContext)
  const [doctors, setDoctors] = useState([])
  const [therapies, setTherapies] = useState([])
  const [slots, setSlots] = useState([])
  const [form , setForm] = useState({
    patientId:"",
    doctorId:"",
    therapyId:"",
    slotId:"",
    notes:""
  })

  useEffect(()=>{
   loadData()
  },[])

  const loadData = async()=>{
    const d = await getDoctors()
    const t = await getTherapies()
    const s = await getslots()

    setDoctors(d.data.filter(u => u.role === "Doctor"))
    setTherapies(t.data)
    setSlots(s.data.filter(s !== IsBooked))
  }

  const handleSubmit = async(e)=>{
    e.preventDefault()

    try{
        await createAppointment({
            ...form,
            receptionistId: user.userId,
            patientId: Number(form.patientId),
            doctorId: Number(form.doctorId),
            therapyId: Number(form.therapyId),
            slotId: Number(form.slotId)
        })

        alert("Appointment booked")
    }
    catch(err){
        alert(err.response?.data || "Error booking")
    }
  }

  return (
    <DashboardLayout>

      <h3 className="text-emerald mb-4">Book Appointment</h3>

      <div className="card-dark p-4">

        <form onSubmit={handleSubmit}>

          <input
            className="form-control mb-2"
            placeholder="Patient ID"
            onChange={(e) =>
              setForm({ ...form, patientId: e.target.value })
            }
            required
          />

          <select
            className="form-select mb-2"
            onChange={(e) =>
              setForm({ ...form, doctorId: e.target.value })
            }
            required
          >
            <option>Select Doctor</option>
            {doctors.map(d => (
              <option key={d.userId} value={d.userId}>
                {d.firstName} {d.lastName}
              </option>
            ))}
          </select>

          <select
            className="form-select mb-2"
            onChange={(e) =>
              setForm({ ...form, therapyId: e.target.value })
            }
            required
          >
            <option>Select Therapy</option>
            {therapies.map(t => (
              <option key={t.therapyId} value={t.therapyId}>
                {t.name}
              </option>
            ))}
          </select>

          <select
            className="form-select mb-2"
            onChange={(e) =>
              setForm({ ...form, slotId: e.target.value })
            }
            required
          >
            <option>Select Slot</option>
            {slots.map(s => (
              <option key={s.slotId} value={s.slotId}>
                {s.date} ({s.startTime}-{s.endTime})
              </option>
            ))}
          </select>

          <textarea
            className="form-control mb-3"
            placeholder="Notes"
            onChange={(e) =>
              setForm({ ...form, notes: e.target.value })
            }
          />

          <button className="btn btn-emerald w-100">
            Book Appointment
          </button>

        </form>

      </div>

    </DashboardLayout>
  )
}

export default BookAppointment
