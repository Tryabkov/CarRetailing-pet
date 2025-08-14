import axios from 'axios';

const API_URL = import.meta.env.VITE_API_URL;

export async function getCars(filters = {}) {
  const params = new URLSearchParams();
  
  if (filters.mark && filters.mark.trim()) {
    params.append('mark', filters.mark.trim());
  }
  
  if (filters.model && filters.model.trim()) {
    params.append('model', filters.model.trim());
  }
  
  if (filters.minPrice && filters.minPrice.trim() && !isNaN(filters.minPrice)) {
    params.append('minPrice', filters.minPrice.trim());
  }
  
  if (filters.maxPrice && filters.maxPrice.trim() && !isNaN(filters.maxPrice)) {
    params.append('maxPrice', filters.maxPrice.trim());
  }
  
  if (filters.year && filters.year.trim() && !isNaN(filters.year)) {
    params.append('year', filters.year.trim());
  }

  const queryString = params.toString();
  const url = queryString ? `${API_URL}/api/cars/all?${queryString}` : `${API_URL}/api/cars/all`;
  
  const response = await axios.get(url, { withCredentials: true });
  return response.data;
}

export async function getCarById(id) {
  const response = await axios.get(`${API_URL}/api/cars/${id}`, { withCredentials: true });
  return response.data;
}

export async function searchCars(query) {
  const response = await axios.get(`${API_URL}/api/cars/search?q=${encodeURIComponent(query)}`, { withCredentials: true });
  return response.data;
}

export async function createCar(carData) {
  const response = await axios.post(`${API_URL}/api/cars`, carData, { withCredentials: true });
  return response.data;
}

export async function updateCar(id, carData) {
  const response = await axios.put(`${API_URL}/api/cars/${id}`, carData, { withCredentials: true });
  return response.data;
}

export async function deleteCar(id) {
  const response = await axios.delete(`${API_URL}/api/cars/${id}`, { withCredentials: true });
  return response.data;
}

