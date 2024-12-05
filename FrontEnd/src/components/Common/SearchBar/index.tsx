import { useEffect, useRef, useState } from 'react';
import styled from './style.module.scss'

import { SeachIcon } from '../../../assets/Home/Search'

import Switch from '../../Common/Switch'

import { Device } from '../../../interfaces/Device'


interface SearchBarProps {
  list: Device[],
  handleStateFunc: ( id_device: number, state: boolean ) => void,
  handleModalFunc: () => void,
  sendData: ( id_device: number ) => void;
}

const SearchBar = ({ list, handleStateFunc, handleModalFunc }: SearchBarProps) => {
  const [isOn, setIsOn] = useState(false);
  const [filteredList, setFilteredList] = useState<Device[]>(list);
  const [inputValue, setInputValue] = useState('');
  
  const listaRef = useRef<HTMLDivElement | null>(null);
  
  const toggleSwitch = () => {
    setIsOn(!isOn);
  };


  const handleClickOutside = (e: MouseEvent) => {
    console.log("a");
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
              <li key={ item.id_device } className={ styled.search__list__item }>
                <div className={ styled.search__list__item__name }>
                  { item.name }
                </div>
                <Switch state={ item.state || false } toggleSwitch={ toggleSwitch }/>
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