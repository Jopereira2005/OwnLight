import { useState } from 'react';

import styled from './style.module.scss'

import logo from '../../../assets/Header_Sidebar/logo.svg'
import ProfileIcon from '../../../assets/Header_Sidebar/Profile.svg'

import Sidebar from '../SideBar';

const Header = () => {
  const [isOpen, setIsOpen] = useState(false); // Estado para controlar abertura

  // Função para alternar entre aberto e fechado
  const toggleSidebar = () => {
    setIsOpen(!isOpen);
  };

  return (
    <>
      <header className={ styled.header }>
        <div className={ styled.header__logo }>
          <img src={ logo } className={ styled.header__logo__img } alt="logo" />
          <h1 className={ styled.header__logo__text }>Own<span>Light</span></h1>
        </div>

        <div className={ styled.header__profile }>
          <img onClick={ toggleSidebar }src={ ProfileIcon } className={ styled.header__logo__img } alt="profile" />
        </div>
      </header>
      <Sidebar isOpen={isOpen} toggleSidebar={toggleSidebar}/>
    </>
  )
}

export default Header