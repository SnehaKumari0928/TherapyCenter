
import API from "./api";

export const createSlot = (data)=>{
    return API.post("/slot/createslot",data);
}

export const getslots = ()=>{
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
  return API.get(`/slot/doctor/${doctorId}?date=${date}`);
};
