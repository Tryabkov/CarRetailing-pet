import React, { createContext, useState, useEffect } from 'react';
import axios from 'axios';

const API_URL = import.meta.env.VITE_API_URL;

export const AuthContext = createContext({
  isLoggedIn: false,
  setIsLoggedIn: () => {},
  logout: () => {}
});

export function AuthProvider({ children }) {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  
    useEffect(() => {
    axios.get(`${API_URL}/auth/status`, { withCredentials: true })
      .then(() => setIsLoggedIn(true))
      .catch(() => setIsLoggedIn(false));
  }, []);
  
    const logout = () => {
      axios.post(`${API_URL}/auth/logout`, {}, { withCredentials: true })
        .finally(() => setIsLoggedIn(false))}

   return (
    <AuthContext.Provider value={{ isLoggedIn, setIsLoggedIn, logout }}>
      {children}
    </AuthContext.Provider>
  );
}