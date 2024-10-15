import styled from './style.module.scss'

import Header from '../../components/Common/Header'
import Slider from '../../components/Common/Slider'
import NavBar from '../../components/Common/NavBar'

import { FilterIcon } from '../../assets/Home/Filter'
import { SeachIcon } from '../../assets/Home/Search'

function Home() {
  return (
    <div className={ styled.home }>
      <Header />
      <main className={ styled.main }>
        <div className={ styled.main__search }>
          <div className={ styled.main__search__search_bar}></div>
          <SeachIcon className={ styled.main__search__search_bar__img }/>
          <input className={ styled.main__search__search_bar__input } type="text" placeholder='Pessoas'/>
        </div>
        <div className={ styled.main__carousel }>
          <Slider/>
        </div>
        <div className={ styled.main__cards }>
          <div className={ styled.main__cards__header }>
            <h1 className={ styled.main__cards__header__text }>Sala</h1>
            <FilterIcon className={ styled.main__cards__header__icon }/>
          </div>
        </div>
      </main>
      <NavBar />
    </div>

  )
}

export default Home
