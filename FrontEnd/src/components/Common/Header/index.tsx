import styled from './style.module.scss'
import logo from '../../../assets/Header_Sidebar/logo.svg'
import { ProfileIcon } from '../../../assets/Header_Sidebar/Profile'

const Header = () => {
  return (
    <header className={ styled.header }>
      <div className={ styled.header__logo }>
        <img src={ logo } className={ styled.header__logo__img } alt="logo" />
        <h1 className={ styled.header__logo__text }>Own<span>Light</span></h1>
      </div>
      
      <ProfileIcon className={ styled.header__profile }/>
    </header>
  )
}

export default Header