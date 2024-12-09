import { useEffect, useRef, useState } from 'react';
import styled from './style.module.scss'

import { SeachIcon } from '../../../assets/Home/Search'

import Switch from '../../Common/Switch'

import { Device } from '../../../interfaces/Device'
import { Room } from '../../../interfaces/Room'

interface SearchBarProps {
  list: Device[],
  listRoom: Room[],
  handleModalFunc: () => void,
  sendData: ( id_device: string ) => void;
}

const SearchBar = ({ list, listRoom, handleModalFunc, sendData}: SearchBarProps) => {
  // const [isOn, setIsOn] = useState(false);
  const [filteredList, setFilteredList] = useState<Device[]>(list);
  const [inputValue, setInputValue] = useState('');
  
  const listaRef = useRef<HTMLDivElement | null>(null);
  
  const toggleSwitch = () => {
  };

  const handleClickOutside = (e: MouseEvent) => {
    if (listaRef.current && !listaRef.current.contains(e.target as Node)) {
      setInputValue("");
    }
  };

  useEffect(() => {
    setFilteredList( list.filter((item) => 
      item.name.toLowerCase().trim().includes(inputValue.toLowerCase().trim())
  ))
  }, [ inputValue ]);

  useEffect(() => {
    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);

  return (
    <div className={ styled.search } ref={listaRef}>
      <div className={ styled.search__search_bar} >
        <SeachIcon className={ styled.search__search_bar__img }/>
        <input className={ styled.search__search_bar__input } 
          type="text" 
          placeholder="Pesquise"
          value={ inputValue }
          onChange={ (e) => setInputValue(e.target.value) }
        />
      </div>

      { inputValue != '' &&
        <ul className={ styled.search__list } >
          { filteredList.length !== 0 ? 
            filteredList.map((item) => (
              <li key={ item.id } className={ styled.search__list__item }>
                <div onClick={() => { item.id && sendData(item.id); handleModalFunc()}} className={ styled.search__list__item__text }>
                  <div className={ styled.search__list__item__text__name }>
                    { item.name } |   
                  </div>
                  <span className={ styled.search__list__item__text__room }>
                    { listRoom.length != 0 ? listRoom.find((room) => room.id === item.roomId)?.name : ''}
                  </span>
                </div>
                <div className={ styled.search__list__item__btn }>
                  <Switch state={ item.status != "Off" ? true : false } toggleSwitch={ toggleSwitch } />
                </div>
              </li>
            )) :
            <span className={ styled.search__list__menssage }>Nenhum resultado encontrado</span>
          }
        </ul>
      }
    </div>
  )
}

export default SearchBar