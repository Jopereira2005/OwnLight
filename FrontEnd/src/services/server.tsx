import axios from 'axios';

const user_id = localStorage.getItem('user_id');
const baseURL = "http://localhost:5008/api"

// Criação da instância do Axios
const api = axios.create({
  baseURL: baseURL, // Substitua pela URL da sua API
});

const forLogin = axios.create({
  baseURL: baseURL, // Substitua pela URL da sua API
});

// Interceptor de requisição
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('accessToken'); // Obtém o accessToken do cookie
    if (token) {
      if (config.headers) {
        config.headers.Authorization = `Bearer ${token}`;
      }
    }
    return config;
  },
  (error) => Promise.reject(error)
);

let isRefreshing = false;
let pendingRequests: (() => void)[] = [];

api.interceptors.response.use(
  (response) => response,
  async (error) => {
    if (error.response?.status === 401) {
      if (!isRefreshing) {
        isRefreshing = true;
        try {
          const refreshToken = localStorage.getItem('refreshToken');
          const { data } = await axios.post<{ accessToken: string }>(`${baseURL}/Auth/refresh_token/${user_id}`, { refreshToken });
          localStorage.setItem('accessToken', data.accessToken);
          pendingRequests.forEach((cb) => cb());
          pendingRequests = [];
        } catch (refreshError) {
          pendingRequests = [];
          throw refreshError;
        } finally {
          isRefreshing = false;
        }
      }

      return new Promise((resolve) => {
        pendingRequests.push(() => {
          error.config.headers.Authorization = `Bearer ${localStorage.getItem('accessToken')}`;
          resolve(axios(error.config));
        });
      });
    }
    return Promise.reject(error);
  }
);

export { api };
export { forLogin };