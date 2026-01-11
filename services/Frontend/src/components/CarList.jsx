import React from 'react';
import CarCard from './CarCard';
import './CarList.css';

export default function CarList({ cars, showStatus = true }) {
  return (
    <div className="car-list">
      {cars.map((car) => (
        <CarCard key={car.id} car={car} showStatus={showStatus} />
      ))}
    </div>
  );
}