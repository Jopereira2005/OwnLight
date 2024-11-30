import styled from'./style.module.scss';
import Input from '../../components/Login_Register/Input'
import logo from '../../assets/Login_Register/logo.svg'
import facebook_icon from '../../assets/Login_Register/facebook.svg'
import google_icon from '../../assets/Login_Register/google.svg'
import x_icon from '../../assets/Login_Register/x.svg'

function Login() {
  return (
    <div className={ styled.login }>
      <div className={ styled.background }>
        <main className={ styled.main }>
          <div className={ styled.main__welcome }>
            <img src={ logo } alt="logo" className={ styled.main__welcome__logo } />
            <h1 className={ styled.main__welcome__text }>Bem-vindo de volta ao <span>OwnLight!</span></h1>
          </div>

          <div className={ styled.main__div }></div>

          <div className={ styled.main__login }>
            <h1 className={ styled.main__login__title }>Email</h1>
            <div className={ styled.main__login__form }>
              <div className={ styled.main__login__form__inputs }>
                <Input
                  type="email"
                  name="Email"
                />

                <Input
                  type="password"
                  name="Senha"
                />
              </div>
              <a href="#" className={ styled.main__login__link }>Esqueceu a senha?</a>
            </div>
            
            <button className={ styled.main__login__button }>Entrar</button>
            <a href="#" className={ styled.main__login__link }>Criar conta</a>
          </div>

          <div className={ styled.main__social_medias }>
            <p className={ styled.main__social_medias__text }>Entrar com</p>
            <div className={ styled.main__social_medias__imgs }>
              <img src={ facebook_icon } alt="facebook" className={ styled.main__social_medias__imgs } />
              <img src={ google_icon } alt="google" className={ styled.main__social_medias__imgs } />
              <img src={ x_icon } alt="x" className={ styled.main__social_medias__imgs } />
            </div>
          </div>  
        </main>
      </div>
    </div>
  )
}

export default Login
