
import API from "./api";

export const createUser = (data)=>{
    return API.post("/user",data);
}

export const getAllUsers = ()=>{
    return API.get("/user")
}

export const getUserById = (id)=>{
    return API.get(`/user/${id}`)
}

export const updateUser = (id,data)=>{
    return API.put(`/user/${id}`,data)
}

export const deleteUser = (id)=>{
    return API.delete(`/user/${id}`)
}

