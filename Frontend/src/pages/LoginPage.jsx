import './LoginPage.css';
// import React, { useState } from 'react';

const LoginPage = ({ onBack, onRegister }) => (
  <div className="login-page">  
    <div className="login-card">
      <h2 className="login-title">Log In</h2>
      <form className="login-form">
        <div className="input-group">
          <label htmlFor="username">Email</label>
          <input id="username" autoComplete='email' type="text" placeholder="Enter email" required />
        </div>
        <div className="input-group">
          <label htmlFor="password">Password</label>
          <input id="password" type="password" placeholder="Enter password" required />
        </div>
        <button type="submit" className="btn btn-primary">Enter</button>
        <button type="button" className="btn btn-secondary" onClick={onBack}>Back</button>
        <button type="button" className="btn btn-redirect-signin" onClick={onRegister}>Register</button>
      </form>
    </div>
  </div>
);

export default LoginPage;