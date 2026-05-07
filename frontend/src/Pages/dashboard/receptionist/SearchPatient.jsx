import { useEffect, useState } from "react";

import DashboardLayout from "../../components/dashboard/DashboardLayout";

import { getUsers } from "../../../services/userService";

 

const SearchPatients = () => {

 

  const [patients, setPatients] = useState([]);

  const [search, setSearch] = useState("");

 

  useEffect(() => {

    loadPatients();

  }, []);

 

  const loadPatients = async () => {

    const res = await getUsers();

 

    const allUsers = res?.data || [];

 

    const filtered = allUsers.filter(

      u => u.role === "Patient"

    );

 

    setPatients(filtered);

  };

 

  const filteredPatients = patients.filter((p) =>

    `${p.firstName} ${p.lastName}`

      .toLowerCase()

      .includes(search.toLowerCase())

  );

 

  return (

    <DashboardLayout>

 

      <h3 className="text-emerald mb-4">

        Search Patients

      </h3>

 

      <input

        type="text"

        className="form-control mb-4"

        placeholder="Search patient..."

        value={search}

        onChange={(e) => setSearch(e.target.value)}

      />

 

      <div className="row">

 

        {filteredPatients.map((p) => (

 

          <div key={p.userId} className="col-md-4 mb-3">

 

            <div className="card-dark p-3">

 

              <h5 className="text-light">

                {p.firstName} {p.lastName}

              </h5>

 

              <p className="text-mute">

                {p.email}

              </p>

 

            </div>

 

          </div>

 

        ))}

 

      </div>

 

    </DashboardLayout>

  );

};

 

export default SearchPatients;