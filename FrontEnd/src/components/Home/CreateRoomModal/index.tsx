import React, { useEffect, useState } from 'react';

import styled from './style.module.scss'

interface CreateRoomModalProps {
  isOpen: boolean;
  toggleCreateRoomModal: () => void
  onSubmit: (dados: FormData) => void;
}

const CreateRoomModal = ({ isOpen, toggleCreateRoomModal, onSubmit }: CreateRoomModalProps) => {
  useEffect(() => {
    if (isOpen) {
      document.body.classList.add("no_scroll");
    } else {
      document.body.classList.remove("no_scroll");
    }
  }, [isOpen]);

  const [name, setName] = useState('');

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const formData = new FormData(e.currentTarget);
    onSubmit(formData);
    setName('');
    handleClose();
  }

  const handleClose = () => {
    setName('');
    toggleCreateRoomModal()
  }

  return (
    <>
      <div className={ `${styled.modal} ${isOpen ? styled.modal__open : styled.modal__closed}` }>
        <h1 className={ styled.modal__title }>Criar Ambiente</h1>
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
                  placeholder="Digite o nome do Ambiente..."
                  required
                  autoComplete="off"
                /> 
            </div>
          </div>

          <button type="submit" className={ styled.modal__form__button }>Criar</button>
        </form>
      </div>
      { isOpen && <div className={ styled.backdrop } onClick={ toggleCreateRoomModal }></div> }
    </> 
  )
}

export default CreateRoomModal
