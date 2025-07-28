import React from "react";
import CarCard from "./CarCard.jsx";
import './CarList.css';

export const CarList = ({ cars }) => (
    <div className="car-list">
        {cars.map(car => (
            <CarCard key={car.id} car={car}/>
        ))}
    </div>
);
export default CarList;