import styled from './style.module.scss'
import { SeachIcon } from '../../../assets/Home/Search'

const SearchBar = () => {
  return (
    <div className={ styled.search }>
      <div className={ styled.search__search_bar}></div>
      <SeachIcon className={ styled.search__search_bar__img }/>
      <input className={ styled.search__search_bar__input } type="text" placeholder="Pesquise"/>
    </div>
  )
}

export default SearchBar