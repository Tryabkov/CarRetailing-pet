import './LoginPage.css';
import React, {useState} from 'react';

const LoginPage = ({ onBack }) => (
  <div className="login-page">
    <div className="login-card">
      <h2 className="login-title">Вход в систему</h2>
      <form className="login-form">
        <div className="input-group">
          <label htmlFor="username">Логин</label>
          <input id="username" type="text" placeholder="Введите логин" required />
        </div>
        <div className="input-group">
          <label htmlFor="password">Пароль</label>
          <input id="password" type="password" placeholder="Введите пароль" required />
        </div>
        <button type="submit" className="btn btn-primary">Войти</button>
        <button type="button" className="btn btn-secondary" onClick={onBack}>Назад</button>
      </form>
    </div>
  </div>
);

export default LoginPage;