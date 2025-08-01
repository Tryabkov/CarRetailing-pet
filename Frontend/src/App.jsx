import axios from 'axios';
axios.defaults.withCredentials = true;

import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import HomePage from './pages/HomePage.jsx'
import LoginPage from './pages/LoginPage.jsx';
import { useNavigate } from 'react-router-dom';
import RegisterPage from './pages/RegisterPage.jsx';
import { AuthProvider } from './auth/AuthContext';
import CreatePage from './pages/CreatePage.jsx';

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

function RegisterWrapper() {
  const navigate = useNavigate();

  const handleBack = () => {
    if (window.history.length > 1){
      return navigate(-1)
    }
    else{
      return navigate('/')
    }
  }

  const handleLogin = () => {
    navigate('/login')
  }

  return(
    <RegisterPage onBack={handleBack} onLogin={handleLogin}/>
  );
}

function App() {
  return (
    <AuthProvider>
      <Router>
        <Routes>
          <Route path="" element={<HomePage/>}/>
          <Route path="login" element={<LoginWrapper/>} />
          <Route path="register" element={<RegisterWrapper/>} />
          <Route path="create" element={<CreatePage/>} />
        </Routes>
      </Router>
    </AuthProvider>
  )
}

export default App


