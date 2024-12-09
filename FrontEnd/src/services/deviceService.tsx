import { deviceAPI } from './server';

const deviceService =  {
  async list_device() {
    try {
      const response = await deviceAPI.get('/Device/all');
      return response;
    } catch (error: any) {
      return error.response.data;
    }
  },

  async list_device_by_room( roomId: string ) {
    try {

      const response = await deviceAPI.get(`/Device/user_devices_by_room/${roomId}`);
      return response;
    } catch (error: any) {
      return error.response.data;
    }
  },

  async create_device( roomId: string, name:string, isDimmable: boolean ) {
    const data = {
      roomId: roomId,
      name: name,
      isDimmable: isDimmable,
      deviceType:  'Light'
    }

    try {
      await deviceAPI.post('/Device/create', data);
    } catch (error: any) {
      return error.error;
    }
  },

  async delete_device( id: string ) {
    try {
      const response = await deviceAPI.delete(`/Device/delete/${id}`);
      return response;
    } catch (error: any) {
      return error.response.data;
    }
  },

  async update_device_name( id: string, name: string ) {
    try {
      const response = await deviceAPI.put(`/Device/update/device_name/${id}`, { name });
      return response;
    } catch (error: any) {
      return error.response.data;
    }
  },

  async update_device_room( id: string, roomId: string ) {
    try {
      const response = await deviceAPI.put(`/Device/update/device_room/${id}`, { roomId });
      return response;
    } catch (error: any) {
      return error.response.data;
    }
  },

  async update_device_dim( id: string, brightness: number ) {
    try {
      const response = await deviceAPI.post(`/DeviceAction/dim/${id}`, { brightness });
      return response;
    } catch (error: any) {
      return error.response.data;
    }
  },

  async device_switch( id: string ) {
    try {
      const response = await deviceAPI.post(`/DeviceAction/switch/${id}`);
      return response;
    } catch (error: any) {
      return error.response.data;
    }
  }
}

export default deviceService;