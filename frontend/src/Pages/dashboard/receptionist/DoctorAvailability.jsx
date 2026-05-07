
import { useEffect, useState } from "react";

import DashboardLayout from "../../components/dashboard/DashboardLayout";

import { getDoctors } from "../../../services/doctorService";

import { getSlotsByDoctor } from "../../../services/slotService";

 

const DoctorAvailability = () => {

 

  const [doctors, setDoctors] = useState([]);

  const [slots, setSlots] = useState([]);

 

  useEffect(() => {

    loadDoctors();

  }, []);

 

  const loadDoctors = async () => {

    const res = await getDoctors();

    setDoctors(res?.data || []);

  };

 

  const fetchSlots = async (doctorId) => {

 

    const today = new Date().toISOString().split("T")[0];

 

    const res = await getSlotsByDoctor(doctorId, today);

 

    setSlots(res?.data || []);

  };

 

  return (

    <DashboardLayout>

 

      <h3 className="text-emerald mb-4">

        Doctor Availability

      </h3>

 

      <select

        className="form-select mb-4"

        onChange={(e) => fetchSlots(e.target.value)}

      >

        <option>Select Doctor</option>

 

        {doctors.map((d) => (

          <option key={d.userId} value={d.userId}>

            Dr. {d.fullName}

          </option>

        ))}

 

      </select>

 

      <div className="row">

 

        {slots.map((s) => (

 

          <div key={s.slotId} className="col-md-3 mb-3">

 

            <div className="card-dark p-3 text-center">

 

              <h6 className="text-light">

                {s.startTime} - {s.endTime}

              </h6>

 

              <p

                className={

                  s.isBooked

                    ? "text-danger"

                    : "text-success"

                }

              >

                {s.isBooked ? "Booked" : "Available"}

              </p>

 

            </div>

 

          </div>

 

        ))}

 

      </div>

 

    </DashboardLayout>

  );

};

 

export default DoctorAvailability;

