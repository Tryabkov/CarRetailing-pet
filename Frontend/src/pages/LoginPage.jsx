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
    <div className="login-page">
      {errorState ? <div className="error-box">{errorState}</div> : null}
      <div className="login-card">
        <h2 className="login-title">Log In</h2>
        <form className="login-form" onSubmit={handleSubmit}>
          <div className="input-group">
            <label htmlFor="email">Email</label>
            <input
              id="email"
              autoComplete="email"
              type="text"
              placeholder="Enter email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
          </div>
          <div className="input-group">
            <label htmlFor="password">Password</label>
            <input
              id="password"
              type="password"
              placeholder="Enter password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </div>
          <button type="submit" className="btn btn-primary">
            Enter
          </button>
          <button type="button" className="btn btn-secondary" onClick={onBack}>
            Back
          </button>
          <button
            type="button"
            className="btn btn-redirect-signin"
            onClick={onRegister}
          >
            Register
          </button>
        </form>
      </div>
    </div>
  );
};

export default LoginPage;
