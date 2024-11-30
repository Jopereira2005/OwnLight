import React, { useEffect, useState } from 'react';

import styled from './style.module.scss'

import InputSelect from '../../Common/InputSelect';

import { Room } from '../../../interfaces/Room'
import { Device } from '../../../interfaces/Device'


interface CreateModalProps {
  device: Device,
  rooms: Room[],
  isOpen: boolean,
  toggleEditModal: () => void,
  onSubmit: (dados: FormData) => void,
}

const EditModal = ({ device, rooms, isOpen, toggleEditModal, onSubmit }: CreateModalProps) => {
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
  const [isDimmable, setIsDimmable] = useState(false);
  const [roomId, setRoomId] = useState<string | number | null>(null);

  const options = rooms.map((room) => ({
    value: room.id_room!,
    label: room.name,
  }));

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const formData = new FormData(e.currentTarget);
    onSubmit(formData);
    handleClose();
  }

  const setDefaultData = () => {
    setName(device.name ?? '');
    setIsDimmable(device.is_dimmable ?? false);
    setRoomId(device.id_room ?? '');
  }

  const handleClose = () => {
    toggleEditModal();
  }

  return (
    <>
      <div className={ `${styled.modal} ${isOpen ? styled.modal__open : styled.modal__closed}` }>
        <h1 onClick={ setDefaultData } className={ styled.modal__title }>Editar Dispositivo</h1>
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
                  onChange={(e) => setIsDimmable(e.target.checked) }
                />
                <span className={ styled.modal__form__container_input__switch__slider }></span>
              </label>
            </div>

            <div className={ styled.modal__form__container_input__select }>
              <label htmlFor="room">Selecione a Sala:</label>
              <InputSelect 
                id="room"
                name="room"
                options={ options } 
                inputValue={ options.find(option => option.value === Number(roomId)) || null }
                onChangeFunc={ (option: { value: number; label: string; } | null) => setRoomId(option ? option.value : '') }
                placeholder="Selecione uma sala..." 
                required
              />
            </div>
          </div>

          <button type="submit" className={ styled.modal__form__button }>Editar</button>
        </form>
      </div>
      { isOpen && <div className={ styled.backdrop } onClick={ toggleEditModal }></div> }
    </> 
  )
}

export default EditModal