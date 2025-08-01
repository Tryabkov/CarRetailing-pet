import './CreatePage.css';
import { use, useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
import axios from 'axios';
import { AuthProvider, AuthContext } from '../auth/AuthContext';

const API_URL = import.meta.env.VITE_API_URL;

export default function CreatePage() {
  const navigate = useNavigate();
  const handleSubmit = async (e) => {
    e.preventDefault()
    // setError('')

    axios.post(`${API_URL}/api/cars`, { mark, price, model, description}, { withCredentials: true })
    navigate('/')
  }

  const [mark, setMark] = useState('')
  const [model, setModel] = useState('')
  const [description, setDescription] = useState('')
  const [price, setPrice] = useState('')
  // const [error, setError] = useState('')

  return (
    <div className="register-page">
      <div className="register-card">
        <h2 className="register-title">Create Car</h2>
        <form className="register-form" onSubmit={handleSubmit}>
          {/* {error && <div className="error-box">{error}</div>} */}

          <div className="input-group">
            <label htmlFor="reg-username">Car mark</label>
            <input
              id="reg-username"
              type="text"
              value={mark}
              onChange={e => setMark(e.target.value)}
              placeholder="Enter mark"
              required
            />
          </div>

          <div className="input-group">
            <label htmlFor="reg-email">Car model</label>
            <input
              id="reg-email"
              type="text"
              value={model}
              onChange={e => setModel(e.target.value)}
              placeholder="Enter model"
              required
            />
          </div>


          <div className="input-group">
            <label htmlFor="reg-confirm">Description</label>
            <textarea className='textarea-description'
              id="reg-confirm"
              type="password"
              value={description}
              onChange={e => setDescription(e.target.value)}
              placeholder="Enter description"
              required
            />
          </div>

          <div className="input-group">
            <label htmlFor="reg-password">Price</label>
            <input
              id="reg-password"
              type="number"
              value={price}
              onChange={e => setPrice(e.target.value)}
              placeholder="Enter price"
              required
            />
          </div>

          <button type="submit" className="btn btn-primary">Create</button>
          {/* <button type="button" className="btn btn-secondary" onClick={onBack}>Back</button>
          <button type="button" className="btn btn-redirect-login" onClick={onLogin}>Register</button> */}
        </form>
      </div>
    </div>
  );
}
