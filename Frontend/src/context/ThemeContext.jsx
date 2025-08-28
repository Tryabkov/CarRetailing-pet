import React, { createContext, useContext, useState, useEffect } from 'react';

const ThemeContext = createContext();

export const useTheme = () => {
  const context = useContext(ThemeContext);
  if (!context) {
    throw new Error('useTheme must be used within a ThemeProvider');
  }
  return context;
};

export const ThemeProvider = ({ children }) => {
  const [theme, setTheme] = useState(() => {
    const savedTheme = localStorage.getItem('theme');
    return savedTheme || 'light';
  });

  const themes = {
    light: {
      // Primary colors
      primary: '#3b82f6',
      primaryHover: '#2563eb',
      secondary: '#64748b',
      accent: '#f59e0b',
      
      // Backgrounds
      background: '#ffffff',
      surface: '#f8fafc',
      surfaceHover: '#f1f5f9',
      card: '#ffffff',
      cardHover: '#f8fafc',
      
      // Borders
      border: '#e2e8f0',
      borderHover: '#cbd5e1',
      borderFocus: '#3b82f6',
      
      // Text
      textPrimary: '#1e293b',
      textSecondary: '#64748b',
      textMuted: '#94a3b8',
      textInverse: '#ffffff',
      
      // Status colors
      success: '#10b981',
      error: '#ef4444',
      warning: '#f59e0b',
      info: '#3b82f6',
      
      // Shadows
      shadow: '0 1px 3px 0 rgba(0, 0, 0, 0.1), 0 1px 2px 0 rgba(0, 0, 0, 0.06)',
      shadowHover: '0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06)',
      shadowLarge: '0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05)',
      
      // Gradients
      gradientPrimary: 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)',
      gradientSecondary: 'linear-gradient(135deg, #f093fb 0%, #f5576c 100%)',
      
      // Overlays
      overlay: 'rgba(0, 0, 0, 0.5)',
      overlayLight: 'rgba(0, 0, 0, 0.1)',
    },
    dark: {
      // Primary colors
      primary: '#58a6ff',
      primaryHover: '#79c0ff',
      secondary: '#8b949e',
      accent: '#fbbf24',
      
      // Backgrounds
      background: '#0d1117',
      surface: '#161b22',
      surfaceHover: '#21262d',
      card: '#21262d',
      cardHover: '#30363d',
      
      // Borders
      border: '#30363d',
      borderHover: '#484f58',
      borderFocus: '#58a6ff',
      
      // Text
      textPrimary: '#ffffff',
      textSecondary: '#8b949e',
      textMuted: '#6e7681',
      textInverse: '#0d1117',
      
      // Status colors
      success: '#238636',
      error: '#da3633',
      warning: '#d29922',
      info: '#58a6ff',
      
      // Shadows
      shadow: '0 1px 3px 0 rgba(0, 0, 0, 0.3), 0 1px 2px 0 rgba(0, 0, 0, 0.2)',
      shadowHover: '0 4px 6px -1px rgba(0, 0, 0, 0.3), 0 2px 4px -1px rgba(0, 0, 0, 0.2)',
      shadowLarge: '0 10px 15px -3px rgba(0, 0, 0, 0.3), 0 4px 6px -2px rgba(0, 0, 0, 0.2)',
      
      // Gradients
      gradientPrimary: 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)',
      gradientSecondary: 'linear-gradient(135deg, #f093fb 0%, #f5576c 100%)',
      
      // Overlays
      overlay: 'rgba(0, 0, 0, 0.7)',
      overlayLight: 'rgba(0, 0, 0, 0.3)',
    }
  };

  const currentTheme = themes[theme];

  const toggleTheme = () => {
    const newTheme = theme === 'light' ? 'dark' : 'light';
    setTheme(newTheme);
    localStorage.setItem('theme', newTheme);
  };

  useEffect(() => {
    // Apply CSS variables to root element
    const root = document.documentElement;
    Object.entries(currentTheme).forEach(([key, value]) => {
      root.style.setProperty(`--${key}`, value);
    });
    
    // Add class to body for additional styling
    document.body.className = `theme-${theme}`;
  }, [theme, currentTheme]);

  const value = {
    theme,
    toggleTheme,
    currentTheme,
    isDark: theme === 'dark',
    isLight: theme === 'light'
  };

  return (
    <ThemeContext.Provider value={value}>
      {children}
    </ThemeContext.Provider>
  );
};
