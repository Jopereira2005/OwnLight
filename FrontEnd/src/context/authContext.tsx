import React, { createContext, useState, useContext } from 'react';
import { forLogin } from '../services/server';
import { User } from '../interfaces/User';
import { useNavigate } from 'react-router-dom';

interface AuthContextType {
  user: User | null;
  loading: boolean; // Indica se uma ação de autenticação está em andamento
  error: string | null; // Armazena mensagens de erro de autenticação
  login: (credentials: { username: string; password: string }) => Promise<void>;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [user, setUser] = useState<User | null>(() => {
    const storedUser = localStorage.getItem('user');
    return storedUser ? JSON.parse(storedUser) : null;
  });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const navigate = useNavigate();

  const login = async (credentials: { username: string; password: string }) => {
    setLoading(true);
    setError(null);
    try {
      const response: any = await forLogin.post('/Auth/login', credentials);

      if(response.status != 200)
        throw response
      
      const accessToken = (response.data as { accessToken: string }).accessToken;

      localStorage.setItem('accessToken', accessToken);
      localStorage.setItem('user_id', response.data.userData.id);
      localStorage.setItem('user', JSON.stringify(response.data.userData));
      setUser(response.data.userData);

      return response
    } catch (err: any) {
      setError(err.response.data.content);
      return err.response
    } finally {
      setLoading(false);
    }

  };

  const logout = () => {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('user');
    setUser(null);
    navigate('/login');
  };

  return (
    <AuthContext.Provider value={{ user, loading, error, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth tem que ser usado com AuthProvider');
  }
  return context;
};
