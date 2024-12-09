import { useState } from 'react';

import styled from './style.module.scss'

import Switch from '../../Common/Switch'

import { MoreIcon } from '../../../assets/Cards/More';

import { Device } from '../../../interfaces/Device';

interface CardProps {
  device: Device,
  onClickFunc: () => void
  chanceStateFunc: (id: string) => Promise<void>
  sendData: ( id_device: string ) => void;
}

const Card = ({ device, onClickFunc, chanceStateFunc, sendData}: CardProps) => {
  const [isOn, setIsOn] = useState(device.status != 'Off');

  const toggleSwitch = () => {
    setIsOn(!isOn);
    chanceStateFunc(device.id || '');
  }

  return (
    <>
      <div className={ `${styled.card} ${ isOn ? styled.card__on : '' }` }>
        <div className={ styled.card__container }>
          <div className={ styled.card__container__name }>{ device.name }</div>

          <div className={ styled.card__container__div }>
            <Switch state={ isOn } toggleSwitch={ toggleSwitch } />
            <MoreIcon onClick={ () => { onClickFunc(); sendData(device.id || '') }} className={ styled.card__container__div__icon }/>
          </div>
        </div>
      </div>
    </>
  )
}

export default Card