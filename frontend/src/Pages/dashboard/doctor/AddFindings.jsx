import { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom'
import DashboardLayout from '../../components/dashboard/DashboardLayout'
import { createDoctorFinding, updateDoctorFinding, deleteDoctorFinding , getByAppointmentId} from '../../../services/doctorFindingsService'

const AddFindings = () => {

    const {id} = useParams();

    const [form, setForm] = useState({
        appointmentId: id,
        observations:"",
        recommendations:"",
        nextSessionDate:""

    })

    const [existingId, setExistingId] = useState(null);

    useEffect(()=>{
        loadFinding()
    },[])

    const loadFinding = async()=>{
        try{
              const res = await getByAppointmentId(id)

              if(res.data){
                setForm({
                    appointmentdId: res.data.appointmentId,
                    observations: res.data.observations || "",
                    recommendations: res.data.recommendations || "",
                    nextSessionDate: res.data.nextSessionDate || ""
                })
                setExistingId(res.data.findingId);
              }
        }
         catch{}
    }

    const handleSubmit = async(e)=>{
        e.preventDefault();

        try{
            if(existingId){
                await updateDoctorFinding(existingId, form);
                alert("Updated successfully")
            }
            else{
                await createDoctorFinding(form)
                alert("Created Successfully")
            }
        }
        catch(err){
            alert(err.response?.data || "Error")
        }
    }
  return (
    <DashboardLayout>

      <h3 className="text-emerald mb-4">
        {existingId ? "Edit Report" : "Add Report"}
      </h3>

      <div className="card-dark p-4">

        <form onSubmit={handleSubmit}>

          {/* Observations */}
          <textarea
            className="form-control mb-3"
            placeholder="Observations (Patient condition, behavior, progress)"
            rows={4}
            value={form.observations}
            onChange={(e) =>
              setForm({ ...form, observations: e.target.value })
            }
          />

          {/* Recommendations */}
          <textarea
            className="form-control mb-3"
            placeholder="Recommendations (Exercises, therapy suggestions)"
            rows={4}
            value={form.recommendations}
            onChange={(e) =>
              setForm({ ...form, recommendations: e.target.value })
            }
          />

          {/* Next Session */}
          <input
            type="date"
            className="form-control mb-3"
            value={form.nextSessionDate}
            onChange={(e) =>
              setForm({ ...form, nextSessionDate: e.target.value })
            }
          />

          <button className="btn btn-emerald w-100">
            {existingId ? "Update Report" : "Save Report"}
          </button>

        </form>

      </div>

    </DashboardLayout>
  )
}

export default AddFindings
