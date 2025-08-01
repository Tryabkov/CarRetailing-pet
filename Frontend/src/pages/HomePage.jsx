
import React, { useContext, useState, useEffect } from 'react';
import { getCars, } from '../services/CarService';
import CarList from "../components/CarList";
import LoginPage from "./LoginPage";
import './HomePage.css';
import { useNavigate } from 'react-router-dom';
import { AuthContext } from '../auth/AuthContext';




export default function HomePage() {
  const { isLoggedIn, logout } = useContext(AuthContext);
  const [ cars, setCars ] = useState([]);
  const navigate = useNavigate();
  
  useEffect(() => {
    // updateLogin()
    getCars().then(data => setCars(data));
  }, []);

  console.log(isLoggedIn)
  return (
    <div className="home-page">
      <header className="navbar">
        <div className="nav-left">
          <h1 className="logo">Auto Catalog</h1>
        </div>
        <nav className="nav-right">
          {isLoggedIn ? (
            <>
              <button className="nav-btn" onClick={() => navigate('/profile')}>Profile</button>
              <button className="nav-btn" onClick={() => navigate('/create')}>Создать объявление</button>
              <button className="nav-btn" onClick={() => { logout(); navigate('/'); }}>Exit</button>
            </>
          ) : (
            <button className="nav-btn" onClick={() => navigate('/login')}>Войти</button>
          )}
        </nav>
      </header>

      <main className="content">
        <CarList cars={cars}/>
      </main>
    </div>
  );
}
