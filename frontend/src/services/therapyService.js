
import API from "./api";

export const createTherapy = (data)=>{
    return API.post("/therapy/createtherapy",data);
}

export const getTherapies = ()=>{
    return API.get("/therapy")
}

export const getTherapyById = (id)=>{
    return API.get(`/therapy/${id}`)
}

export const updateTherapy = (id,data)=>{
    return API.put(`/therapy/${id}`,data)
}

export const deleteTherapy = (id)=>{
    return API.delete(`/therapy/${id}`)
}

