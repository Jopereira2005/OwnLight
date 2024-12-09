import React, { useEffect, useState } from 'react';

import styled from './style.module.scss'

import InputSelect from '../../Common/InputSelect';

import { Room } from '../../../interfaces/Room'
import { Device } from '../../../interfaces/Device'

import { TrashIcon } from '../../../assets/Home/Trash'

interface EditModalProps {
  device: Device,
  rooms: Room[],
  isOpen: boolean,
  toggleEditModal: () => void,
  deleteRoutineFunc: ( id: string ) => Promise<void>,
  onSubmit: (dados: FormData, id: string) => void,
}

const EditModal = ({ device, rooms, isOpen, toggleEditModal, deleteRoutineFunc, onSubmit }: EditModalProps) => {
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
  const [roomId, setRoomId] = useState<string | null>('null');
  const [brightness, setBrightness] = useState(100);

  const options = rooms.map((room) => ({
    value: room.id!,
    label: room.name,
  }));

  const setDefaultData = () => {
    setName(device.name ?? '');
    setRoomId(device.roomId ?? '');
    setBrightness(device.brightness ?? 100);
  }

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const formData = new FormData(e.currentTarget);
    onSubmit(formData, device.id || '');
    toggleEditModal();
  }
  
  const handleDelete = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();
    if (device.id) {
      deleteRoutineFunc(device.id);
    }
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

            { device.isDimmable &&    
              <div className={ styled.modal__form__container_input__range }>
                <label htmlFor="brightness" >Brilho: { brightness }</label>
                <input
                  id="brightness"
                  name="brightness"
                  type="range" 
                  min="1" 
                  max="100" 
                  value={ brightness }
                  onChange={(e) => setBrightness(Number(e.target.value))}
                />
              </div>
            }
            <div className={ styled.modal__form__container_input__select }>
              <label htmlFor="room">Selecione a Sala:</label>
              <InputSelect 
                id="room"
                name="room"
                options={ options } 
                inputValue={ options.find(option => option.value === roomId) || null }
                onChangeFunc={ (option: { value: string; label: string; } | null) => setRoomId(option ? option.value : '') }
                placeholder="Selecione uma sala..." 
                required
              />
            </div>
          </div>

          <div className={ styled.modal__form__button_group }>
            <button className={ styled.modal__form__button_group__button_trash } onClick={ handleDelete }><TrashIcon className={styled.modal__form__button_group__button_trash__icon }/></button>
            <button type="submit" className={ styled.modal__form__button_group__button }>Editar</button>
          </div>
        </form>
      </div>
      { isOpen && <div className={ styled.backdrop } onClick={ toggleEditModal }></div> }
    </> 
  )
}

export default EditModal