import AppRoutes from "../routes/AppRoutes";
import API from "./api";

export const createOrder = (data)=>{
    return API.post("/payment/create_order",data)
}

export const confirmPayment = (data)=>{
    return API.post("/payment/confirm",data)
}

export const getMyPayments = ()=>{
    return API.get("/payment/my")
}