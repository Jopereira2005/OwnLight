import { User } from './User'
import { Room } from './Room'
import { Group } from './Group'

export interface Device {
  id_device?: number,
  id_user?: number,
  id_room?: number,
  id_group?: number,
  name: string,
  type?: string,
  status?: boolean,
  is_dimmable?: boolean,
  brightness?: number,
  user?: User,
  room?: Room,
  group?: Group
}