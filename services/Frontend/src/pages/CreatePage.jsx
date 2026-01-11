import './CreatePage.css';
import { useState } from 'react';
import { useNavigate } from "react-router-dom";
import ThemeToggle from '../components/ThemeToggle';
import { useTheme } from '../context/ThemeContext';
import { createCar } from '../services/CarService';

export default function CreatePage() {
  const navigate = useNavigate();
  const { theme } = useTheme();
  
  const handleSubmit = async (e) => {
    e.preventDefault();
    await createCar({ mark, price, model, description, year, mileage });
    navigate('/');
  }

  const [mark, setMark] = useState('');
  const [model, setModel] = useState('');
  const [description, setDescription] = useState('');
  const [price, setPrice] = useState('');
  const [year, setYear] = useState('');
  const [mileage, setMileage] = useState('');

  return (
    <div className="create-page">
      <nav className="nav-header">
        <a href="/" className="nav-logo">CarMarket</a>
        <div className="nav-links">
          <a href="/" className="nav-link">Home</a>
          <a href="/create" className="nav-link active">Add Listing</a>
          <a href="/profile" className="nav-link">Profile</a>
          <ThemeToggle />
        </div>
      </nav>

      <div className="create-container">
        <div className="create-form">
          <h2 className="create-title">Create Listing</h2>
          <form className="form" onSubmit={handleSubmit}>
            <div className="form-group">
              <label htmlFor="car-mark" className="form-label">Car Brand</label>
              <input
                id="car-mark"
                type="text"
                value={mark}
                onChange={e => setMark(e.target.value)}
                placeholder="Enter brand"
                className="form-input"
                required
              />
            </div>

            <div className="form-group">
              <label htmlFor="car-model" className="form-label">Car Model</label>
              <input
                id="car-model"
                type="text"
                value={model}
                onChange={e => setModel(e.target.value)}
                placeholder="Enter model"
                className="form-input"
                required
              />
            </div>

            <div className="form-group">
              <label htmlFor="car-description" className="form-label">Description</label>
              <textarea 
                className="form-input"
                id="car-description"
                value={description}
                onChange={e => setDescription(e.target.value)}
                placeholder="Enter description"
                rows="4"
                required
              />
            </div>

            <div className="form-group">
              <label htmlFor="car-price" className="form-label">Price</label>
              <input
                id="car-price"
                type="number"
                value={price}
                onChange={e => setPrice(e.target.value)}
                placeholder="Enter price"
                className="form-input"
                required
              />
            </div>

            <div className="form-group">
              <label htmlFor="car-year" className="form-label">Year</label>
              <input
                id="car-year"
                type="number"
                value={year}
                onChange={e => setYear(e.target.value)}
                placeholder="Enter manufacturing year"
                className="form-input"
                min="1900"
                max="2035"
                required
              />
            </div>

            <div className="form-group">
              <label htmlFor="car-mileage" className="form-label">Mileage (km)</label>
              <input
                id="car-mileage"
                type="number"
                value={mileage}
                onChange={e => setMileage(e.target.value)}
                placeholder="Enter mileage"
                className="form-input"
                min="0"
                step="1"
                required
              />
            </div>

            <button type="submit" className="create-btn">Create Listing</button>
            <button type="button" className="back-btn" onClick={() => navigate('/')}>
              Back
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}
