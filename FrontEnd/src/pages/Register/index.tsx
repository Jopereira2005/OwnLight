import styled from'./style.module.scss';
import Input from '../../components/Login_Register/Input'
import logo from '../../assets/Login_Register/logo.svg'
import facebook_icon from '../../assets/Login_Register/facebook.svg'
import google_icon from '../../assets/Login_Register/google.svg'
import x_icon from '../../assets/Login_Register/x.svg'

import AlertNotification from '../../components/Common/AlertNotification'

import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import userService from '../../services/userService'

function Register() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [name, setName] = useState('');
  const [username, setUsername] = useState('');


  const [alertProps, setAlertProps] = useState({ message: '', timeDuration: 0, type: 'success' as 'success' | 'error' });
  const [alertOpen, setAlertOpen] = useState(false)

  const navigate = useNavigate();

  const handleRegister = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const response = await userService.register({ name, username, email, password });
      if (response.statusCode >= 400) {
        throw response;
      }
      setEmail('');
      setPassword('');
      setName('');
      setUsername('');

      setAlertProps({
        message: "Usuario cadastrado com sucesso",
        timeDuration: 3000,
        type: "success"
      })

      setTimeout(() => {
        navigate("/login");
      }, 3500)

      setAlertOpen(true);
    } catch (err: any) {
      console.log(err.content);
      setAlertProps({
        message: err.content,
        timeDuration: 3000,
        type: "error"
      })
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
              <h1 className={ styled.main__welcome__text }>Bem-vindo ao <span>OwnLight!</span></h1>
            </div>

            <div className={ styled.main__div }></div>

            <div className={ styled.main__register }>
              <h1 className={ styled.main__register__title }>Cadastro</h1>
              <form onSubmit={ handleRegister } className={ styled.main__register__form }>
                <div className={ styled.main__register__form__inputs }>
                  <Input
                    type="text"
                    name="name"
                    label="Nome"
                    value={ name }
                    onChangeFunc={ (e: React.ChangeEvent<HTMLInputElement>) => setName(e.target.value) }
                  />

                  <Input
                    type="text"
                    name="username"
                    label="Nome de usuário"
                    value={ username }
                    onChangeFunc={ (e: React.ChangeEvent<HTMLInputElement>) => setUsername(e.target.value) }
                  />

                  <Input
                    type="email"
                    name="name"
                    label="Email"
                    value={ email }
                    onChangeFunc={ (e: React.ChangeEvent<HTMLInputElement>) => setEmail(e.target.value) }
                  />

                  <Input
                    type="password"
                    name="password"
                    label="Senha"
                    value={ password }
                    onChangeFunc={ (e: React.ChangeEvent<HTMLInputElement>) => setPassword(e.target.value) }
                  />
                </div>

                <button type="submit" className={ styled.main__register__button }>Cadastrar</button>
              </form>
            </div>

            <div className={ styled.main__social_medias }>
              <p className={ styled.main__social_medias__text }>Cadastrar com</p>
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

export default Register
