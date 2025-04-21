import React, { useEffect, useState } from 'react';

import styled from './style.module.scss'

import InputSelect from '../../Common/InputSelect';
import InputSelectMult from '../../Common/InputSelectMult';

import { Room } from '../../../interfaces/Room';
import { ActionMeta, MultiValue } from 'react-select';

interface CreateModalProps {
  rooms: Room[],
  isOpen: boolean,
  toggleCreateModal: () => void,
  onSubmit: (dados: FormData) => void
}

const CreateModal = ({ rooms, isOpen, toggleCreateModal, onSubmit }: CreateModalProps) => {
  useEffect(() => {
    if (isOpen) {
      document.body.classList.add("no_scroll");
    } else {
      document.body.classList.remove("no_scroll");
    }
  }, [isOpen]);

  const [name, setName] = useState('');
  const [roomId, setRoomId] = useState('');

  const [isDimmable, setIsDimmable] = useState(false);

  const [selectDays, setSelectDays] = useState([]);
  const [inputRepeat, setInputRepeat] = useState(false);



  const options = rooms.map((room) => ({
    value: room.id!,
    label: room.name,
  }));

  const days = [
    { value: 0, id: "once", label: "Uma vez" },
    { value: 1, id: "seg", label: "Segunda" },
    { value: 2, id: "tec", label: "Terça-feira" },
    { value: 3, id: "qua", label: "Quarta-feira" },
    { value: 4, id: "qui", label: "Quinta-feira" },
    { value: 5, id: "sex", label: "Sexta-feira" },
    { value: 6, id: "sab", label: "Sabado" },
    { value: 7, id: "dom", label: "Domingo" }
  ]

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
        <h1 className={ styled.modal__title }>Cria Rotina</h1>
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
            
            <div className={ styled.modal__form__container_input__repeat }>
              <h2 className={ styled.modal__form__container_input__repeat__label }>Repetição </h2>
              <div className={ styled.modal__form__container_input__repeat__click } onClick={ () => setInputRepeat(!inputRepeat) }>
                Dias
              </div>
              {inputRepeat &&
                <div className={ styled.modal__form__container_input__repeat__days }>
                  { days.map((day) => (
                    <div key={day.value} className={ styled.modal__form__container_input__repeat__days__day }>
                      <input type="checkbox" id={ day.id } name={ day.id } value={ day.value }/>
                      <label  htmlFor={ day.id }>{ day.label }</label>
                    </div>
                  ))}
                </div>
              }
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
