import { useState, useEffect } from "react"
import { getAllUsers, deleteUser, updateUser } from "../../../services/userService"
import DashboardLayout from "../../components/dashboard/DashboardLayout"
import { createUser } from "../../../services/userService"
const Users = () => {
   const [users, setUsers] = useState([])
   const [editUser, setEditUser] = useState(null)
   const [showModal,setShowModal] = useState(false)

   const [form,setForm] = useState({
    firstName:"",
    lastName:"",
    email:"",
    password:"",
    phoneNumber:"",
    role:"Receptionist"
   })

   useEffect(()=>{
    loadUsers();
   },[])

   const loadUsers = async()=>{
     const res = await getAllUsers()
     setUsers(res.data);
   }

   const handleDelete = async (id)=>{
    if(!window.confirm("Delete this user?")) return;

    try{
     await deleteUser(id)
     loadUsers()
    }
    catch(err){
     alert(err.response?.data || "Error deleting")
    }
   }

  const handleUpdate = async()=>{
      try{
      await updateUser(editUser.userId,editUser)
      setEditUser(null)
      loadUsers()
      }
      catch(err){
    console.error(err)
      }
  };

  const handleCreate =async(e)=>{
    e.preventDefault();

    try{
     await createUser(form)

     alert("User created successfully")

     setShowModal(false)

     setForm({
       firstName:"",
    lastName:"",
    email:"",
    password:"",
    phoneNumber:"",
    role:"Receptionist"
     })

     loadUsers()
    }
    catch(err){
     alert(err.response?.data || "Error creating user ")
    }
  }

  return (
   
    <DashboardLayout>

 

      <h3 className="text-emerald mb-4">Manage Users</h3>

 

      <div className="card-dark p-4">

 

        <table className="table">

          <thead>

            <tr>

              <th>Name</th>

              <th>Email</th>

              <th>Role</th>

              <th>Actions</th>

            </tr>

          </thead>

 

          <tbody>

            {users.map(u => (

              <tr key={u.userId}>

                <td>{u.firstName} {u.lastName}</td>

                <td>{u.email}</td>

                <td className="text-emerald">{u.role}</td>

 

                <td>

                  <button

                    className="btn btn-emerald me-2"

                    onClick={() => setEditUser(u)}

                  >

                    Edit

                  </button>

 

                  <button

                    className="btn btn-danger"

                    onClick={() => handleDelete(u.userId)}

                  >

                    Delete

                  </button>

                </td>

              </tr>

            ))}

          </tbody>

 

        </table>

 

      </div>

 

      {/* EDIT MODAL */}

      {editUser && (

        <div className="modal d-block" style={{ background: "#000000aa" }}>

          <div className="modal-dialog">

            <div className="modal-content card-dark p-3">

 

              <h5 className="text-emerald mb-3">Edit User</h5>

 

              <input

                className="form-control mb-2"

                value={editUser.firstName}

                onChange={(e) =>

                  setEditUser({ ...editUser, firstName: e.target.value })

                }

              />

 

              <input

                className="form-control mb-2"

                value={editUser.lastName}

                onChange={(e) =>

                  setEditUser({ ...editUser, lastName: e.target.value })

                }

              />

 

              <select

                className="form-select mb-3"

                value={editUser.role}

                onChange={(e) =>

                  setEditUser({ ...editUser, role: e.target.value })

                }

              >

                <option>Admin</option>

                <option>Doctor</option>

                <option>Receptionist</option>

                <option>Patient</option>

                <option>Guardian</option>

              </select>

 

              <div className="d-flex justify-content-end">

                <button

                  className="btn btn-secondary me-2"

                  onClick={() => setEditUser(null)}

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


        <div className="d-flex justify-content-between align-items-center mb-3">
          <button className="btn btn-emerald "
          onClick={()=> setShowModal(true)}
          >
            + Create User
          </button>
        </div>
    {showModal && (
  <div className="modal d-block" style={{ background: "#000000aa" }}>
    <div className="modal-dialog">
      <div className="modal-content card-dark p-4">

        <h5 className="text-emerald mb-3">Create User</h5>

        <form onSubmit={handleCreate}>

          <input
            className="form-control mb-2"
            placeholder="First Name"
            value={form.firstName}
            onChange={(e) =>
              setForm({ ...form, firstName: e.target.value })
            }
            required
          />

          <input
            className="form-control mb-2"
            placeholder="Last Name"
            value={form.lastName}
            onChange={(e) =>
              setForm({ ...form, lastName: e.target.value })
            }
            required
          />

          <input
            type="email"
            className="form-control mb-2"
            placeholder="Email"
            value={form.email}
            onChange={(e) =>
              setForm({ ...form, email: e.target.value })
            }
            required
          />

          <input
            type="password"
            className="form-control mb-2"
            placeholder="Password"
            value={form.password}
            onChange={(e) =>
              setForm({ ...form, password: e.target.value })
            }
            required
          />

          <input
            className="form-control mb-2"
            placeholder="Phone Number"
            value={form.phoneNumber}
            onChange={(e) =>
              setForm({ ...form, phoneNumber: e.target.value })
            }
            required
          />

          <select
            className="form-select mb-3"
            value={form.role}
            onChange={(e) =>
              setForm({ ...form, role: e.target.value })
            }
          >
            <option value="Receptionist">Receptionist</option>
            <option value="Doctor">Doctor</option>
          </select>

          <div className="d-flex justify-content-end">
            <button
              type="button"
              className="btn btn-secondary me-2"
              onClick={() => setShowModal(false)}
            >
              Cancel
            </button>

            <button className="btn btn-emerald">
              Create
            </button>
          </div>

        </form>

      </div>
    </div>
  </div>
)}
 

    </DashboardLayout>

  );


  
}

export default Users