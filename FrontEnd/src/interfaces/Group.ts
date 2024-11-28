import { User } from './User'
import { Device } from './Device'

export interface Group {
  id_group?: number,
  name: string,
  devices?: Device[],
  user?: User
}