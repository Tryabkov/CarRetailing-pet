import axios from 'axios';
const API_URL = import.meta.env.VITE_API_URL;

export async function getCars() {
  const cars = axios.get(`${API_URL}/api/cars/all`, {
    withCredentials: true
  });
  return (await cars).data;
}

