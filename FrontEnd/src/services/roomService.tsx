import { automationAPI } from './server';

const roomService =  {
  async create_room( name: string ) {
    try {
      const response = await automationAPI.post('/Room/create', { name });
      return response;
    } catch (error: any) {
      return error.response.data;
    }
  },

  async list_rooms(  ) {
    try {
      const response = await automationAPI.get('/Room/get/user_rooms');
      return response;
    } catch (error: any) {
      console.log(error)
      return error.response.data;
    }
  },

  async delete_room( id: string ) {
    try {
      const response = await automationAPI.delete(`/Room/delete/${id}`);
      return response;
    } catch (error: any) {
      return error.response.data;
    }
  },

  async update_room( id: string, name: string ) {
    try {
      const response = await automationAPI.put(`Room/update/${id}`, { name });
      return response;
    } catch (error: any) {
      return error.response.data;
    }
  }
}

export default roomService