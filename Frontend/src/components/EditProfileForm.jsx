import React, { useState, useEffect } from "react";
import "./EditProfileForm.css";

export default function EditProfileForm({ userData, onSave, onCancel }) {
  const [editing, setEditing] = useState(false);
  const [name, setName] = useState("");
  const [bio, setBio] = useState("");
  const [email, setEmail] = useState("");
  const [location, setLocation] = useState("");
  
  const [loading, setLoading] = useState(false);

  // Инициализация данных из props
  useEffect(() => {
    if (userData) {
      setName(userData.name || "");
      setBio(userData.bio || "");
      setEmail(userData.email || "");
      setLocation(userData.location || "");
    }
  }, [userData]);

  const toggleEdit = () => setEditing(!editing);
  
  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    
    try {
      const updatedData = {
        name,
        bio,
        email,
        location
      };
      
      if (onSave) {
        await onSave(updatedData);
      } else {
        // Fallback для автономного использования
        setTimeout(() => {
          setLoading(false);
          setEditing(false);
        }, 1000);
      }
    } catch (error) {
      console.error('Ошибка сохранения:', error);
      setLoading(false);
    }
  };

  const handleCancel = () => {
    if (onCancel) {
      onCancel();
    } else {
      setEditing(false);
    }
  };

  // Если компонент используется автономно (без props)
  if (!userData && !onSave) {
    return (
      <div className="edit-profile">
        <div className="avatar-wrap">
          <img 
            src="../../avatar.jpg"
            alt="avatar"
            className="avatar"/>
        </div>

        {!editing ? (
          <div className="default-form">
            <div className="user-info">
              <h1 className="full-name">{name || "Tima"}</h1>
              <p className="bio">{bio || 'Description description description description description description description description description '}</p>
            </div>
            <button className="btn-outline" onClick={toggleEdit}>
              Edit profile
            </button>
          </div>
        ) : (
          <>
            <form className="edit-form" onSubmit={handleSubmit}>
              <div className="input-group">
                <label htmlFor="name-input">Name</label>
                <input
                  id="name-input"
                  autoComplete="name"
                  type="text"
                  placeholder="Enter your name"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                  required/>
              </div>

              <div className="input-group">
                <label htmlFor="bio-input">Bio</label>
                <textarea
                  id="bio-input"
                  placeholder="Tell us about yourself"
                  value={bio}
                  onChange={(e) => setBio(e.target.value)}
                  rows="3"
                  required/>
              </div>
            </form>
          
            <div className="buttons-row">
              <button 
                type="submit" 
                className="btn-primary"
                onClick={handleSubmit}
                disabled={loading}>
                {loading ? 'Saving...' : 'Save'}
              </button>
              <button 
                type="button" 
                className="btn-cancel" 
                onClick={handleCancel}
                disabled={loading}>
                Cancel
              </button>
            </div>
          </>
        )}
      </div>
    );
  }

  // Если компонент используется в ProfilePage
  return (
    <div className="edit-profile">
      <div className="avatar-wrap">
        <img 
          src={userData?.avatar || "https://via.placeholder.com/120x120/6b7280/ffffff?text=Avatar"}
          alt="avatar"
          className="avatar"/>
      </div>

      <form className="edit-form" onSubmit={handleSubmit}>
        <div className="input-group">
          <label htmlFor="name-input">Name</label>
          <input
            id="name-input"
            autoComplete="name"
            type="text"
            placeholder="Enter your name"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required/>
        </div>

        <div className="input-group">
          <label htmlFor="bio-input">Bio</label>
          <textarea
            id="bio-input"
            placeholder="Tell us about yourself"
            value={bio}
            onChange={(e) => setBio(e.target.value)}
            rows="3"
            required/>
        </div>

        <div className="input-group">
          <label htmlFor="email-input">Email</label>
          <input
            id="email-input"
            autoComplete="email"
            type="email"
            placeholder="Enter your email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required/>
        </div>

        <div className="input-group">
          <label htmlFor="location-input">Location</label>
          <input
            id="location-input"
            type="text"
            placeholder="Enter your location"
            value={location}
            onChange={(e) => setLocation(e.target.value)}/>
        </div>
      </form>
    
      <div className="buttons-row">
        <button 
          type="submit" 
          className="btn-primary"
          onClick={handleSubmit}
          disabled={loading}>
          {loading ? 'Saving...' : 'Save'}
        </button>
        <button 
          type="button" 
          className="btn-cancel" 
          onClick={handleCancel}
          disabled={loading}>
          Cancel
        </button>
      </div>
    </div>
  );
}