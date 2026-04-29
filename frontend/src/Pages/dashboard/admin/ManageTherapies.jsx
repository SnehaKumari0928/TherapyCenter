import { useState,useEffect } from "react"
import DashboardLayout from "../../components/dashboard/DashboardLayout"
import { getTherapies,createTherapy,updateTherapy,deleteTherapy } from "../../../services/therapyService"
const ManageTherapies = () => {
    const [therapies, setTherapies] = useState([])
    const [form,setForm] = useState({
        name:"",
        description:"",
        durationMinutes:"",
        cost:""
    })
    const [editTherapy, setEditTherapy] = useState(null)

    useEffect(()=>{
      loadTherapies()
    },[])

    const loadTherapies= async()=>{
        const res = await getTherapies()
        setTherapies(res.data)
    }

    const handleSubmit = async(e)=>{
    e.preventDefault()

    try{
       await createTherapy({
        ...form,
        durationMinutes:
        Number(form.durationMinutes),
        cost:
        Number(form.cost)
       })

       setForm({
        name:"",
        description:"",
        durationMinutes:"",
        cost:""
       })
        loadTherapies()
    }
    catch(err){
    console.error(err);
    
    }
    }


    const handleDelete = async(id)=>{
      if(!window.confirm("Delete therapy?")) return;

      await deleteTherapy(id)
      loadTherapies()
    }


    const handleUpdate = async()=>{
        await updateTherapy(editTherapy.therapyId, editTherapy)
        setEditTherapy(null)
        loadTherapies()
    }
  return (
   
<DashboardLayout>

 

      <h3 className="text-emerald mb-4">Manage Therapies</h3>

 

      <div className="row">

 

        {/* CREATE FORM */}

        <div className="col-md-4">

          <div className="card-dark p-3">

 

            <h5 className="text-emerald mb-3">Add Therapy</h5>

 

            <form onSubmit={handleSubmit}>

 

              <input

                className="form-control mb-2"

                placeholder="Therapy Name"

                value={form.name}

                onChange={(e) =>

                  setForm({ ...form, name: e.target.value })

                }

                required

              />

 

              <textarea

                className="form-control mb-2"

                placeholder="Description"

                value={form.description}

                onChange={(e) =>

                  setForm({ ...form, description: e.target.value })

                }

              />

 

              <input

                type="number"

                className="form-control mb-2"

                placeholder="Duration (minutes)"

                value={form.durationMinutes}

                onChange={(e) =>

                  setForm({ ...form, durationMinutes: e.target.value })

                }

                required

              />

 

              <input

                type="number"

                className="form-control mb-3"

                placeholder="Cost (₹)"

                value={form.cost}

                onChange={(e) =>

                  setForm({ ...form, cost: e.target.value })

                }

                required

              />

 

              <button className="btn btn-emerald w-100">

                Create Therapy

              </button>

 

            </form>

 

          </div>

        </div>

 

        {/* LIST */}

        <div className="col-md-8">

          <div className="row">

 

            {therapies.map(t => (

              <div key={t.therapyId} className="col-md-6 mb-3">

 

                <div className="card-dark p-3 h-100">

 

                  <h5 className="text-emerald">{t.name}</h5>

 

                  <p className="text-muted small mb-1">

                    {t.description || "No description"}

                  </p>

 

                  <p className="small mb-1">

                    ⏱ {t.durationMinutes} mins

                  </p>

 

                  <p className="small mb-2">

                    💰 ₹{t.cost}

                  </p>

 

                  <div className="mt-auto d-flex justify-content-between">

 

                    <button

                      className="btn btn-emerald btn-sm"

                      onClick={() => setEditTherapy(t)}

                    >

                      Edit

                    </button>

 

                    <button

                      className="btn btn-danger btn-sm"

                      onClick={() => handleDelete(t.therapyId)}

                    >

                      Delete

                    </button>

 

                  </div>

 

                </div>

 

              </div>

            ))}

 

          </div>

        </div>

 

      </div>

 

      {/* EDIT MODAL */}

      {editTherapy && (

        <div className="modal d-block" style={{ background: "#000000aa" }}>

          <div className="modal-dialog">

            <div className="modal-content card-dark p-3">

 

              <h5 className="text-emerald mb-3">Edit Therapy</h5>

 

              <input

                className="form-control mb-2"

                value={editTherapy.name}

                onChange={(e) =>

                  setEditTherapy({ ...editTherapy, name: e.target.value })

                }

              />

 

              <textarea

                className="form-control mb-2"

                value={editTherapy.description}

                onChange={(e) =>

                  setEditTherapy({ ...editTherapy, description: e.target.value })

                }

              />

 

              <input

                type="number"

                className="form-control mb-2"

                value={editTherapy.durationMinutes}

                onChange={(e) =>

                  setEditTherapy({ ...editTherapy, durationMinutes: Number(e.target.value) })

                }

              />

 

              <input

                type="number"

                className="form-control mb-3"

                value={editTherapy.cost}

                onChange={(e) =>

                  setEditTherapy({ ...editTherapy, cost: Number(e.target.value) })

                }

              />

 

              <div className="d-flex justify-content-end">

                <button

                  className="btn btn-secondary me-2"

                  onClick={() => setEditTherapy(null)}

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

export default ManageTherapies