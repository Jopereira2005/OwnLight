import React, { useEffect, useState } from 'react';

import styled from './style.module.scss'

interface CreateModalProps {
  isOpen: boolean;
  toggleCreateModal: () => void
  onSubmit: (dados: FormData) => void;
}

const CreateModal = ({ isOpen, toggleCreateModal, onSubmit }: CreateModalProps) => {
  useEffect(() => {
    if (isOpen) {
      document.body.classList.add("no_scroll");
    } else {
      document.body.classList.remove("no_scroll");
    }
  }, [isOpen]);

  const [name, setName] = useState('');
  const [isDimmable, setIsDimmable] = useState(false);

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const formData = new FormData(e.currentTarget);
    onSubmit(formData);
    handleClear();
    handleClose();
  }

  const handleClear = () => {
    setName('');
    setIsDimmable(false);
  }

  const handleClose = () => {
    handleClear();
    toggleCreateModal()
  }

  return (
    <>
      <div className={ `${styled.modal} ${isOpen ? styled.modal__open : styled.modal__closed}` }>
        <h1 className={ styled.modal__title }>Cria Dispositivo</h1>
        <form onSubmit={ handleSubmit } className={ styled.modal__form }>
          <div className={ styled.modal__form__container_input}>
            <div className={ styled.modal__form__container_input__input}>
                <label htmlFor="nome">Nome: </label>
                <input
                  type="text"
                  id="name"
                  name="name"
                  value={ name }
                  onChange={(e) => setName(e.target.value)}
                  placeholder="Digite o nome do dispositivo..."
                  required
                  autoComplete="off"
                /> 
            </div>

            <div className={ styled.modal__form__container_input__switch}>
              <h2 className={ styled.modal__form__container_input__switch__text }>Iluminação Ajustável</h2>
              <label className={ styled.modal__form__container_input__switch__input }>
                <input 
                  type="checkbox" 
                  id="dimmable"
                  name="dimmable"
                  checked={ isDimmable }
                  onChange={(e) => setIsDimmable(e.target.checked)}
                />
                <span className={ styled.modal__form__container_input__switch__slider }></span>
              </label>
            </div>
          </div>

          <button type="submit" className={ styled.modal__form__button }>Criar</button>
        </form>
      </div>
      { isOpen && <div className={ styled.backdrop } onClick={ toggleCreateModal }></div> }
    </>
    
  )
}

export default CreateModal
