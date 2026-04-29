
import API from "./api";

export const createDoctorFinding = (data)=>{
    return API.post("/doctorfinding/create_finding",data);
}

export const getDoctorFindigs = ()=>{
    return API.get("/doctorfinding")
}

export const getDoctorFindingById = (id)=>{
    return API.get(`/doctorfinding/${id}`)
}

export const getMyReports = ()=>{
    return API.get("/doctorfinding/my-report")
}

export const getByAppointmentId = (id)=>{
    return API.get(`/doctorfinding/appointment/${id}`)
}

export const updateDoctorFinding = (id,data)=>{
    return API.put(`/doctorfinding/${id}`,data)
}

export const deleteDoctorFinding = (id)=>{
    return API.delete(`/doctorfinding/${id}`)
}

