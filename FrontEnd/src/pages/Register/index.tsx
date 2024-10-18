import styled from'./style.module.scss';
import Input from '../../components/Login_Register/Input'
import logo from '../../assets/Login_Register/logo.svg'
import facebook_icon from '../../assets/Login_Register/facebook.svg'
import google_icon from '../../assets/Login_Register/google.svg'
import x_icon from '../../assets/Login_Register/x.svg'

function Register() {
  return (
    <div className={ styled.login }>
      <div className={ styled.background }>
        <main className={ styled.main }>
          <div className={ styled.main__welcome }>
            <img src={ logo } alt="logo" className={ styled.main__welcome__logo } />
            <h1 className={ styled.main__welcome__text }>Bem-vindo ao <span>OwnLight!</span></h1>
          </div>

          <div className={ styled.main__div }></div>

          <div className={ styled.main__register }>
            <h1 className={ styled.main__register__title }>Cadastro</h1>
            <div className={ styled.main__register__form }>
              <div className={ styled.main__register__form__inputs }>
                <Input
                  type="text"
                  name="Nome"
                />

                <Input
                  type="user"
                  name="Usuário"
                />

                <Input
                  type="email"
                  name="Email"
                />

                <Input
                  type="password"
                  name="Senha"
                />

                <Input
                  type="password"
                  name="Confirmar Senha"
                />
              </div>
            </div>
            
            <button className={ styled.main__register__button }>Entrar</button>
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

export default Register
