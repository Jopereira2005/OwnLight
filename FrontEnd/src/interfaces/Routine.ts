import { User } from './User'
import { Room } from './Room'
import { Group } from './Group'

export interface Routine {
  id_routine?: number,
  name: string,
  id_target?: number,
  target_type: string
}