import { User } from './User'
import { Device } from './Device'

export interface Room {
  id_room?: number,
  name: string,
  devices?: Device[],
  user?: User
}