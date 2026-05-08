// pages/dashboard/receptionist/BookAppointment.jsx

import { useEffect, useMemo, useState } from "react";
import DashboardLayout from "../../components/dashboard/DashboardLayout";

import { getDoctors } from "../../../services/doctorService";
import { getTherapies } from "../../../services/therapyService";
import { getSlotsByDoctor } from "../../../services/slotService";

import API from "../../../services/api";

const getTodayDate = () =>
  new Date().toISOString().split("T")[0];

const BookAppointment = () => {
  const [doctors, setDoctors] = useState([]);
  const [therapies, setTherapies] = useState([]);
  const [slots, setSlots] = useState([]);

  const [selectedDoctorId, setSelectedDoctorId] =
    useState("");

  const [selectedTherapyId, setSelectedTherapyId] =
    useState("");

  const [selectedDate, setSelectedDate] = useState(
    getTodayDate()
  );

  const [selectedSlot, setSelectedSlot] =
    useState(null);

  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");

  const [loadingDoctors, setLoadingDoctors] =
    useState(false);

  const [loadingSlots, setLoadingSlots] =
    useState(false);

  const [booking, setBooking] = useState(false);

  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  const canFetchSlots = useMemo(
    () =>
      Boolean(
        selectedDoctorId && selectedDate
      ),
    [selectedDoctorId, selectedDate]
  );

  // LOAD INITIAL DATA
  useEffect(() => {
    const loadData = async () => {
      try {
        setLoadingDoctors(true);

        const doctorRes = await getDoctors();

        const therapyRes =
          await getTherapies();

        setDoctors(doctorRes.data || []);

        setTherapies(therapyRes.data || []);
      } catch (err) {
        console.error(err);

        setError("Failed to load data");
      } finally {
        setLoadingDoctors(false);
      }
    };

    loadData();
  }, []);

  // FETCH SLOTS
  useEffect(() => {
    const loadSlots = async () => {
      if (!canFetchSlots) return;

      try {
        setLoadingSlots(true);

        const res = await getSlotsByDoctor(
          selectedDoctorId,
          selectedDate
        );

        setSlots(res.data || []);

        setSelectedSlot(null);
      } catch (err) {
        console.error(err);

        setError("Failed to load slots");
      } finally {
        setLoadingSlots(false);
      }
    };

    loadSlots();
  }, [
    selectedDoctorId,
    selectedDate,
    canFetchSlots,
  ]);

  // BOOK APPOINTMENT
  const handleBook = async () => {
    setError("");
    setSuccess("");

    if (!selectedDoctorId)
      return setError("Select doctor");

    if (!selectedTherapyId)
      return setError("Select therapy");

    if (!selectedSlot)
      return setError("Select slot");

    if (!firstName.trim())
      return setError("First name required");

    if (!lastName.trim())
      return setError("Last name required");

    try {
      setBooking(true);

      const payload = {
        doctorId: Number(selectedDoctorId),

        therapyId: Number(
          selectedTherapyId
        ),

        slotId: selectedSlot.slotId,

        firstName: firstName,

        lastName: lastName,

        notes: "Walk-in appointment",
      };

      await API.post(
        "/appointment/walkin",
        payload
      );

      setSuccess(
        "Appointment booked successfully"
      );

      setFirstName("");
      setLastName("");
      setSelectedSlot(null);
  

      // REFRESH SLOTS
      const res = await getSlotsByDoctor(
        selectedDoctorId,
        selectedDate
      );

      setSlots(res.data || []);
    } catch (err) {
      console.error(err);

      setError(
        err?.response?.data?.message ||
          "Booking failed"
      );
    } finally {
      setBooking(false);
    }
  };

  return (
    <DashboardLayout>
      {/* HEADER */}
      <div className="d-flex justify-content-between align-items-center mb-4">
        <div>
          <h2 className="text-emerald fw-bold">
            Book Appointment
          </h2>

          <p className="text-secondary mb-0">
            Create walk-in appointments for
            patients
          </p>
        </div>
      </div>

      {/* ALERTS */}
      {error && (
        <div className="alert alert-danger">
          {error}
        </div>
      )}

      {success && (
        <div className="alert alert-success">
          {success}
        </div>
      )}

      {/* FORM */}
      <div className="card-dark p-4 mb-4">
        <div className="row">
          {/* DOCTOR */}
          <div className="col-md-6 mb-3">
            <label className="form-label">
              Select Doctor
            </label>

            <select
              className="form-select"
              value={selectedDoctorId}
              onChange={(e) =>
                setSelectedDoctorId(
                  e.target.value
                )
              }
              disabled={loadingDoctors}
            >
              <option value="">
                Select Doctor
              </option>

              {doctors.map((doctor) => (
                <option
                  key={doctor.doctorId}
                  value={doctor.doctorId}
                >
                  Dr. {doctor.fullName}
                </option>
              ))}
            </select>
          </div>

          {/* THERAPY */}
          <div className="col-md-6 mb-3">
            <label className="form-label">
              Select Therapy
            </label>

            <select
              className="form-select"
              value={selectedTherapyId}
              onChange={(e) =>
                setSelectedTherapyId(
                  e.target.value
                )
              }
            >
              <option value="">
                Select Therapy
              </option>

              {therapies.map((therapy) => (
                <option
                  key={therapy.therapyId}
                  value={therapy.therapyId}
                >
                  {therapy.name}
                </option>
              ))}
            </select>
          </div>

          {/* DATE */}
          <div className="col-md-6 mb-3">
            <label className="form-label">
              Select Date
            </label>

            <input
              type="date"
              className="form-control"
              value={selectedDate}
              onChange={(e) =>
                setSelectedDate(
                  e.target.value
                )
              }
            />
          </div>

          {/* FIRST NAME */}
          <div className="col-md-6 mb-3">
            <label className="form-label">
              First Name
            </label>

            <input
              type="text"
              className="form-control"
              placeholder="Enter first name"
              value={firstName}
              onChange={(e) =>
                setFirstName(
                  e.target.value
                )
              }
            />
          </div>

          {/* LAST NAME */}
          <div className="col-md-6 mb-3">
            <label className="form-label">
              Last Name
            </label>

            <input
              type="text"
              className="form-control"
              placeholder="Enter last name"
              value={lastName}
              onChange={(e) =>
                setLastName(
                  e.target.value
                )
              }
            />
          </div>
        </div>
      </div>

      {/* SLOTS */}
      <div className="card-dark p-4">
        <h5 className="text-emerald mb-4">
          Available Slots
        </h5>

        {!selectedDoctorId ? (
          <p className="text-secondary">
            Select doctor to view slots
          </p>
        ) : loadingSlots ? (
          <p>Loading slots...</p>
        ) : slots.length === 0 ? (
          <p className="text-secondary">
            No slots available
          </p>
        ) : (
          <div className="d-flex flex-wrap gap-2 mb-4">
            {slots.map((slot) => {
              const isBooked =
                slot.isBooked;

              const selected =
                selectedSlot?.slotId ===
                slot.slotId;

              return (
                <button
                  key={slot.slotId}
                  type="button"
                  disabled={isBooked}
                  onClick={() =>
                    setSelectedSlot(slot)
                  }
                  className={`btn btn-sm ${
                    isBooked
                      ? "btn-danger"
                      : selected
                      ? "btn-primary"
                      : "btn-emerald"
                  }`}
                >
                  {slot.startTime} -{" "}
                  {slot.endTime}
                </button>
              );
            })}
          </div>
        )}

        <button
          className="btn btn-emerald"
          disabled={
            booking || !selectedSlot
          }
          onClick={handleBook}
        >
          {booking
            ? "Booking..."
            : "Book Appointment"}
        </button>
      </div>
    </DashboardLayout>
  );
};

export default BookAppointment;