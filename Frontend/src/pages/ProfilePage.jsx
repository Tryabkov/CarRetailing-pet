import React, { useState, useEffect } from 'react';
import './ProfilePage.css';
import EditProfileForm from '../components/EditProfileForm';
import ThemeToggle from '../components/ThemeToggle';
import { useTheme } from '../context/ThemeContext';

export default function ProfilePage() {
  const [userProfile, setUserProfile] = useState(null);
  const [userListings, setUserListings] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showEditForm, setShowEditForm] = useState(false);
  const [editingProfile, setEditingProfile] = useState(null);
  const { theme } = useTheme();

  useEffect(() => {
    loadUserData();
  }, []);

  const loadUserData = async () => {
    setLoading(true);
    const profile = {
      id: 1,
      name: "Timothy Ryabkov",
      username: "Tryabkov",
      bio: "Mr. Pinochet's helicopter",
      avatar: "/avatar.jpg",
      email: "timothy@example.com",
      location: "Moscow, Russia",
      joinDate: "2023-01-15"
    };
    
    setUserProfile(profile);
    setUserListings([]);
    setLoading(false);
  };

  const handleEditProfile = () => {
    setEditingProfile(userProfile);
    setShowEditForm(true);
  };

  const handleSaveProfile = async (updatedData) => {
    setUserProfile({ ...userProfile, ...updatedData });
    setShowEditForm(false);
    setEditingProfile(null);
  };

  const formatPrice = (price) => {
    return new Intl.NumberFormat('ru-RU', {
      style: 'currency',
      currency: 'RUB',
      minimumFractionDigits: 0
    }).format(price);
  };

  const formatDate = (dateString) => {
    return new Date(dateString).toLocaleDateString('ru-RU');
  };

  if (loading) {
    return (
      <div className="profile-page loading">
        <div className="loading-container">
          <div className="loading-spinner"></div>
          <p>Loading profile...</p>
        </div>
      </div>
    );
  }

  return (
    <div className="profile-page">
      <nav className="nav-header">
        <a href="/" className="nav-logo">CarMarket</a>
        <div className="nav-links">
          <a href="/" className="nav-link">Home</a>
          <a href="/create" className="nav-link">Add Listing</a>
          <a href="/profile" className="nav-link active">Profile</a>
          <ThemeToggle />
        </div>
      </nav>

      <div className="main-content">
        <div className="profile-container">
          <div className="profile-sidebar">
          {showEditForm ? (
            <div className="edit-form-wrapper">
              <EditProfileForm
                userData={editingProfile}
                onSave={handleSaveProfile}
                onCancel={() => setShowEditForm(false)}
              />
            </div>
          ) : (
            <div className="profile-card">
              <div className="avatar-container">
                <img
                  src={userProfile?.avatar || "https://via.placeholder.com/120x120/6b7280/ffffff?text=Avatar"}
                  alt="Avatar"
                  className="profile-avatar"
                />
                <div className="avatar-badge">3</div>
              </div>

              <div className="profile-info">
                <h1 className="profile-name">{userProfile?.name}</h1>
                <p className="profile-username">{userProfile?.username} · {userProfile?.bio}</p>
              </div>

              <button className="edit-profile-btn" onClick={handleEditProfile}>
                Edit profile
              </button>
            </div>
          )}
          </div>

          <div className="listings-section">
            <div className="section-header">
              <h2 className="section-title">My Listings</h2>
              <button className="customize-btn">Customize</button>
            </div>

            <div className="listings-grid">
              {userListings.map((listing) => (
                <div key={listing.id} className="listing-card">
                  <div className="listing-image">
                    <img src={listing.image} alt={listing.title} />
                    <div className={`listing-status ${listing.status}`}>
                      {listing.status === 'sold' ? 'Sold' : 'Active'}
                    </div>
                  </div>

                  <div className="listing-content">
                    <h3 className="listing-title">{listing.title}</h3>
                    {listing.description && (
                      <p className="listing-description">{listing.description}</p>
                    )}

                    <div className="listing-meta">
                      <span className="listing-price">{formatPrice(listing.price)}</span>
                      <span className="listing-date">{formatDate(listing.createdAt)}</span>
                    </div>
                  </div>

                  <button className="listing-menu-btn">⋮</button>
                </div>
              ))}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
