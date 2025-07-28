
import React, { useEffect, useState } from 'react';
import { getCars, } from '../services/CarService';
import CarList from "../components/CarList";
import LoginPage from "./LoginPage";
import './HomePage.css';
import { Navigate, useNavigate } from 'react-router-dom';

const HomePage = () => {
  const [cars, setCars] = useState([]);
  const navigate = useNavigate()

  useEffect(() => {
    getCars().then(data => setCars(data));
  }, []);

  return (
    <>
      <div className='header'>
        <button className="login-btn" onClick={() => navigate('/login')}>Enter</button>
      </div>
      <div>
        <h1>Каталог автомобилей</h1>
        <CarList cars={cars} />
      </div>
    </>
  );
};

export default HomePage;
