import axios from 'axios';

export async function getCars() {
  const cars = axios.get('https://localhost:7138/api/cars')
  return (await cars).data;
}

