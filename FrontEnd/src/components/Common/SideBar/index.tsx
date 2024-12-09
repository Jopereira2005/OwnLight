import styled from './style.module.scss'

import Logo from '../../../assets/Header_Sidebar/logo.svg'
import { LogoutIcon } from '../../../assets/Header_Sidebar/logout'

import { useEffect } from 'react';

import { useAuth } from '../../../context/authContext';

interface SidebarProps {
  isOpen: boolean;
  toggleSidebar: () => void
}

const Sidebar = ({ isOpen, toggleSidebar}: SidebarProps) => {
  const { logout } = useAuth()
  useEffect(() => {
    if (isOpen) {
      document.body.classList.add("no_scroll");
    } else {
      document.body.classList.remove("no_scroll");
    }
  }, [isOpen]);

  const user = JSON.parse(localStorage.getItem('user') || '')

  return (
    <>
      <div className={ `${styled.sidebar} ${isOpen ? styled.sidebar__open : styled.sidebar__closed}` }>
        <div className={ styled.sidebar__container }>
          <div className={ styled.sidebar__container__profile }>
            <img onClick={() => toggleSidebar()} src={ Logo } className={ styled.sidebar__container__profile__img } alt="profile" />
            <h1 className={ styled.sidebar__container__profile__text }>Seja bem vindo, {user.name}👋</h1>
          </div>
          <button onClick={() => logout()} className={ styled.sidebar__container__button }>Sair <LogoutIcon/></button>
        </div>
      </div>

      { isOpen && <div className={ styled.backdrop } onClick={ toggleSidebar }></div> }
    </>
  )
}

export default Sidebar