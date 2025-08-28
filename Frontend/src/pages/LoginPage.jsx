import "./LoginPage.css";
import axios from "axios";
import React, { useState, useContext } from "react";
import { useNavigate } from "react-router-dom";
import { AuthProvider, AuthContext } from "../auth/AuthContext";

const API_URL = import.meta.env.VITE_API_URL;

const LoginPage = ({ onBack, onRegister }) => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorState, setError] = useState("");
  const { setIsLoggedIn } = useContext(AuthContext);
  const navigate = useNavigate();

  const handleSubmit = async (event) => {
    event.preventDefault();
    setError("");

    try {
      await axios
        .post(`${API_URL}/auth/login`, {
          email,
          password,
        }, { withCredentials: true })
        .then(() => {
          setIsLoggedIn(true);
          navigate("/");
        })
        .catch((error) => {
          if (error.response.status === 400) {
            setError("Wrong email or password");
          }
        });
    } catch {
      setError("Server error");
    }
  };
  return (
    <div className="login-container">
      <form className="login-form" onSubmit={handleSubmit}>
        <h2 className="login-title">Log In</h2>
        {errorState ? <div className="error-message">{errorState}</div> : null}

        <div className="form-group">
          <label className="form-label" htmlFor="email">Email</label>
          <input
            id="email"
            className="form-input"
            autoComplete="email"
            type="email"
            placeholder="Enter email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>

        <div className="form-group">
          <label className="form-label" htmlFor="password">Password</label>
          <input
            id="password"
            className="form-input"
            type="password"
            placeholder="Enter password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>

        <button type="submit" className="login-btn">Log In</button>
        <button type="button" className="back-btn" onClick={onBack}>Back</button>

        <div className="register-link">
          Don't have an account?{' '}
          <a href="#" onClick={(e) => { e.preventDefault(); onRegister && onRegister(); }}>Register</a>
        </div>
      </form>
    </div>
  );
};

export default LoginPage;
