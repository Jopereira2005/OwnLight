import styled from'./style.module.scss';
import Input from '../../components/Login_Register/Input'
import logo from '../../assets/Login_Register/logo.svg'
import facebook_icon from '../../assets/Login_Register/facebook.svg'
import google_icon from '../../assets/Login_Register/google.svg'
import x_icon from '../../assets/Login_Register/x.svg'

import AlertNotification from '../../components/Common/AlertNotification'

import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

import { useAuth } from '../../context/authContext';

function Login() {
  const { login } = useAuth();
  const [credentials, setCredentials] = useState({ username: '', password: '' });

  const [alertProps, setAlertProps] = useState({ message: '', timeDuration: 0, type: 'success' as 'success' | 'error'});
  const [alertOpen, setAlertOpen] = useState(false)

  const navigate = useNavigate();

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const response: any = await login(credentials);

      if(response.status != 200)  
       throw response.data
       
      setAlertProps({
        message: "Login efetuado com sucesso.",
        timeDuration: 3000,
        type: 'success'     
      });
      setAlertOpen(true);

      setTimeout(() => {
        navigate("/");
      }, 3500)

    } catch(err: any){ 
      setAlertProps({
        message: String(err.content),
        timeDuration: 3000,
        type: 'error'     
      });
      setAlertOpen(true);
    }
  };

  return (
    <>
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
              <form onSubmit={ handleLogin } className={ styled.main__login__form }>
                <div className={ styled.main__login__form__inputs }>
                  <Input
                    type="text"
                    name="username"
                    label="Nome de Usuario"
                    value={credentials.username}
                    onChangeFunc={(e: React.ChangeEvent<HTMLInputElement>) => setCredentials({ ...credentials, username: e.target.value })}
                  />

                  <Input
                    type="password"
                    name="password"
                    label="Senha"
                    value={credentials.password}
                    onChangeFunc={(e: React.ChangeEvent<HTMLInputElement>) => setCredentials({ ...credentials, password: e.target.value })}
                  />
                </div>
                <button className={ styled.main__login__button }>Entrar</button>
                <a href="#" onClick={() => navigate("/cadastro")} className={ styled.main__login__link }>Criar conta</a>
              </form>
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
      <AlertNotification
        {...alertProps}
        state={ alertOpen }
        handleClose={ () => setAlertOpen(false)}
      />
    </>
  )
}

export default Login
