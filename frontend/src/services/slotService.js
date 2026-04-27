
import API from "./api";

export const createSlot = (data)=>{
    return API.post("/slot/createslot",data);
}

export const getslot = ()=>{
    return API.get("/slot")
}

export const getslotById = (id)=>{
    return API.get(`/slot/${id}`)
}

export const updateSlot = (id,data)=>{
    return API.put(`/slot/${id}`,data)
}

export const deleteSlot = (id)=>{
    return API.delete(`/slot/${id}`)
}

export const getSlotsByDoctor = (doctorId, date) => {
  const formattedDate = new Date(date).toISOString().split("T")[0];

  return API.get(`/doctor/${doctorId}?date=${formattedDate}`);
};
