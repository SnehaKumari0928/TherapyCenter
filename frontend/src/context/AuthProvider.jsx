import React, { useEffect, useState } from "react";
import { AuthContext } from "./AuthContext";

const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);

 useEffect(() => {
  try {
    const storedUser = localStorage.getItem("user");

    if (storedUser && storedUser !== "undefined") {
      const parsedUser = JSON.parse(storedUser);
      if (parsedUser && parsedUser.role) {
        setUser(parsedUser);
      } else {
        setUser(null);
      }
    } else {
      setUser(null);
    }
    console.log(storedUser);
    
  } catch (error) {
    console.error("Corrupted user in localStorage:", error);
    localStorage.removeItem("user"); // cleanup bad data
    setUser(null);
  }
}, []);

  const login = (data) => {
    localStorage.setItem("token", data.token);
    localStorage.setItem("user", JSON.stringify(data));
    setUser(data);
  };

  const logout = () => {
    localStorage.clear();
    setUser(null);
  };

  return (
    <AuthContext.Provider value={{ user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthProvider;