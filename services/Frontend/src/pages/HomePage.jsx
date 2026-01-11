
import React, { useContext, useState, useEffect, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthContext } from '../auth/AuthContext';
import ThemeToggle from '../components/ThemeToggle';
import { useTheme } from '../context/ThemeContext';
import CarList from '../components/CarList';
import { getCars, searchCars } from '../services/CarService';
import './HomePage.css';

export default function HomePage() {
  const { isLoggedIn, logout } = useContext(AuthContext);
  const [cars, setCars] = useState([]);
  const [loading, setLoading] = useState(true);
  const [searchQuery, setSearchQuery] = useState('');
  const [filters, setFilters] = useState({ 
    mark: '', 
    model: '', 
    minPrice: '', 
    maxPrice: '', 
    year: '' 
  });
  const navigate = useNavigate();
  const { theme } = useTheme();
  
  const loadCars = useCallback(async (currentFilters) => {
    setLoading(true);
    try {
      const data = await getCars(currentFilters);
      setCars(data);
    } catch (error) {
      console.error('Error loading cars:', error);
      setCars([]);
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    const timeoutId = setTimeout(() => {
      loadCars(filters);
    }, 500); // 500ms debounce

    return () => clearTimeout(timeoutId);
  }, [filters, loadCars]);

  const handleSearch = async (e) => {
    e.preventDefault();
    if (searchQuery.trim()) {
      setLoading(true);
      try {
        const results = await searchCars(searchQuery);
        setCars(results);
      } catch (error) {
        console.error('Error searching cars:', error);
        setCars([]);
      } finally {
        setLoading(false);
      }
    } else {
      loadCars(filters);
    }
  };

  const handleFilterChange = (field, value) => {
    // For numeric fields, check that only numbers or empty string are entered
    if (['minPrice', 'maxPrice', 'year'].includes(field)) {
      if (value === '' || !isNaN(value)) {
        setFilters(prev => ({ ...prev, [field]: value }));
      }
    } else {
      setFilters(prev => ({ ...prev, [field]: value }));
    }
  };

  const clearFilters = () => {
    setFilters({ mark: '', model: '', minPrice: '', maxPrice: '', year: '' });
    setSearchQuery('');
  };

  const handleLogout = () => {
    logout();
    navigate('/');
  };

  return (
    <div className="home-page">
      <header className="navbar">
        <div className="nav-left">
          <h1 className="logo">CarMarket</h1>
        </div>
        
        <div className="search-container">
          <form onSubmit={handleSearch} className="search-form">
            <input
              type="text"
              placeholder="Search cars by brand, model, or description..."
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
              className="search-input"
            />
            <button type="submit" className="search-btn">Search</button>
          </form>
        </div>
        
        <nav className="nav-right">
          <ThemeToggle />
          {isLoggedIn ? (
            <>
              <button className="nav-btn" onClick={() => navigate('/profile')}>Profile</button>
              <button className="nav-btn" onClick={() => navigate('/create')}>Add Listing</button>
              <button className="nav-btn" onClick={handleLogout}>Logout</button>
            </>
          ) : (
            <button className="nav-btn" onClick={() => navigate('/login')}>Login</button>
          )}
        </nav>
      </header>

      <main className="content">
        <section className="hero-section">
          <div className="hero-content">
            <h2 className="hero-title">Find Your Perfect Car</h2>
            <p className="hero-description">
              Browse thousands of cars from trusted sellers. Find the perfect vehicle for your needs.
            </p>
            <div className="hero-stats">
              <div className="stat-item">
                <span className="stat-number">1,234</span>
                <span className="stat-label">Cars Available</span>
              </div>
              <div className="stat-item">
                <span className="stat-number">567</span>
                <span className="stat-label">Trusted Sellers</span>
              </div>
              <div className="stat-item">
                <span className="stat-number">98%</span>
                <span className="stat-label">Satisfaction Rate</span>
              </div>
            </div>
          </div>
        </section>

        <section className="filters-section">
          <div className="filters-container">
            <div className="filter-group">
              <label className="filter-label">Brand</label>
              <input
                type="text"
                placeholder="Enter brand..."
                value={filters.mark}
                onChange={(e) => handleFilterChange('mark', e.target.value)}
                className="filter-input"
              />
            </div>
            
            <div className="filter-group">
              <label className="filter-label">Model</label>
              <input
                type="text"
                placeholder="Enter model..."
                value={filters.model}
                onChange={(e) => handleFilterChange('model', e.target.value)}
                className="filter-input"
              />
            </div>

            <div className="filter-group">
              <label className="filter-label">Price ($)</label>
              <div className="price-inputs">
                <input
                  type="number"
                  placeholder="Min"
                  value={filters.minPrice}
                  onChange={(e) => handleFilterChange('minPrice', e.target.value)}
                  className="price-input"
                  min="0"
                />
                <span className="price-separator">-</span>
                <input
                  type="number"
                  placeholder="Max"
                  value={filters.maxPrice}
                  onChange={(e) => handleFilterChange('maxPrice', e.target.value)}
                  className="price-input"
                  min="0"
                />
              </div>
            </div>

            <div className="filter-group">
              <label className="filter-label">Year</label>
              <input
                type="number"
                placeholder="From year..."
                value={filters.year}
                onChange={(e) => handleFilterChange('year', e.target.value)}
                className="filter-input"
                min="1900"
                max="2030"
              />
            </div>
          </div>
        </section>

        <section className="cars-section">
          <div className="section-header">
            <h3 className="section-title">
              {loading ? 'Loading cars...' : `${cars.length} cars found`}
            </h3>
            <button className="clear-filters-btn" onClick={clearFilters}>Clear Filters</button>
          </div>
          
          {loading ? (
            <div className="loading-container">
              <div className="loading-spinner"></div>
              <p>Loading cars...</p>
            </div>
          ) : (
            <CarList cars={cars} showStatus={false} />
          )}
        </section>
      </main>
    </div>
  );
}
