import { useState, useEffect } from "react"
import { getAllUsers, deleteUser, updateUser } from "../../../services/userService"
import DashboardLayout from "../../components/dashboard/DashboardLayout"
const Users = () => {
   const [users, setUsers] = useState([])
   const [editUser, setEditUser] = useState(null)

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

 

    </DashboardLayout>

  );


  
}

export default Users