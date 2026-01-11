import React from 'react';
import './CarCard.css';

export default function CarCard({ car, showStatus = true }) {
  // Format price in Russian currency
  const formatPrice = (price) => {
    return new Intl.NumberFormat('ru-RU', {
      style: 'currency',
      currency: 'RUB',
      minimumFractionDigits: 0
    }).format(price);
  };

  // Format mileage with thousands separator
  const formatMileage = (mileage) => {
    return new Intl.NumberFormat('ru-RU').format(mileage);
  };

  // Get status color and text
  const getStatusInfo = (status) => {
    switch (status) {
      case 'active':
        return { text: 'Active', color: 'success' };
      case 'sold':
        return { text: 'Sold', color: 'error' };
      case 'pending':
        return { text: 'Pending', color: 'warning' };
      default:
        return { text: 'Unknown', color: 'muted' };
    }
  };

  const statusInfo = getStatusInfo(car.status);

  return (
    <div className="car-card">
      {/* Car Image */}
      <div className="car-image-container">
        <img 
          src={car.image} 
          alt={`${car.mark} ${car.model}`} 
          className="car-image"
        />
        {showStatus && (
          <div className={`car-status ${statusInfo.color}`}>
            {statusInfo.text}
          </div>
        )}
      </div>

      {/* Car Information */}
      <div className="car-content">
        <h3 className="car-title">
          {car.mark} {car.model}
        </h3>
        
        <p className="car-description">
          {car.description}
        </p>

        {/* Car Details */}
        <div className="car-details">
          <div className="detail-item">
            <span className="detail-label">Year:</span>
            <span className="detail-value">{car.year}</span>
          </div>
          <div className="detail-item">
            <span className="detail-label">Mileage:</span>
            <span className="detail-value">{formatMileage(car.mileage)} km</span>
          </div>
          {car.seller && (
            <div className="detail-item">
              <span className="detail-label">Seller:</span>
              <span className="detail-value">{car.seller.name}</span>
            </div>
          )}
        </div>

        {/* Price and Actions */}
        <div className="car-footer">
          <div className="car-price">
            {formatPrice(car.price)}
          </div>
          
          <div className="car-actions">
            <button className="btn-primary">View Details</button>
            <button className="btn-secondary">Contact Seller</button>
          </div>
        </div>
      </div>
    </div>
  );
}
