
// pages/dashboard/receptionist/ReceptionistBookAppointment.jsx

 

import { useEffect, useState } from "react";

import DashboardLayout from "../../components/dashboard/DashboardLayout";
import { getAllUsers } from "../../../services/userService";
import { getDoctors } from "../../../services/doctorService";

import { getTherapies } from "../../../services/therapyService";

import { getSlotsByDoctor } from "../../../services/slotService";

import { createAppointment } from "../../../services/appointmentService";

 

const ReceptionistBookAppointment = () => {

 

  const [patients, setPatients] = useState([]);

  const [doctors, setDoctors] = useState([]);

  const [therapies, setTherapies] = useState([]);

  const [slots, setSlots] = useState([]);

 

  // 🔥 WALK-IN TOGGLE

  const [isGuest, setIsGuest] = useState(false);

 

  const [form, setForm] = useState({

    patientId: "",

    guestFirstName: "",

    guestLastName: "",

    doctorId: "",

    therapyId: "",

    slotId: "",

    notes: "",

    date: ""

  });

 

  useEffect(() => {

    loadData();

  }, []);

 

  // LOAD INITIAL DATA

  const loadData = async () => {

    try {

 

      const userRes = await getUsers();

      const doctorRes = await getDoctors();

      const therapyRes = await getTherapies();

 

      const allUsers = userRes?.data || [];

 

      const patientUsers = allUsers.filter(

        u => u.role === "Patient"

      );

 

      setPatients(patientUsers);

      setDoctors(doctorRes?.data || []);

      setTherapies(therapyRes?.data || []);

 

    } catch (err) {

      console.error(err);

    }

  };

 

  // FETCH AVAILABLE SLOTS

  const fetchSlots = async (doctorId, date) => {

 

    if (!doctorId || !date) return;

 

    try {

 

      const res = await getSlotsByDoctor(

        doctorId,

        date

      );

 

      // SHOW ONLY AVAILABLE SLOTS

      const available = (res?.data || []).filter(

        s => !s.isBooked

      );

 

      setSlots(available);

 

    } catch (err) {

      console.error(err);

    }

  };

 

  // BOOK APPOINTMENT

  const handleBook = async () => {

 

    try {

 

      // VALIDATION

      if (

        !isGuest &&

        !form.patientId

      ) {

        return alert("Select patient");

      }

 

      if (

        isGuest &&

        (

          !form.guestFirstName ||

          !form.guestLastName

        )

      ) {

        return alert("Enter guest name");

      }

 

      if (

        !form.doctorId ||

        !form.therapyId ||

        !form.slotId

      ) {

        return alert("Fill all required fields");

      }

 

      const payload = {

        patientId: isGuest

          ? null

          : Number(form.patientId),

 

        guestFirstName: isGuest

          ? form.guestFirstName

          : null,

 

        guestLastName: isGuest

          ? form.guestLastName

          : null,

 

        doctorId: Number(form.doctorId),

        therapyId: Number(form.therapyId),

        slotId: Number(form.slotId),

 

        notes: form.notes

      };

 

      await createAppointment(payload);

 

      alert("Appointment booked successfully");

 

      // RESET FORM

      setForm({

        patientId: "",

        guestFirstName: "",

        guestLastName: "",

        doctorId: "",

        therapyId: "",

        slotId: "",

        notes: "",

        date: ""

      });

 

      setSlots([]);

 

    } catch (err) {

      console.error(err);

      alert("Booking failed");

    }

  };

 

  return (

    <DashboardLayout>

 

      {/* HEADER */}

      <div className="mb-4">

 

        <h3 className="text-emerald">

          Book Appointment

        </h3>

 

        <p className="text-mute">

          Create offline appointments

        </p>

 

      </div>

 

      {/* MAIN CARD */}

      <div className="card-dark p-4">

 

        {/* TOGGLE */}

        <div className="d-flex gap-2 mb-4">

 

          <button

            className={`btn ${

              !isGuest

                ? "btn-emerald-solid"

                : "btn-emerald"

            }`}

            onClick={() => setIsGuest(false)}

          >

            Registered Patient

          </button>

 

          <button

            className={`btn ${

              isGuest

                ? "btn-emerald-solid"

                : "btn-emerald"

            }`}

            onClick={() => setIsGuest(true)}

          >

            Walk-in Patient

          </button>

 

        </div>

 

        {/* REGISTERED PATIENT */}

        {!isGuest && (

          <select

            className="form-select mb-3"

            value={form.patientId}

            onChange={(e) =>

              setForm({

                ...form,

                patientId: e.target.value

              })

            }

          >

            <option value="">

              Select Patient

            </option>

 

            {patients.map((p) => (

              <option

                key={p.userId}

                value={p.userId}

              >

                {p.firstName} {p.lastName}

              </option>

            ))}

 

          </select>

        )}

 

        {/* WALK-IN PATIENT */}

        {isGuest && (

          <>

            <input

              type="text"

              className="form-control mb-3"

              placeholder="Guest First Name"

              value={form.guestFirstName}

              onChange={(e) =>

                setForm({

                  ...form,

                  guestFirstName: e.target.value

                })

              }

            />

 

            <input

              type="text"

              className="form-control mb-3"

              placeholder="Guest Last Name"

              value={form.guestLastName}

              onChange={(e) =>

                setForm({

                  ...form,

                  guestLastName: e.target.value

                })

              }

            />

          </>

        )}

 

        {/* DOCTOR */}

        <select

          className="form-select mb-3"

          value={form.doctorId}

          onChange={(e) => {

 

            const doctorId = e.target.value;

 

            setForm({

              ...form,

              doctorId

            });

 

            fetchSlots(

              doctorId,

              form.date

            );

          }}

        >

          <option value="">

            Select Doctor

          </option>

 

          {doctors.map((d) => (

            <option

              key={d.userId}

              value={d.userId}

            >

              Dr. {d.firstName}

            </option>

          ))}

 

        </select>

 

        {/* THERAPY */}

        <select

          className="form-select mb-3"

          value={form.therapyId}

          onChange={(e) =>

            setForm({

              ...form,

              therapyId: e.target.value

            })

          }

        >

          <option value="">

            Select Therapy

          </option>

 

          {therapies.map((t) => (

            <option

              key={t.therapyId}

              value={t.therapyId}

            >

              {t.name}

            </option>

          ))}

 

        </select>

 

        {/* DATE */}

        <input

          type="date"

          className="form-control mb-4"

          value={form.date}

          onChange={(e) => {

 

            const date = e.target.value;

 

            setForm({

              ...form,

              date

            });

 

            fetchSlots(

              form.doctorId,

              date

            );

          }}

        />

 

        {/* SLOTS */}

        <div className="row mb-4">

 

          {slots.length === 0 ? (

            <p className="text-mute">

              No available slots

            </p>

          ) : (

            slots.map((s) => (

 

              <div

                key={s.slotId}

                className="col-md-3 mb-3"

              >

 

                <div

                  className={`card-dark p-3 text-center ${

                    form.slotId === s.slotId

                      ? "border-emerald"

                      : ""

                  }`}

                  style={{

                    cursor: "pointer"

                  }}

                  onClick={() =>

                    setForm({

                      ...form,

                      slotId: s.slotId

                    })

                  }

                >

 

                  <h6 className="text-light">

                    {s.startTime}

                  </h6>

 

                  <small className="text-mute">

                    to {s.endTime}

                  </small>

 

                </div>

 

              </div>

 

            ))

          )}

 

        </div>

 

        {/* NOTES */}

        <textarea

          className="form-control mb-4"

          rows="3"

          placeholder="Additional notes..."

          value={form.notes}

          onChange={(e) =>

            setForm({

              ...form,

              notes: e.target.value

            })

          }

        />

 

        {/* BUTTON */}

        <button

          className="btn btn-emerald w-100"

          onClick={handleBook}

        >

          Book Appointment

        </button>

 

      </div>

 

    </DashboardLayout>

  );

};

 

export default ReceptionistBookAppointment;

