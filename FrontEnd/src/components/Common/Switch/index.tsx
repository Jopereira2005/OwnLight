import styled from './style.module.scss'

import { OnIcon } from "../../../assets/Cards/On"

interface SwichProp {
  state: boolean,
  toggleSwitch: any
}

const Switch = ({ state, toggleSwitch}: SwichProp) => {
  return (
    <div className={`${styled.switch} ${state ? styled.switch__active : '' }`} onClick={toggleSwitch}>
      <span className={ styled.switch__text } >{state ? "ON" : "OFF"}</span>
      <div className={`${ styled.switch__icon } ${state ? styled.switch__icon__on : ""}`}>
        <OnIcon className={ styled.switch__icon__svg }/>
      </div>
    </div>
  )
}

export default Switch