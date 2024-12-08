import React, { createContext, useState, useContext } from 'react';
import { api, forLogin } from '../services/server';
import { User } from '../interfaces/User';

interface AuthContextType {
  user: User | null;
  loading: boolean; // Indica se uma ação de autenticação está em andamento
  error: string | null; // Armazena mensagens de erro de autenticação
  login: (credentials: { username: string; password: string }) => Promise<void>;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [user, setUser] = useState<User | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const login = async (credentials: { username: string; password: string }) => {
    setLoading(true);
    setError(null);
    try {
      const response = await forLogin.post('/Auth/login', credentials);

      if(response.status != 200)
        throw response
      
      console.log(response);
      const accessToken = (response.data as { accessToken: string }).accessToken;

      localStorage.setItem('accessToken', accessToken);
      localStorage.setItem('accessToken', accessToken);
      // localStorage.setItem('user_id', user_data.data.id);
      // localStorage.setItem('user', JSON.stringify(user_data.data));

      console.log(user);
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
