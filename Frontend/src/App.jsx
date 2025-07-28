import { useState } from 'react'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import HomePage from './pages/HomePage.jsx'
import LoginPage from './pages/LoginPage.jsx';
import { useNavigate } from 'react-router-dom';

function LoginWrapper() {
  const navigate = useNavigate();
  
  const handleBack = () => {
    if (window.history.length > 1){
      return navigate(-1)
    }
    else{
      return navigate('/')
    }
  }

    const handleRegister = () => {
    navigate('/register')
  }


  return (
    <LoginPage onBack={handleBack} onRegister={handleRegister} />
  );
}

function App() {
  return (
    <Router>
      <Routes>
        <Route path="" element={<HomePage/>}/>
        <Route path="login" element={<LoginWrapper/>} />
        <Route path="register" element={<LoginWrapper/>} />
      </Routes>
    </Router>
  )
}

export default App


