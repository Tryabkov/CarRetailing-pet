import React from 'react';
import './CarCard.css';

export default function CarCard({ car }) {
  return (
    <div className="car-card">
      <img
        src={car.image}
        alt={car.model}
        className="car-image"
      />
      <div className="car-info">
        <h2 className="car-model">{car.model}</h2>
        <p className="car-description">{car.description}</p>
        <div className="car-footer">
          <span className="car-price">
            {car.price.toLocaleString('en-EN')} $
          </span>
          <button className="car-button">Подробнее</button>
        </div>
      </div>
    </div>
  );
}
