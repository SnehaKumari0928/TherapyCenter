
import API from "./api";

export const createDoctor = (data)=>{
    return API.post("/doctor/createdoctor",data);
}

export const getDoctors = ()=>{
    return API.get("/doctor")
}

export const getDoctorsById = (id)=>{
    return API.get(`/doctor/${id}`)
}

export const updateDoctor = (id,data)=>{
    return API.put(`/doctor/${id}`,data)
}

export const deleteDoctor = (id)=>{
    return API.delete(`/doctor/${id}`)
}

