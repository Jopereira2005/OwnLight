import axios from 'axios';

// Base URLs
const loginURL = "https://51ce-2804-90-5000-6d2c-8c38-d2b9-6213-5483.ngrok-free.app"; //5008
const userURL = "https://51ce-2804-90-5000-6d2c-8c38-d2b9-6213-5483.ngrok-free.app"; //5008
const deviceURL = "https://8198-2804-90-5000-6d2c-8c38-d2b9-6213-5483.ngrok-free.app"; //5034
const automationURL = "https://eef2-2804-90-5000-6d2c-8c38-d2b9-6213-5483.ngrok-free.app"; //5233

// Função para criar instâncias de Axios

const createAxiosInstance = (baseURL: string) => {
  const instance = axios.create({
    baseURL: `${baseURL}/api`,
    headers: {
      "ngrok-skip-browser-warning": "ok"
    }
  });

  instance.interceptors.request.use(
    (config) => {
      const token = localStorage.getItem('accessToken');
      if (token && config.headers) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    },
    (error: any) => Promise.reject(error)
  );

  let isRefreshing = false;
  let pendingRequests: (() => void)[] = [];

  instance.interceptors.response.use(
    (response) => response,
    async (error: any) => {
      if (error.response?.status === 401) {
        if (!isRefreshing) {
          isRefreshing = true;
          try {
            const refreshToken = localStorage.getItem('refreshToken');
            const { data } = await axios.post<{ accessToken: string }>(
              `${userURL}/api/Auth/refresh_token/${localStorage.getItem('user_id')}`,
              { refreshToken }
            );
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
            if (error.config.headers) {
              error.config.headers.Authorization = `Bearer ${localStorage.getItem('accessToken')}`;
            }
            resolve(axios(error.config));
          });
        });
      }
      return Promise.reject(error);
    }
  );

  return instance;
};

// Instâncias para as APIs
export const forLogin = axios.create({ baseURL: `${loginURL}/api`, headers: { "ngrok-skip-browser-warning": "ok" } });
export const userAPI = createAxiosInstance(userURL);
export const deviceAPI = createAxiosInstance(deviceURL);
export const automationAPI = createAxiosInstance(automationURL);
