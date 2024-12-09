import { userAPI } from './server';

const userService =  {
  async register( name:string, username: string, email: string, password: string ) {
    const data = {
      name: name,
      username: username,
      email: email,
      password: password
    }
    try {
      const response = await userAPI.post('/User/create', data);
      return response;
    } catch (error: any) {
      return error.response.data;
    }
  },
}

export default userService