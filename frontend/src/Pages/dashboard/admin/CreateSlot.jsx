import { useState, useEffect } from "react"
import { createSlot,getslots,deleteSlot, updateSlot } from "../../../services/slotService"
import { getDoctors } from "../../../services/doctorService"

import DashboardLayout from "../../components/dashboard/DashboardLayout"
const CreateSlot = () => {
    const [slots, setSlots] = useState([])
    const [doctors, setDoctors] = useState([])
    const [editSlot, setEditSlot] = useState(null)

    const [form,setForm] = useState({
        doctorId:"",
        date:"",
        startTime:"",
        endTime:""

    })

    useEffect(()=>{
      loadDoctors()
      loadSlots()
    },[])

    const loadSlots = async()=>{
        const res = await getslots()
        setSlots(res.data)

    }

    const loadDoctors = async()=>{
        const res = await getDoctors()
        setDoctors(res.data.filter(u => u.Role === "Doctor"))

        console.log(res.data.filter(u => u.Role === "Doctor"))
    }

    const handleSubmit = async(e)=>{
        e.preventDefault()

        try{
          await createSlot({
            ...form,
            doctorId: Number(form.doctorId)
          })

          setForm({
             doctorId:"",
        date:"",
        startTime:"",
        endTime:""
          })

          loadSlots()
        }
        catch(err){
    alert(err.response?.data || "Error creating slot")
        }
    }

    const handleDelete = async(id, IsBooked)=>{
        if(IsBooked){
            alert("Cannot delete booked slot")
            return
        }

        if(!window.confirm("Delete slot?")){
            return
        }

        await deleteSlot(id)
        loadSlots()
    }

    const handleUpdate = async()=>{
        try{
         await updateSlot(editSlot.slotId, editSlot)

         setEditSlot(null)

         loadSlots();
        }
        catch(err){
          alert(err.response?.data || "Update failed")
        }
    }

  return (
   
<DashboardLayout>

 

      <h3 className="text-emerald mb-4">Manage Slots</h3>

 

      <div className="row">

 

        {/* CREATE SLOT */}

        <div className="col-md-4">

          <div className="card-dark p-3">

 

            <h5 className="text-emerald mb-3">Create Slot</h5>

 

            <form onSubmit={handleSubmit}>

 

              <select

                className="form-select mb-2"

                value={form.doctorId}

                onChange={(e) =>

                  setForm({ ...form, doctorId: e.target.value })

                }

                required

              >

                <option value="">Select Doctor</option>

                {doctors.map(d => (

                  <option key={d.userId} value={d.userId}>

                    {d.firstName} {d.lastName}

                  </option>

                ))}

              </select>

 

              <input

                type="date"

                className="form-control mb-2"

                value={form.date}

                onChange={(e) =>

                  setForm({ ...form, date: e.target.value })

                }

                required

              />

 

              <input

                type="time"

                className="form-control mb-2"

                value={form.startTime}

                onChange={(e) =>

                  setForm({ ...form, startTime: e.target.value })

                }

                required

              />

 

              <input

                type="time"

                className="form-control mb-3"

                value={form.endTime}

                onChange={(e) =>

                  setForm({ ...form, endTime: e.target.value })

                }

                required

              />

 

              <button className="btn btn-emerald w-100">

                Create Slot

              </button>

 

            </form>

 

          </div>

        </div>

 

        {/* SLOT LIST */}

        <div className="col-md-8">

          <div className="row">

 

            {slots.map(s => {

              const doctor = doctors.find(d => d.userId === s.doctorId);

 

              return (

                <div key={s.slotId} className="col-md-6 mb-3">

 

                  <div className="card-dark p-3">

 

                    <h6 className="text-emerald">

                      {doctor

                        ? `${doctor.firstName} ${doctor.lastName}`

                        : `Doctor #${s.doctorId}`}

                    </h6>

 

                    <p className="small mb-1">📅 {s.date}</p>

                    <p className="small mb-1">

                      ⏰ {s.startTime} - {s.endTime}

                    </p>

 

                    <p className={`small ${s.isBooked ? "text-danger" : "text-success"}`}>

                      {s.isBooked ? "Booked" : "Available"}

                    </p>

 

                    <div className="d-flex justify-content-between mt-2">

 

                      {!s.isBooked ? (

                        <>

                          <button

                            className="btn btn-emerald btn-sm"

                            onClick={() => setEditSlot(s)}

                          >

                            Edit

                          </button>

 

                          <button

                            className="btn btn-danger btn-sm"

                            onClick={() => handleDelete(s.slotId, s.isBooked)}

                          >

                            Delete

                          </button>

                        </>

                      ) : (

                        <span className="text-danger small">Locked</span>

                      )}

 

                    </div>

 

                  </div>

 

                </div>

              );

            })}

 

          </div>

        </div>

 

      </div>

 

      {/* EDIT MODAL */}

      {editSlot && (

        <div className="modal d-block" style={{ background: "#000000aa" }}>

          <div className="modal-dialog">

            <div className="modal-content card-dark p-3">

 

              <h5 className="text-emerald mb-3">Edit Slot</h5>

 

              <input

                type="date"

                className="form-control mb-2"

                value={editSlot.date}

                onChange={(e) =>

                  setEditSlot({ ...editSlot, date: e.target.value })

                }

              />

 

              <input

                type="time"

                className="form-control mb-2"

                value={editSlot.startTime}

                onChange={(e) =>

                  setEditSlot({ ...editSlot, startTime: e.target.value })

                }

              />

 

              <input

                type="time"

                className="form-control mb-3"

                value={editSlot.endTime}

                onChange={(e) =>

                  setEditSlot({ ...editSlot, endTime: e.target.value })

                }

              />

 

              <div className="d-flex justify-content-end">

                <button

                  className="btn btn-secondary me-2"

                  onClick={() => setEditSlot(null)}

                >

                  Cancel

                </button>

 

                <button

                  className="btn btn-emerald"

                  onClick={handleUpdate}

                >

                  Save

                </button>

              </div>

 

            </div>

          </div>

        </div>

      )}

 

    </DashboardLayout>


  )
}

export default CreateSlot