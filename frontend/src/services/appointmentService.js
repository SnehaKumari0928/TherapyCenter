
import API from "./api";

export const createAppointment = (data)=>{
    return API.post("/appointment/createappointment",data);
}

export const getAllAppointments = ()=>{
    return API.get("/appointment")
}
export const getMyAppointments = ()=>{
    return API.get("/appointment/my")
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
    return API.put(`/appointment/${id}/cancel`)
}

export const completeAppointment = (id)=>{
    return API.put(`/appointment/${id}/complete`)
}

export const getDoctorAppointment = ()=>{
    console.log("DOCTOR ENDPOINT HIT");
    return API.get("/appointment/doctor")
}

export const walkIn = (data)=>{
      return API.post("/appointment/walkin",data);
}