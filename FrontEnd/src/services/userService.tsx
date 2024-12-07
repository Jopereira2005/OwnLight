import { api } from './server';

interface RegisterData {
  name:string,
  username: string,
  email: string,
  password: string
}

const userService =  {
  async register({ name, username, email, password }: RegisterData) {
    const data = {
      name: name,
      username: username,
      email: email,
      password: password
    }
    try {
      const response = await api.post('/User/create', data);
      return response;
    } catch (error: any) {
      return error.response.data;
    }
  },
}

export default userService