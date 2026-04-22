
import API from "./api";

export const createAppointment = (data)=>{
    return API.post("/appointment/createappointment",data);
}

export const getAppointments = ()=>{
    return API.get("/appointment")
}

export const getAppointmentById = (id)=>{
    return API.get(`/appointment/${id}`)
}

export const updateAppointment = (id,data)=>{
    return API.put(`/appointment/${id}`,data)
}

export const deleteAppointment = (id)=>{
    return API.delete(`appointment/${id}`)
}

export const cancelAppointment = (id)=>{
    return API.put(`/appointment/cancel/${id}`)
}

export const completeAppointment = (id)=>{
    return API.put(`/appointment/complete/${id}`)
}