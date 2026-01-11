import React, { useState } from 'react';
import axios from 'axios';
import './RegisterPage.css';
import { useNavigate } from 'react-router-dom';

const API_URL = import.meta.env.VITE_API_URL;

const RegisterPage = ({ onBack, onLogin }) => {
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate()

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');

    if (password !== confirmPassword) {
      setError('Passwords do not match');
      return;
    }

    await axios.post(`${API_URL}/auth/signup`, {
      username,
      email,
      password,
    }, {
      withCredentials: true
    })
    .then(() => {
      navigate('/login')
    })
    .catch(error => {
      console.log(error.response.data)
      setError(error.response.data)
    });
  };

  return (
    <div className="register-container">
      <form className="register-form" onSubmit={handleSubmit}>
        <h2 className="register-title">Create Account</h2>
        {error && <div className="error-message">{error}</div>}

        <div className="form-group">
          <label className="form-label" htmlFor="reg-username">Username</label>
          <input
            id="reg-username"
            className="form-input"
            type="text"
            value={username}
            onChange={e => setUsername(e.target.value)}
            placeholder="Enter username"
            required
          />
        </div>

        <div className="form-group">
          <label className="form-label" htmlFor="reg-email">Email</label>
          <input
            id="reg-email"
            className="form-input"
            type="email"
            value={email}
            onChange={e => setEmail(e.target.value)}
            placeholder="Enter email"
            required
          />
        </div>

        <div className="form-group">
          <label className="form-label" htmlFor="reg-password">Password</label>
          <input
            id="reg-password"
            className="form-input"
            type="password"
            value={password}
            onChange={e => setPassword(e.target.value)}
            placeholder="Enter password"
            required
          />
        </div>

        <div className="form-group">
          <label className="form-label" htmlFor="reg-confirm">Confirm Password</label>
          <input
            id="reg-confirm"
            className="form-input"
            type="password"
            value={confirmPassword}
            onChange={e => setConfirmPassword(e.target.value)}
            placeholder="Confirm password"
            required
          />
        </div>

        <button type="submit" className="register-btn">Register</button>
        <button type="button" className="back-btn" onClick={onBack}>Back</button>
        <div className="login-link">
          Already have an account?{' '}
          <a href="#" onClick={(e) => { e.preventDefault(); onLogin && onLogin(); }}>Log in</a>
        </div>
      </form>
    </div>
  );
};

export default RegisterPage;
