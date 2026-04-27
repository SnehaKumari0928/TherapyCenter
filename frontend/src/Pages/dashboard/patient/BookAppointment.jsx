import { useState,useEffect } from "react";
import DashboardLayout from "../../components/dashboard/DashboardLayout";
import { getDoctors } from "../../../services/doctorService";
import { createAppointment } from "../../../services/appointmentService";
import { getSlotsByDoctor } from "../../../services/slotService";
import { getTherapies } from "../../../services/therapyService";

const BookAppointment = () => {
  const [doctors, setDoctors] = useState([]);
  const [therapies,setTherapies] = useState([]);
  const [slot,setSlot] = useState([])
  const [form, setForm] = useState({})


  useEffect(()=>{
    load()
  },[])

  const load = async ()=>{
    setDoctors(await getDoctors())
    setTherapies(await getTherapies())
  }

  const fetchSlot = async(doctorId,date)=>{
    const res = await getSlotsByDoctor(doctorId,date)
    setSlot(res.filter(s => s.IsBooked))
  }
  return (
    <div>BookAppointment</div>
  )
}

export default BookAppointment