import { useState } from 'react';

import styled from './style.module.scss'

import Switch from '../../Common/Switch'

import { MoreIcon } from '../../../assets/Cards/More';

interface CardProps {
  id: number,
  name: string,
  state: boolean,
  onClickFunc: () => void
  sendData: ( id_device: number ) => void;
}

const Card = ({ id, name, state, onClickFunc, sendData}: CardProps) => {
  const [isOn, setIsOn] = useState(state);

  const toggleSwitch = () => {
    setIsOn((prevState) => !prevState);
  };

  return (
    <>
      <div className={ `${styled.card} ${ isOn ? styled.card__on : '' }` }>
        <div className={ styled.card__container }>
          <div className={ styled.card__container__name }>{ name }</div>

          <div className={ styled.card__container__div }>
            <Switch state={ isOn } toggleSwitch={ toggleSwitch }/>
            <MoreIcon onClick={ () => { onClickFunc(); sendData(id) }} className={ styled.card__container__div__icon }/>
          </div>
        </div>
      </div>
    </>
  )
}

export default Card