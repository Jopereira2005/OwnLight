import { useState } from 'react';

import styled from './style.module.scss'

import Switch from '../../Common/Switch'

interface CardProps {
  id: number,
  name: string,
  time: string,
  group: string,
  days: string[] | null,
  state: boolean
}

const Card = ({ id, name, time, group, days, state }: CardProps) => {
  const [isOn, setIsOn] = useState(state);

  const toggleSwitch = () => {
    setIsOn((prevState) => !prevState);
  };

  const setDays = (days : string[] | null ) => {
    if (!days) {
      return "Once";
    }
    if (days.length === 7) {
      return "Daily";
    }
  
    return days.join(', ');
  };

  return (
    <div className={ `${styled.card} ${ isOn ? '' : styled.card__off }` }>
      <div className={ styled.card__container }>
        <div className={ styled.card__container__div }>
          <div className={ styled.card__container__div__top }>
            <h1 className={ styled.card__container__div__top__name }>{ name }</h1>
            <p className={ styled.card__container__div__top__group }>{ group }</p>
          </div>
          <div className={ styled.card__container__div__bottom }>
            <h1 className={ styled.card__container__div__bottom__time }>{ time }</h1>
            <p className={ styled.card__container__div__bottom__days }>| { setDays(days) }</p>
          </div>
        </div>
        <Switch state={ isOn } toggleSwitch={ toggleSwitch }/>
      </div>
    </div>
  )
}

export default Card