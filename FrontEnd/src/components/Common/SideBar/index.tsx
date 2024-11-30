import styled from './style.module.scss'

import ProfileIcon from '../../../assets/Header_Sidebar/Profile.svg'
import { useEffect } from 'react';

interface SidebarProps {
  isOpen: boolean;
  toggleSidebar: () => void
}

const Sidebar = ({ isOpen, toggleSidebar}: SidebarProps) => {
  useEffect(() => {
    if (isOpen) {
      document.body.classList.add("no_scroll");
    } else {
      document.body.classList.remove("no_scroll");
    }
  }, [isOpen]);

  return (
    <>
      <div className={ `${styled.sidebar} ${isOpen ? styled.sidebar__open : styled.sidebar__closed}` }>
        <div className={ styled.sidebar__container }>
          <div className={ styled.sidebar__container__profile }>
            <img onClick={() => toggleSidebar()} src={ ProfileIcon } className={ styled.sidebar__container__profile__img } alt="profile" />
            <h1 className={ styled.sidebar__container__profile__text }>MR|PAXE</h1>
          </div>
        </div>
      </div>

      { isOpen && <div className={ styled.backdrop } onClick={ toggleSidebar }></div> }
    </>
  )
}

export default Sidebar