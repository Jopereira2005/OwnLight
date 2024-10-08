import style from './style.module.scss'
import logo from '../../../assets/Home/logo.svg'
import profile from '../../../assets/Home/profile.svg'

const Header = () => {
  return (
    <>
      <header className={ style.header }>
        <div className={ style.header__logo }>
          <img src={ logo } className={ style.header__logo__img } alt="logo" />
          <h1 className={ style.header__logo__text }>Own<span>Light</span></h1>
        </div>

        <div className={ style.header__profile }>
          <img src={ profile } className={ style.header__logo__img } alt="profile" />
        </div>
      </header>
    </>
  )
}

export default Header