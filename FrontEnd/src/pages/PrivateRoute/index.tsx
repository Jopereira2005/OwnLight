import React from 'react';
import { Navigate } from 'react-router-dom';
import { useAuth } from '../../context/authContext';

interface PrivateRouteProps {
  children: JSX.Element;
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({ children }) => {
  const { user } = useAuth(); // Aqui, você verifica se o usuário está logado.

  return user ? children : <Navigate to="/login" />;
};

export default PrivateRoute;