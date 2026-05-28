import API from "./api";

export const createSlot = (data) => {
    return API.post("/slot", data);
};

export const getSlots = () => {
    return API.get("/slot");
};

export const getSlotById = (id) => {
    return API.get(`/slot/${id}`);
};

export const updateSlot = (id, data) => {
    return API.put(`/slot/${id}`, data);
};

export const deleteSlot = (id) => {
    return API.delete(`/slot/${id}`);
};

// doctorId removed
export const getSlotsByDoctor = (date) => {
    console.log("API HIT:", date);

    return API.get("/slot/doctor", {
        params: { date }
    });
};

export const generateSlot = (data) => {
    return API.post("/slot/bulk", data);
};

export const getGeneratedSlotsByDoctor = (date) => {
    return API.get("/slot/generated", {
        params: { date }
    });
};