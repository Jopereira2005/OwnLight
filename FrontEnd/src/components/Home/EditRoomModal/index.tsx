import React, { useEffect, useState } from 'react';

import styled from './style.module.scss'

import { Room } from '../../../interfaces/Room'

import { TrashIcon } from '../../../assets/Home/Trash'

interface EditRoomModalProps {
  room: Room,
  isOpen: boolean,
  toggleEditRoomModal: () => void,
  deleteRoomFunc: ( id: string ) => Promise<void>,
  onSubmit: (dados: FormData, id: string) => void,
}

const EditRoomModal = ({ room, isOpen, toggleEditRoomModal, deleteRoomFunc, onSubmit }: EditRoomModalProps) => {
  useEffect(() => {
    if (isOpen) {
      document.body.classList.add("no_scroll");
    } else {
      document.body.classList.remove("no_scroll");
    }
  }, [isOpen]);

  useEffect(() => {
    if (isOpen) {
      setDefaultData();
    }
  }, [isOpen == true]);

  const [name, setName] = useState('');

  const setDefaultData = () => {
    setName(room.name ?? '');
  }

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const formData = new FormData(e.currentTarget);
    onSubmit(formData, room.id || '');
    toggleEditRoomModal();
  }
  
  const handleDelete = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();
    if (room.id) {
      deleteRoomFunc(room.id);
    }
    toggleEditRoomModal();
  }

  return (
    <>
      <div className={ `${styled.modal} ${isOpen ? styled.modal__open : styled.modal__closed}` }>
        <h1 onClick={ setDefaultData } className={ styled.modal__title }>Editar Ambiente</h1>
        <form onSubmit={ handleSubmit } className={ styled.modal__form }>
          <div className={ styled.modal__form__container_input}>
            <div className={ styled.modal__form__container_input__input}>
              <label htmlFor="nome">Nome: </label>
              <input
                type="text"
                id="name"
                name="name"
                minLength={3}
                value={ name }
                onChange={(e) => setName(e.target.value)}
                placeholder="Digite o nome do dispositivo..."
                required
                autoComplete="off"
              /> 
            </div>
          </div>

          <div className={ styled.modal__form__button_group }>
            <button className={ styled.modal__form__button_group__button_trash } onClick={ handleDelete }><TrashIcon className={styled.modal__form__button_group__button_trash__icon }/></button>
            <button type="submit" className={ styled.modal__form__button_group__button }>Editar</button>
          </div>
        </form>
      </div>
      { isOpen && <div className={ styled.backdrop } onClick={ toggleEditRoomModal }></div> }
    </> 
  )
}

export default EditRoomModal